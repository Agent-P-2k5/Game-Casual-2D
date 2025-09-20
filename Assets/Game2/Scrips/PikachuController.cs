using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PikachuController : MonoBehaviour
{
    [Header("Grid Settings")]
    public int Width = 6;
    public int Height = 6;
    public float Spacing = 0.2f;

    [Header("Card Size")]
    public float Long = 1f;
    public float Wide = 1.5f;
    public Vector2 FrameOffset;

    public Sprite[] items;
    public int paintedIndex;

    public GameObject Items;

    private List<Sprite> itemList = new List<Sprite>();

    void Start()
    {
     
    }

    private void PrepareSprites()
    {
        itemList = new List<Sprite>();

        List<Sprite> available = new List<Sprite>(items);
        for (int i = 0; i < paintedIndex; i++)
        {
            int randomIndex = Random.Range(0, available.Count);
            Sprite chosen = available[randomIndex];
            available.RemoveAt(randomIndex);

            itemList.Add(chosen);
            itemList.Add(chosen);
        }

        ShuffleSprites(itemList);
    }

    private void ShuffleSprites(List<Sprite> sprites)
    {
        for (int i = sprites.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Sprite temp = sprites[i];
            sprites[i] = sprites[randomIndex];
            sprites[randomIndex] = temp;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int X = 0; X < Width; X++)
        {
            for (int Y = 0; Y < Height; Y++)
            {
                float posX = X * (Long + Spacing) + Long / 2f + FrameOffset.x;
                float posY = Y * (Wide + Spacing) + Wide / 2f + FrameOffset.y;
                Vector3 pos = new Vector3(posX, posY, 0);

                Gizmos.DrawWireCube(pos, new Vector3(Long, Wide, 0));
            }
        }
    }

}
