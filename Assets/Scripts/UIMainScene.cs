using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject gameOverScreen;
    public GameObject player;

    [Header("Player HUD Informations")]
    public GameObject crossHair;
    public Image dodgeCharge;
    public Slider playerHealthBar;
    private int distanceTraveled;
    public TextMeshProUGUI distanceTravel;

    [Header("Enemy Informations")]
    public GameObject currentEnemy;
    public TextMeshProUGUI enemyName;
    public Slider enemyHealthBar;

    private void Awake()
    {
        GameManager.Instance.SetGameOver(false);
        distanceTraveled = 0;
        distanceTravel.SetText(distanceTraveled.ToString());
        Instance = this;
    }

    void Update()
    {
        GetMousePosition();
        crossHair.transform.position = GetMousePosition();

        if (!GameManager.Instance.GetGameOver())
        {
            DisplayMetersTravel();
        }

        if (GameManager.Instance.GetGameOver() && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
            distanceTravel.SetText(distanceTraveled.ToString());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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

            player.GetComponent<Vehicule>().Heal(Mathf.RoundToInt(playerHealthBar.maxValue));

            SpawnManager.Instance.spawnEnemy = true;
            SpawnManager.Instance.lastSpawnTime += distanceTraveled + 300;
            SpawnManager.Instance.InvokeRepeating("SpawnObstacle", SpawnManager.Instance.startDelay*1.2f, SpawnManager.Instance.repeatRate *1.2f);
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

    public void DisplayMetersTravel()
    {
        distanceTravel.SetText(string.Format("{0}", distanceTraveled));
    }

    public int DistanceTraveled()
    {        
        if (!GameManager.Instance.GetGameOver())
        {
           return distanceTraveled = Mathf.RoundToInt(Time.timeSinceLevelLoad * 10f);
        }
        return distanceTraveled = 0;
    }

    public static Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos -= Camera.main.transform.forward * 10f; // Make sure to add some "depth" to the screen point
        return mousePos;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
