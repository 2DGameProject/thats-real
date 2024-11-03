using UnityEngine;

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
        isCabinetAvailable = true;
        Debug.Log("O armário está disponível para interação.");
    }


    public static bool PlayerHasKeyItem()
    {
        return hasKeyItem;
    }
}
