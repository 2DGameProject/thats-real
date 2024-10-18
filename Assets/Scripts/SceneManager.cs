using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public int sceneIndex;

    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
    }
}