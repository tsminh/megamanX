using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_run : Enemy
{
    
    void Start()
    {
        gameObject.GetComponent<Animator>().SetBool("isRunning", true);
    }
    public override void MyMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    public override void NearPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
