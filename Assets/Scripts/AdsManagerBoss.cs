using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManagerBoss : MonoBehaviour
{
    public InitializeAds initializeAds;
    public RewardedAdsBoss rewardedAds;

    public static AdsManagerBoss Instance { get; private set; }



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);


        rewardedAds.LoadRewardedAd();
    }
}