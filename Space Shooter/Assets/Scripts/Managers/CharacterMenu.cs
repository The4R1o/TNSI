using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterMenu : MonoBehaviour
{
    [Header("Sprite/Image References")]
    [SerializeField] private Image characterIMG;
    [SerializeField] private Image shipIMG;
    [SerializeField] private List<Sprite> characters = new List<Sprite>();

    [Header("Ship Info")]
    [SerializeField]private List<ShipInfo> allShips = new List<ShipInfo>();

    [Header("Text Stats References")]
    [SerializeField] TextMeshProUGUI healthTXT;
    [SerializeField] TextMeshProUGUI flySpeedTXT;
    [SerializeField] TextMeshProUGUI damageTXT;
    [SerializeField] TextMeshProUGUI fireRateTXT;
    [SerializeField] TextMeshProUGUI critChanceTXT;
    [SerializeField] TextMeshProUGUI critDamageTXT;
    [SerializeField] TextMeshProUGUI speedTXT;
    [SerializeField] TextMeshProUGUI gunTXT;

    private int currentCharacterIndex = 0;
    private int currentShipIndex = 0;

    private ShipConfig shipConfig;

    [Serializable]
    public class ShipInfo
    {
        public Sprite shipSprite;
        public ShipConfig shipConfig;
    }
    private void Start()
    {
        characterIMG.sprite = characters[currentCharacterIndex];

        shipIMG.sprite = allShips[currentShipIndex].shipSprite;
        shipConfig = allShips[currentShipIndex].shipConfig;
        ShowStats(currentShipIndex);
    }
    public void NextCharacter()
    {
        currentCharacterIndex++;
        if (currentCharacterIndex > characters.Count - 1)
        {
            currentCharacterIndex = characters.Count - 1;
        }
        characterIMG.sprite = characters[currentCharacterIndex];
    }
    public void PreviousCharacter()
    {
        currentCharacterIndex--;
        if (currentCharacterIndex < 0)
        {
            currentCharacterIndex = 0;
        }

        characterIMG.sprite = characters[currentCharacterIndex];
    }
    public void NextShip()
    {
        currentShipIndex++;
        if (currentShipIndex > allShips.Count - 1)
        {
            currentShipIndex = allShips.Count - 1;
        }
        shipIMG.sprite = allShips[currentShipIndex].shipSprite;
        shipConfig = allShips[currentShipIndex].shipConfig;
        ShowStats(currentShipIndex);
    }
    public void PreviousShip()
    {
        currentShipIndex--;
        if (currentShipIndex < 0)
        {
            currentShipIndex = 0;
        }

        shipIMG.sprite = allShips[currentShipIndex].shipSprite;
        shipConfig = allShips[currentShipIndex].shipConfig;
        ShowStats(currentShipIndex);
    }
    private void ShowStats(int index)
    {
        healthTXT.text = allShips[index].shipConfig.health.ToString();
        flySpeedTXT.text = allShips[index].shipConfig.speed.ToString();
        damageTXT.text = allShips[index].shipConfig.damage.ToString();
        fireRateTXT.text = allShips[index].shipConfig.fireRate.ToString();
        critChanceTXT.text = allShips[index].shipConfig.critChance.ToString();
        critDamageTXT.text = allShips[index].shipConfig.critDamage.ToString();
        speedTXT.text = allShips[index].shipConfig.bulletSpeed.ToString();
        gunTXT.text = allShips[index].shipConfig.numOfGuns.ToString();
    }

    public void SendCustomInfo()
    {
        GameManager.instance.SetCharacterSprite(characterIMG.sprite);
        GameManager.instance.SetShipSprite(shipIMG.sprite);
        GameManager.instance.shipConfig = shipConfig;
    }
}
