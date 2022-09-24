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
        if (GameManager.Instance != null)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetGameOver(bool value)
    {
        m_isGameOver = value;

        if (m_isGameOver && UIMainScene.Instance != null)
        {
            UIMainScene.Instance.gameOverScreen.SetActive(isGameOver);
            SpawnManager.Instance.CancelInvoke();
        }
    }

    public bool GetGameOver()
    {
        return isGameOver;
    }

}
