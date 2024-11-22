using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEffect : MonoBehaviour
{
    public Renderer arrowRenderer; // Renderer of the Select Arrow
    public Color defaultColor;     // Default color of the arrow
    public Color selectedColor;    // Color of the arrow when selected

    public void Initialize(Color selectedColor)
    {
        this.selectedColor = selectedColor;
        arrowRenderer.material.color = selectedColor;
    }
}
