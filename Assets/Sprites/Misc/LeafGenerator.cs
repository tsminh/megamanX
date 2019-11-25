using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafGenerator : MonoBehaviour
{
    public GameObject leafPrefab;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 40 == 0)
        {
            GameObject gb = Instantiate(leafPrefab, new Vector3(camera.transform.position.x + Random.Range(-2f, 2f), 1.245f, 1), camera.transform.rotation);
        }
    }
}
