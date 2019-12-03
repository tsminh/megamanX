using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Flying : Enemy
{
    public override void MyMove()
    {
        if (count < nextFrame)
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
    public override void NearPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        gameObject.GetComponent<Rigidbody2D>().velocity = speed * Time.deltaTime * new Vector3(gameObject.GetComponent<Rigidbody2D>().velocity.x, dY, 0);
    }
}
