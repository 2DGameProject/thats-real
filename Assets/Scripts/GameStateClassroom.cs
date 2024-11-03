using UnityEngine;

public class GameStateClassroom : MonoBehaviour
{
    // Vari�vel est�tica para controlar se o jogador pegou o objeto chave
    public static bool hasKeyItem = false;

    // M�todo para definir que o jogador pegou o objeto
    public static void PickUpKeyItem()
    {
        hasKeyItem = true;
        Debug.Log("Objeto chave adquirido! Agora voc� pode interagir com o puzzle.");
    }

    // M�todo para verificar se o jogador possui o objeto chave
    public static bool PlayerHasKeyItem()
    {
        return hasKeyItem;
    }
}
