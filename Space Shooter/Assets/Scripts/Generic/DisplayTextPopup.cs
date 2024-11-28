using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTextPopup : MonoBehaviour
{
    public void DisplayeText(string name)
    {
        GameObject go = ObjectPooler.instance.GetPooledObject("TextPopup");
        if (go != null)
        {
            go.GetComponentInChildren<TextMeshPro>().text = name;
            go.SetActive(true);
        }
    }
}
