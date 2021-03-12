using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public int score { get; set; } = 0;
    public int highScore = 0;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI highScoreUI;
    public TextMeshProUGUI timerUI;
    public GameObject startScreen;
    public AudioSource music;

    static Game instance = null;

    float timer = 90.0f;

    public enum eState
    {
        Title,
        StartGame,
        Game,
        GameOver
    }

    public eState State { get; set; } = eState.Title;

    private void Update()
    {
        switch (State)
        {
            case eState.Title:
                startScreen.SetActive(true);
                break;
            case eState.StartGame:
                timer = 10;
                score = 0;
                scoreUI.text = "0000";
                music.Play();
                startScreen.SetActive(false);
                State = eState.Game;
                break;
            case eState.Game:
                timer -= Time.deltaTime;
                timerUI.text = string.Format("{0:D2}", (int)timer);
                if (timer <= 0)
                {
                    music.Stop();
                    timer = 5;
                    State = eState.GameOver;
                }
                break;
            case eState.GameOver:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    if (score > highScore)
                    {
                        highScoreUI.text = string.Format("{0:D4}", score);
                        highScore = score;
                    }
                    score = 0;
                    State = eState.Title;
                }
                break;
            default:
                break;
        }

        
    }

    private void Awake()
    {
        instance = this;
    }

    public static Game Instance
    {
        get
        {
            return instance;
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        scoreUI.text = string.Format("{0:D4}", score);
    }

    public void StartGame()
    {
        State = eState.StartGame;
    }
}
