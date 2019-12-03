using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public AudioClip dieSound;
    AudioSource die;
    // Start is called before the first frame update
    public void PlayDeadSound()
    {
        die.Play();
    }
    void Start()
    {
        die = gameObject.AddComponent<AudioSource>();
        die.clip = dieSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
