using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines2 : MonoBehaviour
{
    public Renderer bodyRenderer;
    public Color normalColor;
    public Color passedColor;

    bool passed = false;
    // Start is called before the first frame update
    void Start()
    {
        bodyRenderer.material.color = normalColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (passed)
        {
            bodyRenderer.material.color = passedColor;
        }
        else
        {
            bodyRenderer.material.color = normalColor;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        // Increment count if a unit enters the boundary
        if (other.CompareTag("Unit"))
        {
            passed = true;
        }
    }
    public bool checkPassed()
    {
        return passed;
    }
}
