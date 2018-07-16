using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform platformGenerator;
    private Vector3 platformStart;
    public PlayerController player;
    private Vector3 playerStart;
    private ObjectDestroyer[] platforms;
    private ScoreManager scoreManager;
    public DeathMenu deathMenu;

	// Use this for initialization
	void Start () {
        platformStart = platformGenerator.position;
        playerStart = player.transform.position;
        scoreManager = FindObjectOfType<ScoreManager>();
	}

    public void RestartGame() {
        // StartCoroutine("RestartGameCo");
        scoreManager.isDead = true;
        player.gameObject.SetActive(false); // make player disappear
        deathMenu.gameObject.SetActive(true);
    }

    public void ResetState() {
        deathMenu.gameObject.SetActive(false);

        // reset all platforms
        platforms = FindObjectsOfType<ObjectDestroyer>();
        for (int i = 0; i < platforms.Length; i++)
            platforms[i].gameObject.SetActive(false);

        player.transform.position = playerStart;
        platformGenerator.position = platformStart;
        player.gameObject.SetActive(true);
        scoreManager.isDead = false;
        scoreManager.currentScore = 0;
    }
}
