using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mario_script : MonoBehaviour
{
    Animator m_animator;
    Rigidbody2D _rigid;
    BoxCollider2D m_collider;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    float jumpForce = 3.0f;
    [SerializeField]
    bool isRunning;
    int countFrame = 0;
    public float speed;
    int jumpLeft = 0;
    bool flip = false;
    
    // Start is called before the first frame update
    void Start()
    {
        m_collider = gameObject.GetComponent<BoxCollider2D>();
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        m_animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput < 0) flip = true;
        else if (horizontalInput > 0) flip = false;
        m_animator.SetFloat("Move", Mathf.Abs(horizontalInput));
        spriteRenderer.flipX = flip;
        
        

        if (Input.GetKeyDown(KeyCode.V) && (isGround() || jumpLeft > 0)) {
            // if (m_animator.GetBool("isJump")) {
            //     m_animator.SetTrigger("isDoubleJump");
            // } else 
            m_animator.SetBool("isJump", true);
            _rigid.velocity = new Vector2(_rigid.velocity.x, jumpForce);
            jumpLeft = jumpLeft - 1;
            
        }   
    
        
        _rigid.velocity = new Vector2(horizontalInput, _rigid.velocity.y);
        
    }
    void LateUpdate() {
        isGround();
    }
    bool isGround() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, m_collider.bounds.extents.y + 0.1f, 1<<8);
        if (hitInfo.collider != null) {
            jumpLeft = 0;
            m_animator.SetBool("isJump", false);

            return true;
        } else {
            return false;
        }
    }
}
