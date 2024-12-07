using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButtons : MonoBehaviour
{

    public void Multiplier()
    {
        AdsManager.Instance.rewardedAds.ShowRewardedAd();
    }
}