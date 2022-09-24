using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    public List<Transform> slots;
    [Header("Obstacles Settings")]
    public List<GameObject> obstaclePrefabs;
    public float obstacleSpeed;
    public float startDelay;
    public float repeatRate;
    [Header("Enemies Settings")]
    public List<GameObject> enemyPrefabs;
    public float lastSpawnTime;
    public float spawnRate;

    public bool spawnEnemy;

    private void Awake()
    {
        Instance = this;
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
    }

    private void Update()
    {
        if (UIMainScene.Instance.DistanceTraveled() >= lastSpawnTime && spawnEnemy && !GameManager.Instance.GetGameOver())
        {
            spawnEnemy = true;
            CancelInvoke();

            if (spawnEnemy)
            {
                spawnEnemy = false;
                Invoke(nameof(SpawnEnemy), startDelay);
            }
        }

    }

    public void RepeatInvoke(string method, float delay, float repeatRate)
    {
        InvokeRepeating(nameof(method), delay, repeatRate);
    }

    void SpawnObstacle()
    {
        Instantiate(obstaclePrefabs[GetRandomInt(obstaclePrefabs)], slots[GetRandomInt(slots)].position, Quaternion.identity);
    }

    void SpawnEnemy()
    {
        UIMainScene.Instance.SetActiveUIContent(UIMainScene.Instance.upperCenter, true);
        Instantiate(enemyPrefabs[GetRandomInt(enemyPrefabs)], this.transform.position, Quaternion.identity);
    }

    // ABSTRACTION
    public int GetRandomInt<T>(List<T> list)
    {
        return Random.Range(0,list.Count);
    }

}

