using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManagerOffice : MonoBehaviour
{
    public InitializeAds initializeAds;
    public RewardedAdsOffice rewardedAds;

    public static AdsManagerOffice Instance { get; private set; }



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