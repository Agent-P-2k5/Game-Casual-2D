using UnityEngine;
using UnityEngine.UI;

public class PikachuCard : MonoBehaviour
{
    public Image icon;
    public int id;
    public Vector2Int gridPos; // vị trí trong grid

    private PikachuController manager;

    public void Setup(int cardId, Sprite sprite, PikachuController cardManager, Vector2Int pos)
    {
        id = cardId;
        icon.sprite = sprite;
        manager = cardManager;
        gridPos = pos;

        // Gán sự kiện click 1 lần
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Button>().onClick.AddListener(() => manager.SelectCard(this));
    }
}