using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] int healAmount;
    [SerializeField] AudioClip collectSFX;
    [SerializeField] float collectSFXVolume;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collectSFX, Camera.main.transform.position, collectSFXVolume);
            other.collider.GetComponent<PlayerHealth>().Heal(healAmount);
            FindObjectOfType<ObjectSpawner>().isSpawned = false;
            GameObject go = ObjectPooler.instance.GetPooledObject("HealVFX");
            if (go != null)
            {
                go.transform.position = transform.position;
                go.transform.rotation = Quaternion.identity;
                go.SetActive(true);
                go.GetComponent<ParticleSystem>().Play();
            }
            gameObject.SetActive(false);
        }   

    }
}
