using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildItem : MonoBehaviour
{
    public Material material;

    private void Update()
    {
        keycode();
    }

    private void keycode()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A");
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("B");
        }
    }

    private void SetColor(float _r, float _g, float _b)
    {
        Color color = material.color;
        color.r = _r;
        color.g = _g;
        color.b = _b;
        material.color = color;
    }
}
