using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentDistance;
    public int bestDistanceTraveled;

    public bool isGameOver = false;
    public bool isEnemyClose;

    private void Awake()
    {
        Instance = this;    
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
        int distance = Mathf.RoundToInt(Time.time * 5f);

        return distance;
    }
}
