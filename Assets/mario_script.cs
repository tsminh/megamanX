using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class mario_script : MonoBehaviour
{
    public Slider healthBar;
    public int health = 100;
    public AudioClip buster_s;
    public AudioClip buster_m;
    public AudioClip buster_l;
    public AudioClip dash;
    public AudioClip land;
    public AudioClip jump;
    public AudioClip foot_step;
    public AudioClip side_dash;
    public AudioClip charging;
    public AudioClip charging2;
    public GameObject Bullet1;
    public GameObject Bullet2;
    public GameObject Bullet3;
    public AudioClip damage;
    public AudioClip dead;
    Animator m_animator;
    public GameObject obj_side_dash_dust;
    public GameObject obj_dashDust_base;
    Animator m_powerCharger;
    Rigidbody2D _rigid;
    BoxCollider2D m_collider;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    float jumpForce = 3.0f;
    [SerializeField]
    bool isRunning;
    int countFrame = 0;
    public float speed = 1;
    int jumpLeft = 2;
    bool resetJump = false;
    bool resetDash = false;
    bool flip = false;
    bool isGettingPower = false;
    float power = 0f;
    bool playLandSound = true;
    AudioSource[] audioSource = new AudioSource[20];
    int chargingState = 0;
    bool Die = false;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = 1;
        audioSource[0] = gameObject.AddComponent<AudioSource>();
        audioSource[0].clip = buster_s;
        audioSource[1] = gameObject.AddComponent<AudioSource>();
        audioSource[1].clip = buster_m;
        audioSource[2] = gameObject.AddComponent<AudioSource>();
        audioSource[2].clip = buster_l;
        audioSource[3] = gameObject.AddComponent<AudioSource>();
        audioSource[3].clip = dash;
        audioSource[4] = gameObject.AddComponent<AudioSource>();
        audioSource[4].clip = land;
        audioSource[5] = gameObject.AddComponent<AudioSource>();
        audioSource[5].clip = jump;
        audioSource[6] = gameObject.AddComponent<AudioSource>();
        audioSource[6].clip = foot_step;
        audioSource[7] = gameObject.AddComponent<AudioSource>();
        audioSource[7].clip = side_dash;
        audioSource[8] = gameObject.AddComponent<AudioSource>();
        audioSource[8].clip = charging;
        audioSource[9] = gameObject.AddComponent<AudioSource>();
        audioSource[9].clip = charging2;
        audioSource[10] = gameObject.AddComponent<AudioSource>();
        audioSource[10].clip = damage;
        audioSource[11] = gameObject.AddComponent<AudioSource>();
        audioSource[11].clip = dead;
        m_collider = gameObject.GetComponent<BoxCollider2D>();
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        m_animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_powerCharger = gameObject.transform.Find("PowerCharger").GetComponent<Animator>();

        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_animator.GetBool("isDie"))
        {
            return;
        }
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput < 0) flip = true;
        else if (horizontalInput > 0) flip = false;
        _rigid.velocity = new Vector2(horizontalInput * speed, _rigid.velocity.y);
        spriteRenderer.flipX = flip;
        if (horizontalInput != 0) m_animator.SetBool("isRunning", true);
        else m_animator.SetBool("isRunning", false);
        if (m_animator.GetBool("isRunning") && !m_animator.GetBool("isJump") && Time.frameCount % 15 == 0)
        {
            audioSource[6].Play();
        }

        m_powerCharger.SetFloat("power", power);
        if (isGettingPower)
        {
            if (chargingState == 0 && !audioSource[8].isPlaying)
            {
                audioSource[8].Play();
                chargingState = 1;
            } else if (chargingState == 1 && !audioSource[8].isPlaying && !audioSource[9].isPlaying)
            {
                audioSource[9].Play();
            }
            power += 0.01f;
        } else
        {
            chargingState = 0;
            audioSource[8].Stop();
            audioSource[9].Stop();
        }
        Debug.DrawLine(transform.position - new Vector3(0.1f, 0, 0), transform.position - new Vector3(0.1f, 0, 0) + Vector3.down * 0.2f, Color.red);
        Debug.DrawLine(transform.position + new Vector3(0.1f, 0, 0), transform.position + new Vector3(0.1f, 0, 0) +  Vector3.down * 0.2f, Color.red);
        Debug.DrawLine(transform.position + new Vector3(0, -0.13f, 0), transform.position + new Vector3(0, -0.13f, 0) + Vector3.left * 0.2f, Color.red);
        Debug.DrawLine(transform.position + new Vector3(0, -0.13f, 0), transform.position + new Vector3(0, -0.13f, 0) + Vector3.right * 0.2f, Color.red);
        if ((isGround() || jumpLeft > 0) && Input.GetKeyDown(KeyCode.V))
        {
            m_animator.SetBool("isJump", true);
            _rigid.velocity = new Vector2(_rigid.velocity.x, jumpForce * (m_animator.GetBool("isSideDash") ? 1.5f : 1));
            jumpLeft = jumpLeft - 1;
            if (jumpLeft == 0) m_animator.SetTrigger("isDoubleJump");
            playLandSound = true;
            audioSource[5].Play();
            StartCoroutine(Cooldown());
        } else
        {
            if (Input.GetKeyDown(KeyCode.X) && m_animator.GetBool("isRunning"))
            {
                if (!resetDash)
                {
                    m_animator.SetBool("isDash", true);
                    Vector3 tmp = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
                    GameObject dashClone = Instantiate(obj_dashDust_base);
                    dashClone.transform.position = tmp;
                    dashClone.GetComponent<Animator>().SetTrigger("isDash");
                    dashClone.GetComponent<SpriteRenderer>().flipX = !flip;
                    resetDash = true;
                    StartCoroutine(CooldownDash());
                    audioSource[3].Play();
                    Destroy(dashClone, 1.2f);
                }
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                
                resetDash = false;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                isGettingPower = true;
            }
            if (isGettingPower && Input.GetKeyUp(KeyCode.C))
            {
                if (power >= 1.3f)
                {
                    audioSource[2].Play();
                } else if (power >= 0.50f)
                {
                    audioSource[1].Play();
                } else
                {
                    audioSource[0].Play();
                }
                
                m_animator.SetTrigger("isShoot");
                Shoot();
                isGettingPower = false;
                power = 0;
            }

        }
        
        if (!isGround() && isTouchSide() && m_animator.GetBool("isRunning"))
        {
            Debug.Log("sidedash");
            if (Time.frameCount % 10 == 0)
            {
                audioSource[7].Play();
            }
            
            m_animator.SetBool("isSideDash", true);
            
            if (Time.frameCount % 20 == 0)
            {
                GameObject dashClone = Instantiate(obj_side_dash_dust);
                dashClone.transform.position = transform.position + new Vector3((0.05f + Random.RandomRange(0f, 0.05f)) * (!flip ? 1 : -1), 0, 0);
                Destroy(dashClone, 1.2f);
            }
            
            _rigid.velocity = new Vector2(_rigid.velocity.x, -0.5f);

        } else
        {
            m_animator.SetBool("isSideDash", false);
        }
        
        
    }
    void LateUpdate() {
        
    }
    bool isGround() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position - new Vector3(0.1f, 0, 0), Vector2.down, 0.2f, 1<<8);
        RaycastHit2D hitInfo2 = Physics2D.Raycast(transform.position + new Vector3(0.1f,0,0), Vector2.down, 0.2f, 1 << 8);
        if (hitInfo.collider != null || hitInfo2.collider != null)
        {
            
            if (!resetJump)
            {
                jumpLeft = 2;
                m_animator.SetBool("isJump", false);
                if (playLandSound) audioSource[4].Play();
                playLandSound = false;
                return true;
            }
        }


        return false;
    }
    bool isTouchSide()
    {
        RaycastHit2D hitInfo3 = Physics2D.Raycast(transform.position + new Vector3(0, -0.13f, 0), Vector2.left, 0.2f, 1 << 8);
        RaycastHit2D hitInfo4 = Physics2D.Raycast(transform.position + new Vector3(0, -0.13f, 0), Vector2.right, 0.2f, 1 << 8);
        if (hitInfo4.collider != null || hitInfo3.collider != null)
        {

            if (!resetJump)
            {
                jumpLeft = 2;
                return true;
            }
        }


        return false;
    }
    private IEnumerator Cooldown()
    {
        resetJump = true;
        yield return new WaitForSeconds(0.1f);
        resetJump = false;
    }
    private IEnumerator CooldownDash()
    {
        speed = 3.7f;
        yield return new WaitForSeconds(.2f);
        speed = 1;
        m_animator.SetBool("isDash", false);
    }
    private IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(.2f);
        m_animator.SetBool("isDamage", false);
    }
    void Shoot()
    {
        Vector3 dir = (!flip ? Vector3.right : Vector3.left);
        GameObject bullet;
        if (power >= 0.7f)
        {
            bullet = Instantiate(Bullet3, transform.position, transform.rotation);
        } else if (power >= 0.35f)
        {
            bullet = Instantiate(Bullet2, transform.position, transform.rotation);
        } else
        {
            bullet = Instantiate(Bullet1, transform.position, transform.rotation);
        }
        bullet.GetComponent<BulletScript>().start(dir);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            TakeDamage(collision.gameObject.GetComponent<Enemy>().damage);
    }
    public void TakeDamage(int dam)
    {
        if (Die)
        {
            return;
        }
        health -= dam;
        healthBar.value = (float)(1.0 * health / 100);
        if (!audioSource[10].isPlaying) audioSource[10].Play();
        Debug.Log(health);
        m_animator.SetBool("isDamage", true);
        StartCoroutine(ResetDamage());

        if (health <= 0)
        {
            audioSource[11].Play();
            m_animator.SetBool("isDie", true);
            Die = true;
        }
    }
}


