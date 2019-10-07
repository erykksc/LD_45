using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSource : MonoBehaviour
{
    public AudioSource Track1;
    public AudioClip Track2;
    public int TrackSelector;
    public int TrackHistory;

    // Start is called before the first frame update
    void Start()
    {
        TrackSelector = Random.Range(0, 2);
        if(TrackSelector == 0)
        {
            Track1.Play();
            TrackHistory = 1;
        }
        else if (TrackSelector == 1)
        {
            Track2.Play();
            TrackHistory = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Track1.isPlaying == false && Track2.isPlaying == false)
        {
            TrackSelector = Random.Range(0, 2);
            if (TrackSelector == 0 && TrackHistory != 1)
            {
                Track1.Play();
                TrackHistory = 1;
            }
            else if (TrackSelector == 1 && TrackHistory != 2)
            {
                Track2.Play();
                TrackHistory = 2;
            }
        }
    }
}
