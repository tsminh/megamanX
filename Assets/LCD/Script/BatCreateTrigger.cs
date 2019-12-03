using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCreateTrigger : MonoBehaviour
{
    public GameObject batObject;
    public GameObject player;
    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            GameObject batClone = Instantiate(batObject);
            Destroy(gameObject);
        }
    }
}
