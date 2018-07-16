using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    public float scoreMultiplier;
    private bool increaseScore;
    private bool deactivateSpikes;
    private bool powerUpActive;
    private float powerUpTime;
    private ScoreManager scoreManager;
    private PlatformGenerator platformGenerator;
    private float baseScorePerSecond;
    private int baseSpikeGenThreshold;
    private ObjectDestroyer[] spikes;
    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        scoreManager = FindObjectOfType<ScoreManager>();
        platformGenerator = FindObjectOfType<PlatformGenerator>();
        gameManager = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
		if (powerUpActive) {
            powerUpTime -= Time.deltaTime;

            if (scoreManager.isDead)
                powerUpTime = 0;

            if (increaseScore)
            {
                scoreManager.scorePerSecond = baseScorePerSecond * scoreMultiplier;
                scoreManager.increaseScore = true;
            }

            if (deactivateSpikes)
            {
                spikes = FindObjectsOfType<ObjectDestroyer>();
                for (int i = 0; i < spikes.Length; i++)
                    if (spikes[i].gameObject.name.Contains("Spikes")) 
                        spikes[i].gameObject.SetActive(false);
                platformGenerator.spikeGenThreshold = -1;
            }
                

            if (powerUpTime <= 0) {
                scoreManager.increaseScore = false;
                scoreManager.scorePerSecond = baseScorePerSecond;
                platformGenerator.spikeGenThreshold = baseSpikeGenThreshold;
                powerUpActive = false;
            }
        }
	}

    public void ActivatePowerUp(bool increase, bool deactivate, float time) {
        // save the old base values to reset when power up expires
        increaseScore = increase;
        deactivateSpikes = deactivate;
        powerUpTime = time;

        // prevent score from getting multiplied indefinitely
        // when picking multiple powerups in the same time frame
        if (!powerUpActive) {
            baseScorePerSecond = scoreManager.scorePerSecond;
            baseSpikeGenThreshold = platformGenerator.spikeGenThreshold;
        }
        powerUpActive = true;
    }
}
