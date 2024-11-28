using TMPro;
using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    private ShipConfig shipConfig;
    public int damage { protected set; get; }
    public float fireRate { protected set; get; }
    public int bulletSpeed { protected set; get; }
    public float critChance { protected set; get; }
    public float critDamage { protected set; get; }
    public int numOfGuns { protected set; get; }
    public int spreadAngle { protected set; get; }

    [Header("TEXT")]
    [SerializeField] TextMeshProUGUI damageTXT;
    [SerializeField] TextMeshProUGUI fireRateTXT;
    [SerializeField] TextMeshProUGUI critChanceTXT;
    [SerializeField] TextMeshProUGUI critDamageTXT;
    [SerializeField] TextMeshProUGUI speedTXT;
    [SerializeField] TextMeshProUGUI dpsTXT;

    private Action OnStatChange;
    private void Start()
    {
        OnStatChange += UpdateStatsUI;
        shipConfig = GameManager.instance.GetShipConfig();
        LoadStats();
        UpdateStatsUI();
    }

    private void LoadStats()
    {
        damage = shipConfig.damage;
        fireRate = shipConfig.fireRate;
        critChance = shipConfig.critChance;
        critDamage = shipConfig.critDamage;
        bulletSpeed = shipConfig.bulletSpeed;
        numOfGuns = shipConfig.numOfGuns;
        spreadAngle = shipConfig.spreadAngle;
    }

    private void OnDestroy()
    {
        OnStatChange -= UpdateStatsUI;
    }
    private void UpdateStatsUI()
    {
        damageTXT.text = damage.ToString();
        fireRateTXT.text = Mathf.RoundToInt(fireRate).ToString();
        critDamageTXT.text = critDamage.ToString();
        critChanceTXT.text = critChance.ToString();
        speedTXT.text = bulletSpeed.ToString();
        dpsTXT.text = DPS().ToString();
    }

    #region CALULATE STATS
    protected bool isCrit;
    protected int GetDamage()
    {
        isCrit = UnityEngine.Random.Range(0, 1f) > (1 - (critChance/100));
        if (isCrit)            
            return Mathf.RoundToInt(damage + (damage * (critDamage / 100)));
        else
            return damage;
    }
    public void IncDamage(int _bounsDamage)
    {
        damage += _bounsDamage;
        OnStatChange?.Invoke();
    }
    public void IncFireRate(float _bounsFireRate)
    {
        fireRate += _bounsFireRate;
        OnStatChange?.Invoke();
    }

    public void IncBulletSpeed(int _bounsBulletSpeed)
    {
        bulletSpeed += _bounsBulletSpeed;
        OnStatChange?.Invoke();
    }

    public void IncCritChance(float _bounsCritChance)
    {

        critChance += _bounsCritChance;
        OnStatChange?.Invoke();
    }

    public void IncCritDamage(float _bounsCritDamage)
    {
        critDamage += _bounsCritDamage;
        OnStatChange?.Invoke();
    }

    public void IncGunCount(int _bounsGuns)
    {
        numOfGuns += _bounsGuns;
        OnStatChange?.Invoke();
    }

    public int DPS()
    {
        float baseDamage = damage;
        float maxDamage = damage + (damage * (critDamage / 100));

        float BDPA = (baseDamage + maxDamage) / 2;
        float CDPA = BDPA * (critChance / 100) * (critDamage / 100);
        float NDPA = BDPA * (1 -(critChance / 100));

        return Mathf.RoundToInt((CDPA + NDPA) * fireRate * numOfGuns);

    }
    #endregion
    #region Stat Difference
    public void StartStats()
    {
        
    }
    #endregion

}
