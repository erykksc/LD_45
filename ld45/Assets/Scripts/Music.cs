using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip Track1;
    public AudioClip Track2;
    public AudioClip Track3;
    public AudioClip Track4;

    public AudioSource Source;
    private int TrackSelector;
    private int TrackHistory;

    // Start is called before the first frame update
    void Start()
    {
        Source = gameObject.GetComponent<AudioSource>();
        //Track1 = GetComponent<AudioSource>();
        //Track2 = GetComponent<AudioSource>();
        

        TrackSelector = Random.Range(0, 4);
        if (TrackSelector == 0)
        {
            Source.clip = Track1;
            Source.Play();
            TrackHistory = 1;
        }
        else if (TrackSelector == 1)
        {
            Source.clip = Track2;
            Source.Play();
            TrackHistory = 2;
        }
        else if (TrackSelector == 2)
        {
            Source.clip = Track3;
            Source.Play();
            TrackHistory = 3;
        }
        else if (TrackSelector == 3)
        {
            Source.clip = Track4;
            Source.Play();
            TrackHistory = 4;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Source.isPlaying == false)
        {
            TrackSelector = Random.Range(0, 4);
            if (TrackSelector == 0 && TrackHistory != 1)
            {
                Source.clip = Track1;
                Source.Play();
                TrackHistory = 1;
            }
            else if (TrackSelector == 1 && TrackHistory != 2)
            {
                Source.clip = Track2;
                Source.Play();
                TrackHistory = 2;
            }
            else if (TrackSelector == 2 && TrackHistory != 3)
            {
                Source.clip = Track3;
                Source.Play();
                TrackHistory = 3;
            }
            else if (TrackSelector == 3 && TrackHistory != 4)
            {
                Source.clip = Track4;
                Source.Play();
                TrackHistory = 4;
            }
        }
    }
}
