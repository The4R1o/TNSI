using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider; 

    private Health health;
    private void Awake()
    {
        health = GetComponentInParent<Health>();
    }
    private void Start()
    {
        slider.maxValue = health.GetMaxHealth();
    }
    private void Update()
    {
        slider.value = health.currentHealth;
    }
}
