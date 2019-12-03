using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Only_Jump_then_Hide : Enemy
{
    int baseH;
    private void Start()
    {
        baseH = health;
    }
    public override void MyMove()
    {
        health = baseH;
        if (count < nextFrame)
        {
            dY = Random.Range(2, 4);
            //gameObject.GetComponent<Rigidbody2D>().velocity = speed * Time.deltaTime * new Vector3(dX, dY, 0);
        }
        else
        {
            count = 0;
            nextFrame = (int)(Random.value * 500);
            gameObject.GetComponent<Animator>().SetTrigger("Jump");
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, dY);
        }
        count++;
    }

    public override void NearPlayer()
    {
        health = baseH;
    }
}
