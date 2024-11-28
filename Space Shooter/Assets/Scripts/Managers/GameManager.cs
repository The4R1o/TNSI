using System.Collections;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChanged;

    private Sprite characterSprite, shipSprite;
    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
        UpdateGameState(GameState.MainMenu);
    }
    

    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:               
                break;
            case GameState.Gameplay:
                HandleGamepalyStage();
                break;
            case GameState.BossFight:
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }
    private void HandleGamepalyStage()
    {
        SetRandomBackGround();
    }
    private void HandleGameOver()
    {
        Time.timeScale = 0;
    }

    #region Ship Setup
    public ShipConfig shipConfig { get; set; }
    public void SetShipSprite(Sprite newSprite)
    {
        shipSprite = newSprite;
    }
    public Sprite GetShipSprite()
    {
        return shipSprite;
    }

    public ShipConfig GetShipConfig()
    {
        return shipConfig;
    }
    #endregion


    #region Setting UI 
    public void SetCharacterSprite(Sprite newSprite)
    {
        characterSprite = newSprite;
    }

    public Sprite GetCharacterSprite()
    {
        return characterSprite;
    }
   
    [SerializeField] private GameObject background;
    [SerializeField] private Sprite[] images;

    private void SetRandomBackGround()
    {
        background.GetComponent<SpriteRenderer>().sprite = images[UnityEngine.Random.Range(0, images.Length)];
    }

    #endregion


    #region Game Over Panel Stats
    public void ResetTrackingStats()
    {
        commonCount = 0;
        rareCount = 0;
        epicCount = 0;
        legendaryCount = 0;

        damage = 0;
        firerate = 0;
        critChance = 0;
        critDamage = 0;
        gunCount = 0;
        health = 0;
        speed = 0;

        DPS = 0;
    }

    public int commonCount { private set; get; }
    public int rareCount { private set; get; }
    public int epicCount { private set; get; }
    public int legendaryCount { private set; get; }
    public void SortItemType(ItemRarity item)
    {
        switch (item)
        {
            case ItemRarity.Common:
                commonCount++;
                break;
            case ItemRarity.Rare:
                rareCount++;
                break;
            case ItemRarity.Epic:
                epicCount++;
                break;
            case ItemRarity.Legendary:
                legendaryCount++;
                break;
        }
    }



    public float damage { private set; get; }
    public float firerate { private set; get; }
    public float critDamage { private set; get; }
    public float critChance { private set; get; }
    public float speed { private set; get; }
    public float health { private set; get; }
    public int gunCount { private set; get; }
    public float DPS { set; get; }

    public void SortStat(ItemType itemType, float value)
    {
        switch (itemType)
        {
            case ItemType.Damage:
                damage += value;
                break;
            case ItemType.FireRate:
                firerate += value;
                break;
            case ItemType.BulletSpeed:
                speed += value;
                break;
            case ItemType.CritDamage:
                critDamage += value;
                break;
            case ItemType.CritChance:
                critChance += value;
                break;
            case ItemType.Gun:
                gunCount += (int)value;
                break;
            case ItemType.HealthBuff:
                health += value;
                break;
        }


        #endregion

    }
}

    public enum GameState
    {   
    MainMenu,
    Gameplay,
    BossFight,
    GameOver,
    }

