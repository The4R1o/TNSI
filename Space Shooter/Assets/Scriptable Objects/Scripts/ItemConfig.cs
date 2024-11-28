using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName ="ItemData")]
public class ItemConfig : ScriptableObject
{
    public string name;
    public ItemRarity rarity;
    [TextArea]public string description;
    public  List<Modifier> modifier = new List<Modifier>();
    [Serializable]
    public class Modifier
    {
        public float value;
        public  ItemType itemType;
    }
}
public enum ItemType
{
    Damage,
    FireRate,
    BulletSpeed,
    CritDamage,
    CritChance,
    Gun,
    HealthBuff
}
public enum ItemRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}