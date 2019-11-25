using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControler : MonoBehaviour
{
    Rigidbody2D _rigid;
    public float offsetY;
    public GameObject character;
    float left = -1;
    float top = 0.362f;
    Vector3 right = Vector3.zero;
    float height;
    float width;
    void Start()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        Camera cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(character.transform.position.x, character.transform.position.y, -1);

        float posX = 0;
        float posY = 0;
        posY = transform.position.y - offsetY;
        RaycastHit2D hitInfo1 = Physics2D.Raycast(transform.position, Vector2.left, width/2, 1<<9);
        // Debug.DrawRay(transform.position, Vector2.left * width / 2, Color.red);
        if (hitInfo1.collider != null) {
            if (left == -1) {
                left = transform.position.x;
                posX = left;
            } else {
                posX = left;
            } 
            // Debug.Log("Hit: " + hitInfo1.collider.name);
        } else {
            if (character.transform.position.x >= left) {
                posX = character.transform.position.x;
            }
        }


        RaycastHit2D hitInfo2 = Physics2D.Raycast(transform.position - new Vector3(0, offsetY, 0), Vector2.up, height/ 2, 1 << 9);
        Debug.DrawRay(transform.position - new Vector3(0,offsetY, 0), Vector2.up * height / 2, Color.red);
        if (hitInfo2.collider != null)
        {
            posY = top;
        }
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
