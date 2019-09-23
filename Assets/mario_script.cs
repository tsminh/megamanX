using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mario_script : MonoBehaviour
{
    Animator m_animator;
    bool isRunning;
    int countFrame = 0;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        m_animator = gameObject.GetComponent<Animator>();
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            transform.position += move * speed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0, 180, 0);
            isRunning = true;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            transform.position += move * speed * Time.deltaTime;
            
            transform.eulerAngles = new Vector3(0, 0, 0);
            isRunning = true;
        } else if (Input.GetKey(KeyCode.C)) {
            m_animator.SetTrigger("isShoot");
        } else {
            isRunning = false;
        }
        m_animator.SetBool("isRunning", isRunning);
    }
}
