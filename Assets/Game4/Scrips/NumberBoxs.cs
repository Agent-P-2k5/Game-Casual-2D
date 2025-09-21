using System;
using UnityEngine;

public class NumberBoxs : MonoBehaviour
{
    public int index = 0;
    private int x = 0;
    private int y = 0;
    private Action<int, int> onClick = null;

    public void Init(int i, int j, int index, Sprite sprite, Action<int, int> onClick)
    {
        this.index = index;
        var renderer = this.gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        UpdatePos(i, j);
        this.onClick = onClick;
    }

    public void UpdatePos(int i, int j)
    {
        x = i;
        y = j;
        this.transform.localPosition = new Vector2(i, j);
    }

    public bool isEmpty()
    {
        return index == 9;
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && onClick != null)
            onClick(x, y);
    }
}
