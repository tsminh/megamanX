using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public GameObject target;
    public int health;
    public int damage;
    public float speed;
    public float distanceAttack;
    protected Vector3 preVec = Vector3.zero; //the vector of previous frame
    protected int count = 0; //counting frame for nextFrame
    protected int nextFrame; //number frames will change the character behavior
    protected float dX; //random delta X movement
    protected float dY; //random  delta Y movement
    protected bool overrideFlip = false;
    public virtual void MyMove() { }
    // Move the character
    public virtual void NearPlayer() { }
    // Behavior when near the player
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
        //gameObject.GetComponent<Animator>().SetTrigger("Damage");
        GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(CoolDownDamage());
        if (health <= 0)
        {
            Die();
        }
    }
    protected void Die()
    {
        transform.parent.gameObject.GetComponent<EnemySound>().PlayDeadSound();
        GetComponent<Collider2D>().enabled = false;
        Destroy(GetComponent<Rigidbody2D>());
        gameObject.GetComponent<Animator>().SetBool("isDie", true);
        if (gameObject != null) { 
            Destroy(gameObject, .5f);
        }   
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
            NearPlayer();
        } else
        {
            MyMove();
        }

        if (!overrideFlip) GetComponent<SpriteRenderer>().flipX = !(preVec.x - transform.position.x >= 0);
        preVec = transform.position;
    }
    private IEnumerator CoolDownDamage()
    {
        yield return new WaitForSeconds(.2f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
