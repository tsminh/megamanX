using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControler : MonoBehaviour
{
    Rigidbody2D _rigid;
    public GameObject character;
    float left = -1;
    Vector3 right = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(character.transform.position.x, character.transform.position.y, -1);
        
        RaycastHit2D hitInfo1 = Physics2D.Raycast(transform.position, Vector2.left, 1.25f, 1<<9);
        Debug.DrawRay(transform.position, Vector2.left * 1.25f, Color.red);
        if (hitInfo1.collider != null) {
            if (left == -1) {
                left = transform.position.x;
            } else {
                transform.position = new Vector3(left, transform.position.y, transform.position.z);
            } 
            // Debug.Log("Hit: " + hitInfo1.collider.name);
        } else {
            if (character.transform.position.x >= left) {
                transform.position = new Vector3(character.transform.position.x, character.transform.position.y, -1);
            }
        }
        // _rigid.velocity = new Vector2(horizontalInput, _rigid.velocity.y);
    }
}
