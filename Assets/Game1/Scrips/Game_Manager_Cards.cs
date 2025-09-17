using UnityEngine;

public class Game_Manager_Cards : MonoBehaviour
{
    public float Width;
    public float Height;
    public float Spacing;

    public float Long;
    public float Wide;

    public Vector2 FrameOffset;

    public GameObject CardPrefab;
    public float Cards = 54f;

    public float QuantityCards = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Random()
    {
        for (int i = 0; i <= Cards; i++)
        {
            //float c = UnityEngine.Random.Range(20, Cards);
        }
    }

    public void Spamner()
    {
        for(int i = 0; i <= QuantityCards; i++)
        {
            //float posX = Random.Range(0, Width) * (Long + Spacing) + Long / 2f + FrameOffset.x;
            //float posY = Random.Range(0, Height) * (Wide + Spacing) + Wide / 2f + FrameOffset.y;
            //Vector3 pos = new Vector3(posX, posY, 0);
            //Instantiate(CardPrefab, pos, Quaternion.identity);

        }
    }

    public void OnDrawGizmosSelected()
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
