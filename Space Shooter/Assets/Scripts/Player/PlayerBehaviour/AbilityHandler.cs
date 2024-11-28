using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityHandler : MonoBehaviour
{
    [Header("Ability")]
    [SerializeField] private GameObject shildPF;
    public float abilityCooldown = 5f;
    public float abilityDuration = 3f;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI cooldownText;
    [SerializeField] Image cooldownBar;


    float abilityCooldownTimer;
    bool isActive;
    private void Start()
    {
        abilityCooldownTimer = 0;
        cooldownBar.fillAmount = 1;
    }
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton1)) && abilityCooldownTimer <= 0)
        {
            AudioManager.instance.PlaySound("Shild");
            isActive = true;
            StartCoroutine(ActivateShild());
            abilityCooldownTimer = abilityCooldown;
        }

        // UI
        if(!isActive)
        {     
            abilityCooldownTimer -= Time.deltaTime;
            if (abilityCooldownTimer <= 0)
            {
                abilityCooldownTimer = 0;
                cooldownText.text = "";
            }
            else
            {
                cooldownText.text = Mathf.Round(abilityCooldownTimer).ToString();
            }

            cooldownBar.fillAmount += Time.deltaTime/abilityCooldown;
        }
    }
    private IEnumerator ActivateShild()
    {
        shildPF.GetComponent<Animator>().SetBool("IsActive", true);
        yield return new WaitForSeconds(abilityDuration);
        shildPF.GetComponent<Animator>().SetBool("IsActive", false);
        cooldownBar.fillAmount = 0;
        isActive = false;
    }

}
