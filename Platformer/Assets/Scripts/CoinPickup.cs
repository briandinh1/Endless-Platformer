using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {
    
    public int scoreAwarded;
    private ScoreManager scoreManager;
    public AudioSource coinSound;

	// Use this for initialization
	void Start () {
        scoreManager = FindObjectOfType<ScoreManager>();
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            scoreManager.AddScore(scoreAwarded);
            gameObject.SetActive(false);
            if (coinSound.isPlaying) 
                coinSound.Stop();
            coinSound.Play();
        }
    }
}
