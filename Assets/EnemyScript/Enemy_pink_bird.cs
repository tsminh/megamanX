using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_pink_bird : Enemy
{
    public GameObject bullet;
    void Start()
    {
        preVec = transform.position;
        nextFrame = (int)(Random.value * 100);
    }
    public override void MyMove() {
    }
    public override void NearPlayer() {
        if (GetComponent<Animator>().GetBool("isDie"))
        {
            return;
        }
        if (count < nextFrame)
        {
        }
        else
        {
            count = 0;
            gameObject.GetComponent<Animator>().SetTrigger("Shoot");
            GameObject worm = Instantiate(bullet, transform.position, transform.rotation);
            worm.transform.parent = transform.parent;
            nextFrame = (int)(Random.value * 200);
        }
        count++;
        
    }
}
