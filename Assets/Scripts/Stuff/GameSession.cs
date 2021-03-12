﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GameSession : MonoBehaviour
{
    public int score { get; set; } = 0;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI timerUI;
    public GameObject startScreen;

    float timer = 0;

    static GameSession instance = null;
    public static GameSession Instance
    {
        get
        {
            return instance;
        }
    }




    public enum eState
    {
        StartSession,
        Session,
        EndSession,
        GameOver
    }

    public eState State { get; set; } = eState.StartSession;

    private void Start()
    {
        EventManager.Instance.Subscribe("PlayerDead", OnPlayerDead);
    }

    private void Update()
    {
        switch (State)
        {
            case eState.StartSession:
                score = 0;
                EventManager.Instance.TriggerEvent("StartSession");
                GameController.Instance.transition.StartTransition(Color.clear, 1);
                State = eState.Session;
                break;
            case eState.Session:

                break;
            case eState.EndSession:
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    GameObject gameObject = GameObject.FindGameObjectWithTag("PlayerPackage");
                    Destroy(gameObject);
                    State = eState.StartSession;
                }
                break;
            case eState.GameOver:

                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreUI.text = string.Format("{0:D4}", score);
    }

    public void OnPlayerDead()
    {
        GameController.Instance.transition.StartTransition(Color.black, 1);
        timer = 2;
        State = eState.EndSession;
    }
}
