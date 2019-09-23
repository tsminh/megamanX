using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beescript : MonoBehaviour
{
    int count = 0;
    float speed = .5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count += 1;
        Vector3 move = new Vector3(1, 0, 0);
        if (count < 100) {
            transform.position += move * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (count < 200) {
            transform.position -= move * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else count = 0;
    }
}
