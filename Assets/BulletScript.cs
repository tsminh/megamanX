using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int damage;
    public void start(Vector3 dir)
    {
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }
    void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
        }
        if (collision.tag != "Player")
        {
            Destroy(gameObject);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
