using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    public int health;
    public int damage;
    public float speed;
    public float distanceAttack;
    Vector3 preVec = Vector3.zero;
    int count = 0;
    int nextFrame;
    float dX;
    float dY;
    // Start is called before the first frame update
    void Start()
    {
        nextFrame = (int)(Random.value * 100);
        dX = Random.Range(-10, 10);
        dY = Random.Range(1, 25);
    }
    public void TakeDamage(int dam)
    {
        health -= dam;
        Debug.Log("Enemy: " + health);
        gameObject.GetComponent<Animator>().SetTrigger("Damage");
        if (health <= 0)
        {
            
            Die();
        }
    }
    void Die()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        gameObject.GetComponent<Animator>().SetBool("isDie", true);
        Destroy(gameObject, .5f);
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Animator>().GetBool("isDie"))
        {
            return;
        }
        gameObject.transform.rotation = new Quaternion(0,0,0,0);
        Debug.DrawLine(target.transform.position, transform.position, Color.green);
        if (Vector2.Distance(target.transform.position, transform.position) < distanceAttack)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            gameObject.GetComponent<Rigidbody2D>().velocity = speed * Time.deltaTime * new Vector3(gameObject.GetComponent<Rigidbody2D>().velocity.x, dY, 0);
        } else
        {
            MyMove();
        }

        GetComponent<SpriteRenderer>().flipX = !(preVec.x - transform.position.x > 0);
        preVec = transform.position;
    }
    void MyMove()
    {
        if (count < nextFrame )
        {
            
            gameObject.GetComponent<Rigidbody2D>().velocity = speed * Time.deltaTime * new Vector3(dX, dY, 0);
        }
        else 
        {
            count = 0;
            nextFrame = (int)(Random.value * 100);
            dX = Random.Range(-10, 10);
            dY = Random.Range(10, 25);
        }
        count++;
    }
}
