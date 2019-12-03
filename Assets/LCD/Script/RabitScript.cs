using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabitScript : MonoBehaviour
{
    public float distance = 1.5f;
    Rigidbody2D r2d;
    GameObject player;
    bool grounded = true;
    Animator anm;
    public int faceLeft=1;
    bool canJump = true;
    float jumpDelay = 1;
    public GameObject bulletRef;
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        anm = GetComponent<Animator>();
        r2d.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        anm.SetBool("grounded", grounded);
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < distance && grounded == true && canJump == true)
        {
            Shoot();
            r2d.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
            canJump = false;
            jumpDelay = 2;
        }
        if (grounded == false)
        {
            Vector3 pos = this.transform.position;
            pos.x -= faceLeft * 0.01f;
            this.transform.position = pos;
        }
        if (canJump == false)
        {
            if (jumpDelay > 0)
            {
                jumpDelay -= Time.deltaTime;
            }
            else
            {
                canJump = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        grounded = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        grounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
    }
    void Flip()
    {
        if (player.transform.position.x - transform.position.x > 0)
        {
            faceLeft = -1;
        }
        else
        {
            faceLeft = 1;
        }
        transform.localScale = new Vector3(faceLeft, 1, 1);
    }
    void Shoot()
    {
        anm.SetBool("isShoot", true);
       // GameObject bullet = (GameObject)Instantiate(bulletRef);
       // bullet.transform.position = new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z);
       // bullet.GetComponent<RabitBullet>().setDirection(faceLeft);
        anm.SetBool("isShoot", false);
    }
}
