using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(DisableObject());
    }

    private IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
