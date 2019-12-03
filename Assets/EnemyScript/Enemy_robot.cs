using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_robot : Enemy
{
    Rigidbody2D r2d;
    GameObject player;
    Animator anm;
    public int faceLeft = 1;
    float distance = 2.3f;
    // Start is called before the first frame update
    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        anm = GetComponent<Animator>();
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        r2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        overrideFlip = true;
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

    override public void MyMove()
    {
        Flip();
        anm.SetBool("isShoot", false);
    }
    public override void NearPlayer()
    {
        Flip();
        anm.SetBool("isShoot", true);
    }
}
