using System;
using System.Collections;
using UnityEngine;

public class NumberBox : MonoBehaviour
{
    public int index = 0;
    int x = 10;
    int y = 10;
    public int GetX() => x;
    public int GetY() => y;


    private Action<int, int> swapFunc = null;

    public void Init(int i, int j, int index, Sprite sprite, Action<int, int> swapFunc)
    {
        this.index = index;
        this.GetComponent<SpriteRenderer>().sprite = sprite;
        UpdatePos(i, j);
        this.swapFunc = swapFunc;
    }

    public void UpdatePos(int i, int j)
    {
        x = i;
        y = j;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float elapsedTime = 0;
        float duration = 0.1f;
        Vector2 start = this.gameObject.transform.localPosition;

        // Ajuste o deslocamento para +8 no eixo X e +8 no eixo Y
        Vector2 end = new Vector2(x + 8, y + 6);

        while (elapsedTime < duration)
        {
            this.gameObject.transform.localPosition = Vector2.Lerp(start, end, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.gameObject.transform.localPosition = end;
    }


    public bool IsEmpty()
    {
        return index == 9;
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && swapFunc != null)
        {
            swapFunc(x, y);
        }
    }
}
