using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timeAlive = 0;

    void Update()
    {
        timeAlive += Time.deltaTime;
    }
    public float GetTime()
    {
        return timeAlive;
    }
}
