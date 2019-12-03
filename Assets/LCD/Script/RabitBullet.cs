using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabitBullet : MonoBehaviour
{
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
        if (collision.tag == "Player")
        {
            collision.GetComponent<mario_script>().TakeDamage(damage);
        }
        if (collision.tag != "Enemy")
        {
            Destroy(gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
