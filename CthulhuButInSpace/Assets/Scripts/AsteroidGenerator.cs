using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    public float spawnRange;
    public float amountToSpawn;
    private  Vector3 spawnPoint;
    public GameObject asteroidOne;
    public GameObject asteroidTwo;
    public float startSafeRange;
    private List<GameObject> objectsToPlace = new List<GameObject>();
    
    private bool onoff = false;

    // Start is called before the first frame update
    void Start()
    {
       
        for (int i = 0; i < amountToSpawn; i++)
        {

            PickSpawnPoint();

            //pick new spawn point if too close to player start
            while (Vector3.Distance(spawnPoint, Vector3.zero) < startSafeRange)
            {
                PickSpawnPoint();
            }
            onoff = !onoff; // toggles onoff

            if (onoff)
            {
                objectsToPlace.Add(Instantiate(asteroidOne, spawnPoint, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f))));
                objectsToPlace[i].transform.parent = this.transform;
            }
            else
            {
                objectsToPlace.Add(Instantiate(asteroidTwo, spawnPoint, Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f))));
                objectsToPlace[i].transform.parent = this.transform;
            }

        }

        //if (onoff)
        //{
        //    asteroidOne.SetActive(false);
        //}
        //else
        //{
        //    asteroidTwo.SetActive(false);
        //}
    }

    public void PickSpawnPoint()
    {
        Vector3 origin = gameObject.transform.position;
        spawnPoint = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f));

        if (spawnPoint.magnitude > 1)
        {
            spawnPoint.Normalize();
        }

        spawnPoint *= spawnRange;
        spawnPoint += origin; 
    }
}

