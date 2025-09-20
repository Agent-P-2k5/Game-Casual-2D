using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Cards_Hand : MonoBehaviour
{
    public Cards CardPrefab;
    public Transform gridTransform;
    public Sprite[] Sprites;

    public int NumberCard;

    private List<Sprite> spritePairs;

    Cards FirstSelected;
    Cards SecondSelected;

    private int matchedPairs = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrepareSprites();
        CreateCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PrepareSprites()
    {
        spritePairs = new List<Sprite>();

        // B1: Random chọn 10 lá trong 54 lá
        List<Sprite> available = new List<Sprite>(Sprites);
        for (int i = 0; i < NumberCard; i++) // NumberCard = 10
        {
            int randomIndex = Random.Range(0, available.Count);
            Sprite chosen = available[randomIndex];
            available.RemoveAt(randomIndex);

            // B2: Nhân đôi lá đó (cặp bài)
            spritePairs.Add(chosen);
            spritePairs.Add(chosen);
        }

        // B3: Xáo trộn
        ShuffleSprites(spritePairs);
    }


    private void CreateCards()
    {
        for(int i = 0; i < spritePairs.Count; i++)
        {
            Cards newCard = Instantiate(CardPrefab, gridTransform);
            newCard.SeticonSprite(spritePairs[i]);
            newCard.controller = this;
        }
    }

    public void SetSelected(Cards card)
    {
        if(card.IsSelected == false)
        {
            card.Show();

            if(FirstSelected == null)
            {
                FirstSelected = card;
                return;
            }
            else
            {
                SecondSelected = card;
                StartCoroutine(CheckMatching(FirstSelected, SecondSelected));
                FirstSelected = null;
                SecondSelected = null;
            }
        }
    }

    IEnumerator CheckMatching(Cards a, Cards b)
    {
        yield return new WaitForSeconds(0.5f);
        if (a.IconSprite == b.IconSprite)
        {
            matchedPairs++;

            if (matchedPairs >= NumberCard)
            {
                Debug.Log(" thang");
                WinGame();
            }
        }
        else
        {
            a.Hide();
            b.Hide();
        }
    }

    private void ShuffleSprites(List<Sprite> sprites)
    {
        for (int i = sprites.Count -1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Sprite temp = sprites[i];
            sprites[i] = sprites[randomIndex];
            sprites[randomIndex] = temp;
        }
    }

    private void WinGame()
    {
        Debug.Log("You Win!");
    }

}
