using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Video;

public class VideoVisibilityHandler : MonoBehaviour
{
    public VideoPlayer video;
    public float maxDistanceToPlay = 10f;
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        video.Pause();
        maxDistanceToPlay = 10f;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, mainCam.transform.position);

        if (dist <= maxDistanceToPlay)
        {
            if (!video.isPlaying)
                video.Play();
        }
        else
        {
            if (video.isPlaying)
                video.Pause();
        }
    }
}
