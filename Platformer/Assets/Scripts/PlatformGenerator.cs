using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {

    public ObjectPooler[] objectPool;
    public GameObject platformToGenerate; 
    public Transform generationPoint;
    public float distanceBetween;
    public float distanceBetweenMin;
    public float distanceBetweenMax;
    private float[] platformWidth;
    private int platformSelected;
    private float platformMaxHeight;
    private float platformMinHeight;
    public Transform platformHeightCeil; // needs to be transform to know pos in the world
                                        // don't let new platforms go past this ceiling limit
    public float platformHeightMaxChange; // limit the highest Y axis change in a new height
    private float platformHeightChange;
    public CoinGenerator coinGenerator;
    public int coinGenThreshold;
    public int spikeGenThreshold;
    public ObjectPooler spikePool;
    public float powerUpHeight;
    public ObjectPooler powerUpPool;
    public int powerUpGenThreshold;


    // Use this for initialization
    void Start () {
        platformWidth = new float[objectPool.Length];
        for (int i = 0; i < objectPool.Length; i++) {
            platformWidth[i] = objectPool[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }
        platformMinHeight = transform.position.y;
        platformMaxHeight = platformHeightCeil.position.y;

        coinGenerator = FindObjectOfType<CoinGenerator>();
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.x < generationPoint.position.x) {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            // platform generation point is attached to camera 
            // once it moves past the current generation point, pick an inactive object from the pool
            platformSelected = Random.Range(0, objectPool.Length);

            // randomly select new height for the next platform
            platformHeightChange = transform.position.y + Random.Range(platformMinHeight, platformMaxHeight);

            // bounds check on new height change
            if (platformHeightChange > platformMaxHeight)
                platformHeightChange = platformMaxHeight;
            else if (platformHeightChange < platformMinHeight)
                platformHeightChange = platformMinHeight;

            if (Random.Range(0,100) < powerUpGenThreshold)
            {
                // add new powerup halfway between platforms
                GameObject newPowerUp = powerUpPool.GetPooledObject();
                newPowerUp.transform.position = transform.position + 
                    new Vector3(distanceBetween / 2f, Random.Range(powerUpHeight / 2f, powerUpHeight), 0f);
                newPowerUp.SetActive(true);
            }

            // move generator to position of the platform generation point / 2 + platform width, and new height
            transform.position = new Vector3(transform.position.x + (platformWidth[platformSelected] / 2) + distanceBetween, 
                platformHeightChange, transform.position.z);

            // put randomly selected object at new position
            GameObject newPlatform = objectPool[platformSelected].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            // add coins right before so the current generation points is still center
            // threshold value to not spawn coins on every platform
            if (Random.Range(0,100) < coinGenThreshold)
                coinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));

            if (Random.Range(0,100) < spikeGenThreshold) {
                GameObject newSpike = spikePool.GetPooledObject();
                float spikeRandomPos = Random.Range(-platformWidth[platformSelected] / 2 + 1f, platformWidth[platformSelected] / 2 - 1f);
                Vector3 spikePosition = new Vector3(spikeRandomPos, 0.5f, 0f);
                newSpike.transform.position = transform.position + spikePosition;
                newSpike.transform.rotation = transform.rotation;
                newSpike.SetActive(true);
            }

            // don't add distanceBetween twice to avoid overlapping platforms
            transform.position = new Vector3(transform.position.x + (platformWidth[platformSelected] / 2),
               transform.position.y, transform.position.z);
        }

	}
}
