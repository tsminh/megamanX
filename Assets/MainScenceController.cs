using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScenceController : MonoBehaviour
{
    public GameObject overlayVideo;
    public UnityEngine.Video.VideoPlayer videoTrailer;
    public GameObject uiElements;
    // Start is called before the first frame update
    void Start()
    {
        uiElements.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        if (videoTrailer.isPaused || Input.GetKeyDown(KeyCode.S)) {
            overlayVideo.SetActive(false);
            uiElements.SetActive(true);
        }
    }
}
