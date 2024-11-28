using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxEffectManager : MonoBehaviour
{
    [Range(-10f,0f)]
    [SerializeField] float scrollSpeed = .5f;
    private float offset;
    Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }
    private void Update()
    {
        offset += (Time.deltaTime * scrollSpeed) / 10f;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
