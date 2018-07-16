using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public float currentScore;
    public Text currentScoreUI;
    public float highScore;
    public Text highScoreUI;
    public float scorePerSecond;
    public bool isDead; // don't increment score if dead
    public bool increaseScore; // if power up is active to 

	// Use this for initialization
	void Start () {
        /* uncomment to save high score
        if (PlayerPrefs.HasKey("HighScore"))
            highScore = PlayerPrefs.GetFloat("HighScore"); */
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDead) {
            // delta = time taken from the last update loop to the current
            currentScore += scorePerSecond * Time.deltaTime;
            currentScoreUI.text = "" + Mathf.Round(currentScore);

            if (currentScore > highScore) {
                highScore = currentScore;
                // uncomment to save high score
                // PlayerPrefs.SetFloat("HighScore", highScore);
            }

            highScoreUI.text = "High Score: " + Mathf.Round(highScore);
        }
    }

    public void AddScore(int scoreAwarded) {
        if (increaseScore) scoreAwarded <<= 2;
        currentScore += scoreAwarded;
    }
}
