using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    public float startDelay;
    public float repeatRate;
    
    public List<GameObject> obstaclePrefabs;
    public List<GameObject> enemyPrefabs;

    public List<Transform> slots;

    public bool spawnEnemy;
    private void Awake()
    {
        Instance = this;
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);

    }

    private void Update()
    {
        if (GameManager.Instance.DistanceTraveled() >= 150 && spawnEnemy && !GameManager.Instance.GetGameOver())
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

