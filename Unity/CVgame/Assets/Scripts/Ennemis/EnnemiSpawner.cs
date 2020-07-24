using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiSpawner : MonoBehaviour
{
    public GridPathFinder pathFinder;
    public GameObject[] ennemisSpawning;
    public Vector3[] relativePositionSpawning;
    public float spawnRate = 2.0f;
    private float spawnTimeCounter = 0.0f;
    public float spawnRateRandomness = 0.8f;
    // Start is called before the first frame update
    public bool isSpawning = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpawning)
        {
            spawnTimeCounter+=Time.deltaTime;
            while(spawnTimeCounter>spawnRate){
                spawnTimeCounter-=spawnRate;
                Vector3 randomPosition = transform.position+relativePositionSpawning[Random.Range(0,relativePositionSpawning.Length)]+Vector3.left*Random.Range(-0.5f,0.5f)+Vector3.forward*Random.Range(-0.5f,0.5f);
                GameObject ennemi= Instantiate(ennemisSpawning[Random.Range(0,ennemisSpawning.Length)],randomPosition,Quaternion.identity,transform);
                ennemi.GetComponent<EnnemiController>().Initialize(pathFinder);
            }
        }
    }
}
