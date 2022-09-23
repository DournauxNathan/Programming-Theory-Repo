using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int bestDistanceTraveled;

    private bool m_isGameOver = false;
    public bool isGameOver { get {return m_isGameOver; } private set { m_isGameOver = value;  } }
    
    public bool isEnemyClose;

    private void Awake()
    {
        Instance = this;    
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetGameOver())
        {
            UIMainScene.Instance.DisplayMetersTravel(DistanceTraveled());
        }
    }

    public int DistanceTraveled()
    {
        int distance = Mathf.RoundToInt(Time.time * 10f);

        return distance;
    }

    public void SetGameOver(bool value)
    {
        m_isGameOver = value;

        if (m_isGameOver)
        {
            SpawnManager.Instance.CancelInvoke();
        }
    }

    public bool GetGameOver()
    {
        return isGameOver;
    }

}
