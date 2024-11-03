using System.Collections;
using UnityEngine;

public class Puzzle1516 : MonoBehaviour
{
    public NumberBox boxPrefab;
    public NumberBox[,] boxes = new NumberBox[3, 3]; // Ajustado para 3x3
    public Sprite[] sprites; 
    public GameObject backgroundOverlay;
    public GameObject puzzleUI; 
    public GameObject closeButton;
    public GameObject player;
    public GameObject cabinet; // Referência ao armário na cena
    public Vector2 startPoint = new Vector2(8, 8); 

    private bool isActive = false; 
    private bool isInitialized = false;
    private Vector2 lastMove; 
    private Collider2D playerCollider;

    void Start()
    {
        backgroundOverlay.SetActive(false);
        puzzleUI.SetActive(false);
        closeButton.SetActive(false);


        playerCollider = player.GetComponent<Collider2D>();
    }

    public void StartPuzzle()
    {
        if (isActive) return;

        backgroundOverlay.SetActive(true);
        puzzleUI.SetActive(true);
        closeButton.SetActive(true);


        player.GetComponent<NewBehaviourScript>().enabled = false;


        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        foreach (var box in boxes)
        {
            if (box != null)
            {
                box.gameObject.SetActive(true);
                box.UpdatePos(box.GetX(), box.GetY()); 
            }
        }


        if (!isInitialized)
        {
            Init();
            for (int i = 0;i<100;i++)
                Shuffle(); 
            isInitialized = true;
        }

        isActive = true;
    }

    void Init()
    {
        int n = 0;
        for (int y = 2; y >= 0; y--)
        {
            for (int x = 0; x < 3; x++)
            {
                Vector2 position = new Vector2(x + startPoint.x, y + startPoint.y); 
                NumberBox box = Instantiate(boxPrefab, position, Quaternion.identity);
                box.Init(x, y, n + 1, sprites[n], ClickToSwap);
                boxes[x, y] = box;
                n++;
            }
        }
    }

    public void SetInitialPositions(int[,] initialPositions)
    {
        for (int y = 2; y >= 0; y--)
        {
            for (int x = 0; x < 3; x++)
            {
                int pieceIndex = initialPositions[x, y];
                boxes[x, y].index = pieceIndex;
                boxes[x, y].UpdatePos(x, y);
            }
        }
    }

    void ClickToSwap(int x, int y)
    {
        int dx = getDx(x, y);
        int dy = getDy(x, y);
        if (dx != 0 || dy != 0)
        {
            Swap(x, y, dx, dy);
            if (IsPuzzleSolved())
            {
                OnPuzzleSolved();
            }
        }
    }

    void Swap(int x, int y, int dx, int dy)
    {
        var from = boxes[x, y];
        var target = boxes[x + dx, y + dy];

        boxes[x, y] = target;
        boxes[x + dx, y + dy] = from;

        from.UpdatePos(x + dx, y + dy);
        target.UpdatePos(x, y);
    }

    int getDx(int x, int y)
    {
        if (x < 2 && boxes[x + 1, y].IsEmpty())
            return 1;
        if (x > 0 && boxes[x - 1, y].IsEmpty())
            return -1;
        return 0;
    }

    int getDy(int x, int y)
    {
        if (y < 2 && boxes[x, y + 1].IsEmpty())
            return 1;
        if (y > 0 && boxes[x, y - 1].IsEmpty())
            return -1;
        return 0;
    }

    void Shuffle()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (boxes[i, j].IsEmpty())
                {
                    Vector2 pos = getValidMove(i, j);
                    Swap(i, j, (int)(pos.x), (int)(pos.y));
                }
            }
        }
    }

    Vector2 getValidMove(int x, int y)
    {
        Vector2 pos = new Vector2();
        do
        {
            int n = Random.Range(0, 4);
            if (n == 0)
                pos = Vector2.left;
            else if (n == 1)
                pos = Vector2.right;
            else if (n == 2)
                pos = Vector2.up;
            else
                pos = Vector2.down;
        } while (!(isValidRange(x + (int)pos.x) && isValidRange(y + (int)pos.y)) || isRepeatMove(pos));

        lastMove = pos;
        return pos;
    }

    bool isValidRange(int n)
    {
        return n >= 0 && n <= 2;
    }

    bool isRepeatMove(Vector2 pos)
    {
        return pos * -1 == lastMove;
    }

    bool IsPuzzleSolved()
    {
        int n = 1;
        for (int y = 2; y >= 0; y--)
        {
            for (int x = 0; x < 3; x++)
            {
                if (boxes[x, y].index != n) return false;
                n++;
                if (n > 8) return true;
            }
        }
        return true;
    }

    void OnPuzzleSolved()
    {

        // Traz o armário para frente
        GameStateClassroom.SetCabinetAvailable();
        GameObject cabinet = GameObject.FindWithTag("Armario");
        if (cabinet != null)
        {
            SpriteRenderer renderer = cabinet.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.sortingOrder = 3; // Ajuste o sorting order conforme necessário
            }
        }

        EndPuzzle();
    }

    public void EndPuzzle()
    {
        backgroundOverlay.SetActive(false);
        puzzleUI.SetActive(false);
        closeButton.SetActive(false);

        foreach (var box in boxes)
        {
            if (box != null)
            {
                box.gameObject.SetActive(false);
            }
        }
        player.GetComponent<NewBehaviourScript>().enabled = true;


        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }

        isActive = false;
    }
}
