using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public Image cardImage;
    public Sprite HiddenIconSprite;
    public Sprite IconSprite;

    public bool IsSelected;

    public Cards_Hand controller;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        controller.SetSelected(this);
    }

    public void SeticonSprite(Sprite sp)
    {
        IconSprite = sp;
    }

    public void Show()
    {
        cardImage.sprite = IconSprite;
        IsSelected = true;
    }

    public void Hide() 
    {
        cardImage.sprite = HiddenIconSprite;
        IsSelected = false;
    }
}
