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

    public bool spawnEnemy;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
    }

    private void Update()
    {
        if (GameManager.Instance.DistanceTraveled() >= 50 && spawnEnemy)
        {
            spawnEnemy = true;

            if (spawnEnemy)
            {
                spawnEnemy = false;
                Invoke(nameof(SpawnEnemy), 0.0f);
            }
        }
    }

    void SpawnObstacle()
    {
        Instantiate(obstaclePrefabs[GetRandomInt(obstaclePrefabs)], slots[GetRandomInt(slots)].position, Quaternion.identity);
    }

    void SpawnEnemy()
    {
        Debug.Log("1");
        Instantiate(enemyPrefabs[GetRandomInt(enemyPrefabs)], this.transform.position, Quaternion.identity);
    }

    // ABSTRACTION
    public int GetRandomInt<T>(List<T> list)
    {
        return Random.Range(0,list.Count);
    }
}
