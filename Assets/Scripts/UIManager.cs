using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager uiManagerInstance;
    [SerializeField]
    Text enemiesKilledNumberText;
    int enemiesKilledNumber = 0;
    [SerializeField]
    Text playerCoinsText;
    int playerCoins = 0;
    [SerializeField]
    Slider playerHealthSlider;
    [SerializeField]
    GameObject inGamePanel, endGamePanel;
    // Start is called before the first frame update
    void Awake()
    {
        // Save a reference to the AudioManager component as our //singleton instance.
        uiManagerInstance = this;
    }
    void Start()
    {
        playerCoins = 0;
        inGamePanel.SetActive(true);
        endGamePanel.SetActive(false);
        enemiesKilledNumber = 0;
        enemiesKilledNumberText.text = "0";
        playerCoinsText.text = "0";
    }

    public void UpdateEnemiesKilledNumber()
    {
        enemiesKilledNumber++;
        enemiesKilledNumberText.text = "" + enemiesKilledNumber;
    }

    public void PlayerGetCoins(int coinNumber)
    {
        playerCoins += coinNumber;
        playerCoinsText.text = "" + playerCoins;
    }

    public void GameOver()
    {
        inGamePanel.SetActive(false);
        endGamePanel.SetActive(true);
    }

    public void RePlay()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdatePlayerHealthSlider(float value)
    {
        playerHealthSlider.value = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
