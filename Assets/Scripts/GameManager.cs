using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int bestDistanceTraveled;

    public bool isGameOver = false;
    public bool isEnemyClose;

    private void Awake()
    {
        Instance = this;    
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && !isEnemyClose)
        {
            UIMainScene.Instance.DisplayMetersTravel(DistanceTraveled());
        }
    }

    public int DistanceTraveled()
    {
        int distance = Mathf.RoundToInt(Time.time * 10f);

        return distance;
    }
}
