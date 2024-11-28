using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField][Range(0,1)] private float itemDropSuccessChance;

    [Header("DROP CHANCES")]
    [SerializeField] [Range(0, 1f)] private float commonItemDropChance; 
    [SerializeField] [Range(0, 1f)] private float rareItemDropChance; 
    [SerializeField] [Range(0, 1f)] private float epicItemDropChance; 
    [SerializeField] [Range(0, 1f)] private float legendaryItemDropChance; 

    [Header("ITEMS")]
    [SerializeField] private GameObject[] commonItems;
    [SerializeField] private GameObject[] rareItems;
    [SerializeField] private GameObject[] epicItems;
    [SerializeField] private GameObject[] legenderyItems;

    private Health objectHealth;
    private void OnEnable()
    {
        objectHealth = GetComponent<Health>();
        objectHealth.OnDead += CheckRoll;
    }

    private void OnDisable()
    {
        objectHealth.OnDead -= CheckRoll;
    }
    private float RollDice()
    {
        return Random.Range(0, 1f); 
    }
    void CheckRoll()
    {
        if (RollDice() > (1 - itemDropSuccessChance)) RollItem(); // RollDice(0.3) > 1 - 0.4 -> 0.3 > 0.6 -> false
    }

    private void RollItem()
    {
        int itemRarityIndex = 0;
        var itemRarityRoll = RollDice();
        if (itemRarityRoll >= commonItemDropChance)
        {
            itemRarityRoll -= commonItemDropChance;

            if (itemRarityRoll >= rareItemDropChance)
            {
                itemRarityRoll -= rareItemDropChance;

                if (itemRarityRoll >=  epicItemDropChance)
                {
                    itemRarityRoll -= epicItemDropChance;

                    if (itemRarityRoll >= legendaryItemDropChance)
                    {
                        itemRarityIndex = 3;
                    }
                }
                else
                {
                    itemRarityIndex = 2;
                }
            }
            else
            {
                itemRarityIndex = 1;
            }
        }
        else
        {
            itemRarityIndex = 0;
        }    
               
        switch (itemRarityIndex)
        {
            case 0:
                SpawnCommonItem();
                break;
            case 1:
                SpawnRareItem();
                break;
            case 2:
                SpawnEpicItem();
                break;
            case 3:
                SpawnLegendaryItem();
                break;
        }
    }

    private void SpawnLegendaryItem()
    {
        var index = Random.Range(0, legenderyItems.Length);
        Instantiate(legenderyItems[index], transform.position, Quaternion.identity);
    }

    private void SpawnEpicItem()
    {
        var index = Random.Range(0, epicItems.Length);
        Instantiate(epicItems[index], transform.position, Quaternion.identity);
    }

    private void SpawnRareItem()
    {
        var index = Random.Range(0, rareItems.Length);
        Instantiate(rareItems[index], transform.position, Quaternion.identity);
    }

    private void SpawnCommonItem()
    {
        var index = Random.Range(0, commonItems.Length);
        Instantiate(commonItems[index], transform.position, Quaternion.identity);
    }
}
