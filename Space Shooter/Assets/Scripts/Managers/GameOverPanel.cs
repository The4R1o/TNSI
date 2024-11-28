using System;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Timer timer;
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private TextMeshProUGUI dpsTXT;
    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timeAliveTXT;
    [Header("Enemy Kills")]
    [SerializeField] private TextMeshProUGUI enemyKillsTXT;
    [Header("Stats From Items")]
    [SerializeField] private TextMeshProUGUI damageTXT;
    [SerializeField] private TextMeshProUGUI fireRateTXT;
    [SerializeField] private TextMeshProUGUI critChanceTXT;
    [SerializeField] private TextMeshProUGUI critDamageTXT;
    [SerializeField] private TextMeshProUGUI speedTXT;
    [SerializeField] private TextMeshProUGUI gunsTXT;
    [SerializeField] private TextMeshProUGUI healthTXT;
    [Header("Items Obtained")]
    [SerializeField] private TextMeshProUGUI commonTXT;
    [SerializeField] private TextMeshProUGUI rareTXT;
    [SerializeField] private TextMeshProUGUI epicTXT;
    [SerializeField] private TextMeshProUGUI legendaryTXT;

    private Animator animator;
    private void Start()
    {
        animator = gameOverPanel.GetComponent<Animator>();
        animator.SetBool("IsActive", false);
        gameOverPanel.SetActive(false);
        GameManager.OnGameStateChanged += OnGameOver;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameOver;
    }
    private void OnGameOver(GameState state)
    {
        if (state == GameState.GameOver) DisplayGameOverScreen();
    }

    private void DisplayGameOverScreen()
    {
        animator.SetBool("IsActive", true);
        gameOverPanel.SetActive(true);
        finalScore.text = ScoreManager.instance.GetScore().ToString();
        enemyKillsTXT.text = ScoreManager.instance.GetEnemyKillCount().ToString();

        //items rarity count
        commonTXT.text = GameManager.instance.commonCount.ToString();
        rareTXT.text = GameManager.instance.rareCount.ToString();
        epicTXT.text = GameManager.instance.epicCount.ToString();
        legendaryTXT.text = GameManager.instance.legendaryCount.ToString();

        //stats for items
        damageTXT.text = GameManager.instance.damage.ToString();
        fireRateTXT.text = Mathf.RoundToInt(GameManager.instance.firerate).ToString();
        critChanceTXT.text = GameManager.instance.critChance.ToString();
        critDamageTXT.text = GameManager.instance.critDamage.ToString();
        speedTXT.text = GameManager.instance.speed.ToString();
        healthTXT.text = GameManager.instance.health.ToString();
        gunsTXT.text = GameManager.instance.gunCount.ToString();

        dpsTXT.text = GameManager.instance.DPS.ToString();

        timeAliveTXT.text = Mathf.RoundToInt(timer.GetTime()).ToString() + "s";

    }
}
