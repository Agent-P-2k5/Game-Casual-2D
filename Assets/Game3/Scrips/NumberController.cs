using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class NumberController : MonoBehaviour
{
    [Header("Config")]
    public GameObject numberPrefab;         // Prefab của số
    public Sprite[] numberSprites;          // Sprite từ 0 -> 10
    public Transform parentContainer;       // Container chứa các số
    public float minX = -5, maxX = 5;       // Random vị trí X
    public float minY = -3, maxY = 3;       // Random vị trí Y
    public float minScale = 0.5f, maxScale = 1.5f; // Random scale

    [Header("Gameplay")]
    public float timeLimit = 20f;           // Thời gian giới hạn
    private float timer;
    private int currentTarget = 0;          // Số cần chọn hiện tại
    private List<GameObject> spawnedNumbers = new List<GameObject>();

    [Header("UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI statusText;

    void Start()
    {
        timer = timeLimit;
        SpawnNumbers();
        UpdateUI();
    }

    void Update()
    {
        // Đếm ngược thời gian
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(timer).ToString();

            if (timer <= 0)
            {
                GameOver(false);
            }
        }
    }

    void SpawnNumbers()
    {
        for (int i = 0; i < numberSprites.Length; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            GameObject number = Instantiate(numberPrefab, randomPos, Quaternion.identity, parentContainer);

            // random scale
            float scale = Random.Range(minScale, maxScale);
            number.transform.localScale = new Vector3(scale, scale, 1);

            // gán sprite và dữ liệu
            number.GetComponent<SpriteRenderer>().sprite = numberSprites[i];
            number.GetComponent<Numbers>().Init(this, i);

            spawnedNumbers.Add(number);
        }
    }

    public void SelectNumber(int clickedIndex, GameObject clickedObj)
    {
        if (clickedIndex == currentTarget)
        {
            Destroy(clickedObj); // số biến mất
            currentTarget++;

            if (currentTarget >= numberSprites.Length)
            {
                GameOver(true); // thắng
            }
            else
            {
                statusText.text = "Find number: " + currentTarget;
            }
        }
        else
        {
            Debug.Log("Sai rồi! Cần chọn số: " + currentTarget);
        }
    }

    void GameOver(bool win)
    {
        if (win)
            statusText.text = "🎉 You Win!";
        else
            statusText.text = "⏰ Time's Up!";

        // Xóa tất cả số còn lại
        foreach (var n in spawnedNumbers)
        {
            if (n != null) Destroy(n);
        }
    }

    void UpdateUI()
    {
        statusText.text = "Find number: 0";
        timerText.text = "Time: " + timeLimit;
    }
}
