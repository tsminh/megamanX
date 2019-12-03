using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D r2d;
    private float angle = 0;
    private GameObject player;
    public float angleDiv = 0.03f;
    public float radius = 0.3f;
    public float deltaX = .5f, deltaY = .2f;
    bool isHigher = true; //is higher than player
    public Vector2 velocity;
    public float smoothX, smoothY;
    private float scale = .0f;
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        r2d.gravityScale = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = 0;
        float y = 0;
        if (scale < 0.8)
        {
            scale += 0.01f;
            this.transform.localScale = new Vector3(scale, scale, scale);
        }
        y = radius * Mathf.Sin(angle)+deltaY;
        x = radius * Mathf.Cos(angle) + player.transform.position.x+deltaX;
        float posX = Mathf.SmoothDamp(this.transform.position.x, x, ref velocity.x, smoothX);
        float posY = Mathf.SmoothDamp(this.transform.position.y, y, ref velocity.y, smoothY);

        this.transform.position = new Vector3(posX, posY, this.transform.position.z);
        angle += angleDiv * Mathf.Rad2Deg * Time.deltaTime;
    }
    public void TakeDamage(int dam)
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        gameObject.GetComponent<Animator>().SetBool("isDie", true);
        Debug.Log("Bamp");
        Destroy(gameObject, .5f);
    }
}
