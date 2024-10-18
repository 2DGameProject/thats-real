using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public GameObject backgroundOverlay; 
    public GameObject puzzleUI;         

    void StartPuzzle()
    {
        // Ativa o escurecimento de fundo
        backgroundOverlay.SetActive(true);

        // Ativa o Puzzle UI (o puzzle centralizado no canvas)
        puzzleUI.SetActive(true);
    }

    void EndPuzzle()
    {
        // Desativa o escurecimento de fundo
        backgroundOverlay.SetActive(false);

        // Desativa o Puzzle UI
        puzzleUI.SetActive(false);
    }
}
