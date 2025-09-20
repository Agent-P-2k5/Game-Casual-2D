using UnityEngine;

public class Numbers : MonoBehaviour
{
    private NumberController controller;
    private int myIndex;
    void Start()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        if (col != null)
        {
            // Tự động match collider với sprite
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                col.size = sr.bounds.size; // kích thước collider = sprite
                col.offset = Vector2.zero; // giữ collider ở giữa
            }
        }
    }

    public void Init(NumberController controller, int index)
    {
        this.controller = controller;
        this.myIndex = index;
    }

    void OnMouseDown()
    {
        controller.SelectNumber(myIndex, gameObject);
    }
}
