using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ShipConfig")]
public class ShipConfig : ScriptableObject
{

    [Header("Base Ship Stats")]
    public float speed;
    [SerializeField] public float health;

    [Header ("Base Weapon Stats")]
    public int damage;
    public float fireRate;
    public int bulletSpeed;
    public float critChance;
    public float critDamage;
    public int numOfGuns;
    public int spreadAngle;


}
