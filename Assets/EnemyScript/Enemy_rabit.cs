using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_rabit : Enemy
{
    
    bool canJump = true;
    public GameObject bulletRef;
    bool canShoot = true;
    void Start()
    {
        overrideFlip = true;
    }
    public override void MyMove()
    {
        GetComponent<SpriteRenderer>().flipX = (transform.position.x - target.transform.position.x < 0);
        //gameObject.GetComponent<SpriteRenderer>().flipX = ((target.transform.position.x - transform.position.x) >= 0);
        if (!canJump)
        {
            //GetComponent<Rigidbody2D>().velocity = new Vector2((-transform.position.x + target.transform.position.x) / Mathf.Abs(-transform.position.x + target.transform.position.x) * 0.0001f, 0f);

            return;
        }
        StartCoroutine(CoolDownCanJump());
        canJump = false;
        GetComponent<Animator>().SetTrigger("Jump");
        GetComponent<Rigidbody2D>().velocity = new Vector2(0  , 2f);
        
    }
    public override void NearPlayer()
    {
        GetComponent<SpriteRenderer>().flipX = (transform.position.x - target.transform.position.x < 0);
        if (!canShoot)
        {
            return;

        }
        StartCoroutine(CoolDownCanShoot());
        canShoot = false;
        gameObject.GetComponent<Animator>().SetBool("isShoot", true);
        GameObject bullet = Instantiate(bulletRef, transform.position, transform.rotation);
        bullet.transform.parent = transform.parent;
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y + .1f, transform.position.z);
        bullet.GetComponent<RabitBullet>().start(!gameObject.GetComponent<SpriteRenderer>().flipX ? Vector2.left : Vector2.right);
        
    }
    private IEnumerator CoolDownCanShoot()
    {
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
    private IEnumerator CoolDownCanJump()
    {
        gameObject.GetComponent<Animator>().SetBool("isShoot", false);
        yield return new WaitForSeconds(1f);
        canJump = true;
    }
}
