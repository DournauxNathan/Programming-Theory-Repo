using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMainScene : MonoBehaviour
{    
    public static UIMainScene Instance { get; private set; }

    public interface IUIInfoContent
    {
        string GetName();
        string GetData();
        void GetContent();
    }

    public GameObject crossHair;

    [Header("Enemy Informations")]
    public GameObject currentEnemy;
    public TextMeshProUGUI enemyName;
    public Slider enemyHealthBar;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosition();
        crossHair.transform.position = GetMousePosition();
    }

    public void DisplayEnemyInfo(GameObject enemy, string name, float healthValue)
    {
        //Set enemy
        currentEnemy = enemy;
        //Set hisname
        enemyName.SetText(name);
        //Set the slider max value to the his max health
        enemyHealthBar.maxValue = healthValue;
        enemyHealthBar.value = enemyHealthBar.maxValue;
    }

    public void UpdateEnemyHealth(float damage)
    {
        //Set the slider max value to the enemy max health
        enemyHealthBar.value -= damage;

        if (enemyHealthBar.value <= 0)
        {
            Destroy(currentEnemy);
            currentEnemy = null;
        }
    }

    public static Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos -= Camera.main.transform.forward * 10f; // Make sure to add some "depth" to the screen point
        return mousePos;
    }
}
