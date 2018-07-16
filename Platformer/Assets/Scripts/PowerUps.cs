using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

    public bool increaseScore;
    public bool deactivateSpikes;
    public float powerUpLength;
    private PowerUpManager powerUpManager;
    public Sprite[] powerUpSprites;

	// Use this for initialization
	void Start () {
        powerUpManager = FindObjectOfType<PowerUpManager>();
	}

    // run everytime object is active
    private void Awake()
    {
        int powerUpSelected = Random.Range(0, 100) <= 65 ? 0 : 1;
        if (powerUpSelected == 0) {
            increaseScore = true;
            powerUpLength = 3;
        }
        else {
            deactivateSpikes = true;
            powerUpLength = 5;
        } 
        GetComponent<SpriteRenderer>().sprite = powerUpSprites[powerUpSelected];
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "Player") 
            powerUpManager.ActivatePowerUp(increaseScore, deactivateSpikes, powerUpLength);
        gameObject.SetActive(false);
    }
}
