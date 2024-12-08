using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int gamePlayed = 1;


    public bool isRewared = false;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    public void AddScore()
    {
        //if (isRewared == true)
        //{
        //    score = score + 2;
        //}
        //else 
        //{
        //    score++;
        //}

        score++;
    }



}