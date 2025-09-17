using UnityEngine;
using UnityEngine.UI;

public class Cards_Hand : MonoBehaviour
{
    public GameObject cardImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(cardImage, transform.position, Quaternion.identity);
        }
    }
}
