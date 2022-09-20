using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float startDelay;
    public float repeatRate;
    
    public List<GameObject> obstaclePrefabs;
    public List<GameObject> enemyPrefabs;

    public List<Transform> slots;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
    }

    void SpawnObstacle()
    {
        Instantiate(obstaclePrefabs[GetRandomInt(obstaclePrefabs)], slots[GetRandomInt(slots)].position, Quaternion.identity);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefabs[GetRandomInt(obstaclePrefabs)], slots[GetRandomInt(slots)].position, Quaternion.identity);
    }

    // ABSTRACTION
    public int GetRandomInt<T>(List<T> list)
    {
        return Random.Range(0,list.Count);
    }
}
