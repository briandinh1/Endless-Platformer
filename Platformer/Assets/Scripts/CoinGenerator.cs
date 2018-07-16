using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

    public ObjectPooler[] coinPool;
    public float distanceBetweenCoins;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnCoins(Vector3 startPosition) {
        // coin1 in the center
        GameObject coin1 = coinPool[getCoinType()].GetPooledObject();
        coin1.transform.position = startPosition;
        coin1.SetActive(true);
        
        // coin2 to the left of coin1
        GameObject coin2 = coinPool[getCoinType()].GetPooledObject();
        coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, 
            startPosition.y, startPosition.z);
        coin2.SetActive(true);

        // coin3 to the right of coin1
        GameObject coin3 = coinPool[getCoinType()].GetPooledObject();
        coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins,
            startPosition.y, startPosition.z);
        coin3.SetActive(true);
    }

    // pretty crude; not exactly perfectly weighted for common -> rare
    private int getCoinType() {
        float coinChance = Random.Range(0, 100);

        if (coinChance <= 50)
            return 0; // bronze
        else if (coinChance > 50 && coinChance <= 90)
            return 1; // silver

        return 2; // gold
    }
}
