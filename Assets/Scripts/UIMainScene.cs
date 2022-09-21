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

    [Header("")]
    public GameObject upperCenter;
    
    [Header("Player HUD Informations")]
    public GameObject crossHair;
    public Image dodgeCharge;
    public Slider playerHealthBar;
    public TextMeshProUGUI distanceTravel;

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

    public void SetActiveUIContent(GameObject uiContent, bool state)
    {
        uiContent.SetActive(state);
    }

    public void DisplayEnemyInfo(GameObject enemy, string name, float healthValue)
    {
        //Set enemy
        currentEnemy = enemy;
        //Set hisname
        enemyName.SetText(name);
        //Set the slider max value to the his max health
        SetSliderTo(enemyHealthBar, healthValue);
    }

    public void UpdateEnemyHealth(float damage)
    {
        UpdateSlider(enemyHealthBar, damage);

        if (enemyHealthBar.value <= 0)
        {
            GameManager.Instance.isEnemyClose = false;
            SetActiveUIContent(UIMainScene.Instance.upperCenter, false);

            Destroy(currentEnemy);
            currentEnemy = null;

            SpawnManager.Instance.InvokeRepeating("SpawnObstacle", SpawnManager.Instance.startDelay, SpawnManager.Instance.repeatRate);
        }
    }

    public void SetSliderTo(Slider slider, float value)
    {
        slider.maxValue = value;

        slider.value = slider.maxValue;
    }

    public void UpdateSlider(Slider slider, float value)
    {
        slider.value -= value;
    }

    public void DisplayMetersTravel(int distance)
    {
        distanceTravel.SetText(string.Format("{0} {1}", "Distance: ", distance));
    }

    public static Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos -= Camera.main.transform.forward * 10f; // Make sure to add some "depth" to the screen point
        return mousePos;
    }
}
