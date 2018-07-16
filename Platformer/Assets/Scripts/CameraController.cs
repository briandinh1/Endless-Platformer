using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public PlayerController player;
    private Vector3 lastPlayerPos;
    private float distanceToMove; 

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        lastPlayerPos = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // move camera by current player pos - last player pos
        distanceToMove = player.transform.position.x - lastPlayerPos.x;
        
        // update camera pos
        transform.position = new Vector3(transform.position.x + distanceToMove, 
            transform.position.y, transform.position.z);

        lastPlayerPos = player.transform.position;
	}
}
