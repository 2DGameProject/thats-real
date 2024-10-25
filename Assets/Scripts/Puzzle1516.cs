using System.Collections;
using UnityEngine;

public class Puzzle1516 : MonoBehaviour
{
    public NumberBox boxPrefab; // Prefab de cada peça do puzzle
    public NumberBox[,] boxes = new NumberBox[3, 3]; // Ajustado para 3x3
    public Sprite[] sprites; // Deve conter 8 sprites (1 a 8)
    public GameObject backgroundOverlay; // Fundo escuro
    public GameObject puzzleUI; // Painel do Puzzle
    public GameObject closeButton; // Botão de Fechar
    public GameObject player; // Jogador para desativar o movimento

    private bool isActive = false; // Controla o estado do puzzle (ativo ou inativo)
    private Vector2 lastMove; // Guarda o último movimento para evitar repetições no shuffle

    void Start()
    {
        // Desativa o fundo escuro e o botão de fechar no início do jogo
        backgroundOverlay.SetActive(false);
        puzzleUI.SetActive(false); // Desativa o puzzle UI no início também
        closeButton.SetActive(false);
    }

    public void StartPuzzle()
    {
        if (isActive) return; // Se o puzzle já está ativo, não faz nada

        backgroundOverlay.SetActive(true); // Mostra o fundo escuro
        puzzleUI.SetActive(true); // Mostra o UI do Puzzle
        closeButton.SetActive(true); // Ativa o botão de fechar

        player.GetComponent<NewBehaviourScript>().enabled = false; // Desativa o movimento do jogador
        Init(); // Inicializa o puzzle
        Shuffle(); // Embaralha o puzzle

        isActive = true; // Marca o puzzle como ativo
    }

    // Inicializa as peças do puzzle
    void Init()
    {
        int n = 0;
        for (int y = 2; y >= 0; y--) // Ajustado para 3x3
        {
            for (int x = 0; x < 3; x++) // Ajustado para 3x3
            {
                NumberBox box = Instantiate(boxPrefab, new Vector2(x, y), Quaternion.identity);
                box.Init(x, y, n + 1, sprites[n], ClickToSwap); // Inicializa a peça
                boxes[x, y] = box; // Armazena a peça na matriz
                n++;
            }
        }
    }

    // Lógica de movimento ao clicar na peça
    void ClickToSwap(int x, int y)
    {
        int dx = getDx(x, y);
        int dy = getDy(x, y);
        if (dx != 0 || dy != 0) // Se há um movimento possível
        {
            Swap(x, y, dx, dy);
        }
    }

    // Troca as peças de lugar
    void Swap(int x, int y, int dx, int dy)
    {
        var from = boxes[x, y]; // Peça de origem
        var target = boxes[x + dx, y + dy]; // Peça alvo

        // Troca as peças na matriz
        boxes[x, y] = target;
        boxes[x + dx, y + dy] = from;

        // Atualiza as posições das peças
        from.UpdatePos(x + dx, y + dy);
        target.UpdatePos(x, y);
    }

    // Verifica se a peça pode se mover no eixo X
    int getDx(int x, int y)
    {
        if (x < 2 && boxes[x + 1, y].IsEmpty()) // Checa se a peça à direita está vazia
            return 1;
        if (x > 0 && boxes[x - 1, y].IsEmpty()) // Checa se a peça à esquerda está vazia
            return -1;
        return 0;
    }

    // Verifica se a peça pode se mover no eixo Y
    int getDy(int x, int y)
    {
        if (y < 2 && boxes[x, y + 1].IsEmpty()) // Checa se a peça acima está vazia
            return 1;
        if (y > 0 && boxes[x, y - 1].IsEmpty()) // Checa se a peça abaixo está vazia
            return -1;
        return 0;
    }

    // Embaralha o puzzle
    void Shuffle()
    {
        for (int i = 0; i < 3; i++) // Ajustado para 3x3
        {
            for (int j = 0; j < 3; j++) // Ajustado para 3x3
            {
                if (boxes[i, j].IsEmpty()) // Encontra a peça vazia
                {
                    Vector2 pos = getValidMove(i, j);
                    Swap(i, j, (int)(pos.x), (int)(pos.y)); // Realiza a troca
                }
            }
        }
    }

    // Calcula um movimento válido durante o shuffle
    Vector2 getValidMove(int x, int y)
    {
        Vector2 pos = new Vector2();
        do
        {
            int n = Random.Range(0, 4); // Gera uma direção aleatória
            if (n == 0)
                pos = Vector2.left;
            else if (n == 1)
                pos = Vector2.right;
            else if (n == 2)
                pos = Vector2.up;
            else
                pos = Vector2.down;
        } while (!(isValidRange(x + (int)pos.x) && isValidRange(y + (int)pos.y)) || isRepeatMove(pos));

        lastMove = pos; // Atualiza o último movimento
        return pos;
    }

    // Verifica se a posição está dentro do grid 3x3
    bool isValidRange(int n)
    {
        return n >= 0 && n <= 2; // Ajustado para 3x3
    }

    // Impede movimentos repetidos (de ida e volta) no shuffle
    bool isRepeatMove(Vector2 pos)
    {
        return pos * -1 == lastMove; // Evita refazer o último movimento
    }

    // Método para encerrar o puzzle e reativar o jogador
    public void EndPuzzle()
    {
        // Desativa o fundo escuro e o puzzle UI
        backgroundOverlay.SetActive(false);
        puzzleUI.SetActive(false);
        closeButton.SetActive(false); // Desativa o botão de fechar

        // Reativa o controle do jogador
        player.GetComponent<NewBehaviourScript>().enabled = true;

        isActive = false; // Marca o puzzle como inativo
    }
}
