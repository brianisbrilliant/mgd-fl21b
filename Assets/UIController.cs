using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int score = 0;

    void Start() {
        score = PlayerPrefs.GetInt("Score", 0);
    }

    public void AddScore(int amount = 1) {
        score += amount;
        scoreText.text = "Score: " + score.ToString();
        PlayerPrefs.SetInt("Score", score);
    }    
}
