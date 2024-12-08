using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStateClassroom : MonoBehaviour
{

    public static bool hasKeyItem = false;
    public static bool isCabinetAvailable = false; 


    public static void PickUpKeyItem()
    {
        hasKeyItem = true;

    }

    public static void SetCabinetAvailable()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2, LoadSceneMode.Single);
        isCabinetAvailable = true;
        Debug.Log("O arm�rio est� dispon�vel para intera��o.");
    }


    public static bool PlayerHasKeyItem()
    {
        return hasKeyItem;
    }
}
