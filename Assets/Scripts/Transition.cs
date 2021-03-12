﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public RawImage image;
    public float time;
    public Color color;
    public bool startOnAwake;

    public bool IsDone { get; set; }
    Color startColor;

    void Start()
    {
        if(startOnAwake)
        {
            StartTransition(color, time);
        }
    }

    public void StartTransition(Color color, float time)
    {
        this.color = color;
        this.time = time;

        startColor = image.color;
        StartCoroutine(TransitionRoutine(time));
    }

    IEnumerator TransitionRoutine(float timer)
    {
        IsDone = false;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            float t = 1.0f - (timer / time); // 0 -> 1
            image.color = Color.Lerp(startColor, color, t);
            yield return null;
        }

        IsDone = true;

        yield return null;
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Wait 1");
        yield return new WaitForSeconds(time);
        Debug.Log("Wait 2");
        yield return null;
    }

    IEnumerator Timer(float time)
    {
        Debug.Log("Start");
        while(time > 0)
        {
            time -= Time.deltaTime;
            Debug.Log(time);
            yield return null;
        }
    }
}
