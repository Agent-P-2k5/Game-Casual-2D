using UnityEngine;
using System.Collections.Generic;

public class Puzzles : MonoBehaviour
{
    public NumberBoxs boxPrefab;
    public NumberBoxs[,] boxes = new NumberBoxs[3, 3];
    public Sprite[] numberSprites;

    void Start()
    {
        Init();
    }

    void Init()
    {
        // Tạo danh sách số từ 1 -> 9
        List<int> numbers = new List<int>();
        for (int i = 1; i <= 9; i++)
            numbers.Add(i);

        // Shuffle ngẫu nhiên
        for (int i = 0; i < numbers.Count; i++)
        {
            int rand = Random.Range(i, numbers.Count);
            int temp = numbers[i];
            numbers[i] = numbers[rand];
            numbers[rand] = temp;
        }

        // Sinh các ô theo số random
        int n = 0;
        for (int y = 2; y >= 0; y--)
        {
            for (int x = 0; x < 3; x++)
            {
                NumberBoxs box = Instantiate(boxPrefab, new Vector2(x, y), Quaternion.identity);

                int value = numbers[n];
                if (value == 9) // 9 là ô trống
                    box.Init(x, y, 9, null, ClickToSwam);
                else
                    box.Init(x, y, value, numberSprites[value - 1], ClickToSwam);

                boxes[x, y] = box;
                n++;
            }
        }
    }

    void ClickToSwam(int x, int y)
    {
        int dx = GetDx(x, y);
        int dy = GetDy(x, y);

        if (dx == 0 && dy == 0) return;

        var box = boxes[x, y];
        var emptyBox = boxes[x + dx, y + dy];

        boxes[x, y] = emptyBox;
        boxes[x + dx, y + dy] = box;

        box.UpdatePos(x + dx, y + dy);
        emptyBox.UpdatePos(x, y);
    }

    int GetDx(int x, int y)
    {
        if (x < 2 && boxes[x + 1, y].isEmpty()) return 1;
        if (x > 0 && boxes[x - 1, y].isEmpty()) return -1;
        return 0;
    }

    int GetDy(int x, int y)
    {
        if (y < 2 && boxes[x, y + 1].isEmpty()) return 1;
        if (y > 0 && boxes[x, y - 1].isEmpty()) return -1;
        return 0;
    }
}
