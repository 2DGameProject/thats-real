using UnityEngine;

public class GameStateClassroom : MonoBehaviour
{
    // Variável estática para controlar se o jogador pegou o objeto chave
    public static bool hasKeyItem = false;

    // Método para definir que o jogador pegou o objeto
    public static void PickUpKeyItem()
    {
        hasKeyItem = true;
        Debug.Log("Objeto chave adquirido! Agora você pode interagir com o puzzle.");
    }

    // Método para verificar se o jogador possui o objeto chave
    public static bool PlayerHasKeyItem()
    {
        return hasKeyItem;
    }
}
