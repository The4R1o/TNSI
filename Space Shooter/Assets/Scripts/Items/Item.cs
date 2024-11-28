using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemConfig itemData;
    private float fallSpeed = 2;
    private Rigidbody2D rb;
    private void Start()
    {
        Destroy(gameObject, 10f);
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        rb.velocity = Vector2.down * fallSpeed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player"))
        {
            var playerStats = other.collider.GetComponent<PlayerStats>();
            var playerHealth = other.collider.GetComponent<PlayerHealth>();
            foreach(var modifier in itemData.modifier)
            {
                GameManager.instance.SortStat(modifier.itemType, modifier.value); 
                switch (modifier.itemType)
                {
                    case ItemType.Damage:
                        playerStats.IncDamage((int)modifier.value);
                        break;
                    case ItemType.FireRate:
                        playerStats.IncFireRate(modifier.value);
                        break;
                    case ItemType.BulletSpeed:
                        playerStats.IncBulletSpeed((int)modifier.value);
                        break;
                    case ItemType.CritDamage:
                        playerStats.IncCritDamage(modifier.value);
                        break;
                    case ItemType.CritChance:
                        playerStats.IncCritChance(modifier.value);
                        break;
                    case ItemType.Gun:
                        playerStats.IncGunCount((int)modifier.value);
                        break;
                    case ItemType.HealthBuff:
                        playerHealth.SetMaxHealth((int)modifier.value);
                        break;
                }

            }
            GameManager.instance.SortItemType(itemData.rarity);

            FindObjectOfType<DisplayTextPopup>().DisplayeText(itemData.name); // Sending info about item to be displayed to the palyer

            Destroy(gameObject);

        }
    }
}

