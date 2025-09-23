using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PikachuController : MonoBehaviour
{
    public int rows = 5;  // số hàng (dọc)
    public int cols = 4;  // số cột (ngang)
    public PikachuCard cardPrefab;
    public Transform gridContainer;
    public Sprite[] sprites;

    private PikachuCard firstSelected;
    private PikachuCard secondSelected;
    private List<PikachuCard> allCards = new List<PikachuCard>();
    private int[,] grid; // 0 = trống, 1 = có thẻ

    void Start()
    {
        InitCards();
    }

    void InitCards()
    {
        grid = new int[rows, cols];

        List<int> cardIds = new List<int>();
        int pairs = (rows * cols) / 2;
        for (int i = 0; i < pairs; i++)
        {
            cardIds.Add(i);
            cardIds.Add(i);
        }

        // shuffle
        for (int i = 0; i < cardIds.Count; i++)
        {
            int rand = Random.Range(0, cardIds.Count);
            int tmp = cardIds[i];
            cardIds[i] = cardIds[rand];
            cardIds[rand] = tmp;
        }

        float cellSize = 100f;
        float spacing = 10f;

        int index = 0;
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int id = cardIds[index++];
                PikachuCard newCard = Instantiate(cardPrefab, gridContainer);

                RectTransform rt = newCard.GetComponent<RectTransform>();
                rt.anchoredPosition = new Vector2(c * (cellSize + spacing), -r * (cellSize + spacing));

                newCard.Setup(id, sprites[id], this, new Vector2Int(r, c));
                allCards.Add(newCard);
                grid[r, c] = 1;
            }
        }
    }

    public void SelectCard(PikachuCard card)
    {
        if (firstSelected == null)
        {
            firstSelected = card;
        }
        else if (secondSelected == null && card != firstSelected)
        {
            secondSelected = card;
            CheckMatch();
        }
    }

    void CheckMatch()
    {
        if (firstSelected.id == secondSelected.id)
        {
            if (CanConnect(firstSelected.gridPos, secondSelected.gridPos))
            {
                grid[firstSelected.gridPos.x, firstSelected.gridPos.y] = 0;
                grid[secondSelected.gridPos.x, secondSelected.gridPos.y] = 0;

                firstSelected.gameObject.SetActive(false);
                secondSelected.gameObject.SetActive(false);

                Debug.Log("ĂN ĐƯỢC!");
            }
            else
            {
                Debug.Log("Không nối được (bị chặn)");
            }
        }

        firstSelected = null;
        secondSelected = null;
    }

    // ========================
    // LUẬT PIKACHU
    // ========================
    bool CanConnect(Vector2Int a, Vector2Int b)
    {
        if (a == b) return false;

        int oldA = grid[a.x, a.y];
        int oldB = grid[b.x, b.y];
        grid[a.x, a.y] = 0;
        grid[b.x, b.y] = 0;

        bool can = CheckStraight(a, b)
                || CheckOneCorner(a, b)
                || CheckTwoCorners(a, b)
                || CheckOutOfBounds(a, b);

        grid[a.x, a.y] = oldA;
        grid[b.x, b.y] = oldB;

        return can;
    }

    bool IsInside(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < rows && pos.y >= 0 && pos.y < cols;
    }


    // đi thẳng
    bool CheckStraight(Vector2Int a, Vector2Int b)
    {
        if (a.x == b.x) // cùng hàng
        {
            int minC = Mathf.Min(a.y, b.y);
            int maxC = Mathf.Max(a.y, b.y);

            if (maxC - minC == 1) return true;

            for (int c = minC + 1; c < maxC; c++)
            {
                Vector2Int pos = new Vector2Int(a.x, c);
                if (IsInside(pos) && grid[pos.x, pos.y] == 1) return false;
            }
            return true;
        }
        else if (a.y == b.y) // cùng cột
        {
            int minR = Mathf.Min(a.x, b.x);
            int maxR = Mathf.Max(a.x, b.x);

            if (maxR - minR == 1) return true;

            for (int r = minR + 1; r < maxR; r++)
            {
                Vector2Int pos = new Vector2Int(r, a.y);
                if (IsInside(pos) && grid[pos.x, pos.y] == 1) return false;
            }
            return true;
        }
        return false;
    }


    // đi qua 1 góc
    bool CheckOneCorner(Vector2Int a, Vector2Int b)
    {
        Vector2Int corner1 = new Vector2Int(a.x, b.y);
        Vector2Int corner2 = new Vector2Int(b.x, a.y);

        if (IsEmptyOrOutside(corner1))
        {
            if (CheckStraight(a, corner1) && CheckStraight(corner1, b)) return true;
        }
        if (IsEmptyOrOutside(corner2))
        {
            if (CheckStraight(a, corner2) && CheckStraight(corner2, b)) return true;
        }

        return false;
    }

    // đi qua 2 góc
    bool CheckTwoCorners(Vector2Int a, Vector2Int b)
    {
        for (int r = 0; r < rows; r++)
        {
            Vector2Int p1 = new Vector2Int(r, a.y);
            Vector2Int p2 = new Vector2Int(r, b.y);

            if (IsEmptyOrOutside(p1) && IsEmptyOrOutside(p2))
            {
                if (CheckStraight(a, p1) && CheckStraight(p1, p2) && CheckStraight(p2, b))
                    return true;
            }
        }

        for (int c = 0; c < cols; c++)
        {
            Vector2Int p1 = new Vector2Int(a.x, c);
            Vector2Int p2 = new Vector2Int(b.x, c);

            if (IsEmptyOrOutside(p1) && IsEmptyOrOutside(p2))
            {
                if (CheckStraight(a, p1) && CheckStraight(p1, p2) && CheckStraight(p2, b))
                    return true;
            }
        }

        return false;
    }

    // đi vòng ngoài biên
    bool CheckOutOfBounds(Vector2Int a, Vector2Int b)
    {
        // vòng theo cột ngoài
        for (int c = -1; c <= cols; c++)
        {
            Vector2Int p1 = new Vector2Int(a.x, c);
            Vector2Int p2 = new Vector2Int(b.x, c);

            if (IsEmptyOrOutside(p1) && IsEmptyOrOutside(p2))
            {
                if (CheckStraight(a, p1) && CheckStraight(p1, p2) && CheckStraight(p2, b))
                    return true;
            }
        }

        // vòng theo hàng ngoài
        for (int r = -1; r <= rows; r++)
        {
            Vector2Int p1 = new Vector2Int(r, a.y);
            Vector2Int p2 = new Vector2Int(r, b.y);

            if (IsEmptyOrOutside(p1) && IsEmptyOrOutside(p2))
            {
                if (CheckStraight(a, p1) && CheckStraight(p1, p2) && CheckStraight(p2, b))
                    return true;
            }
        }

        return false;
    }

    // ngoài biên coi như ô trống
    bool IsEmptyOrOutside(Vector2Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= rows || pos.y >= cols)
            return true;
        return grid[pos.x, pos.y] == 0;
    }
}
