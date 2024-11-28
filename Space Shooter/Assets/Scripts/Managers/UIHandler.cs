using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Image characterImg;
    [SerializeField] private Image fill;

    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private GameObject player;
    [SerializeField] GameObject pauseMenuPanel;

    Health playerHealth;

    private bool isPaused = false;
    void Awake()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        SetCharacterImage();
        pauseMenuPanel.SetActive(false);
    }

    void Start()
    {
        playerHealth.OnMaxHealthChange += UpdateHealthUI;
        fill.fillAmount = playerHealth.GetMaxHealth();
        healthText.text = playerHealth.GetMaxHealth().ToString();
    }
    private void OnDisable()
    {
        playerHealth.OnMaxHealthChange -= UpdateHealthUI;
    }
    private void UpdateHealthUI()
    {
        fill.fillAmount = playerHealth.GetMaxHealth();
        healthText.text = playerHealth.GetMaxHealth().ToString();
    }

    void Update()
    {     
        healthText.text = playerHealth.currentHealth.ToString();
        fill.fillAmount = playerHealth.currentHealth / playerHealth.GetMaxHealth();
        #region Pause The Game
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
        #endregion
    }
    private void SetCharacterImage()
    {
        characterImg.sprite = GameManager.instance.GetCharacterSprite();
    }
    private void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseMenuPanel.SetActive(true);
    }
    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.SetActive(false);
    }
}
