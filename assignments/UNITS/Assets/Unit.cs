using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    // Hover Variables
    float hoverSpeed = 3f; // Speed of the hovering motion
    float hoverHeight = .08f; // Height of the hover motion
    float time = 0;
    float rand;

    public string unitName;
    public string bio;
    public string stats;

    private float originalY;

    public NavMeshAgent nma;

    public Renderer bodyRenderer;
    public Color normalColor;
    public Color selectedColor;

    public GameObject selectEffectPrefab; // Prefab for the SE
    private GameObject currentSelectEffect; // Reference to the instantiated SE

    public Vector3 destination;

    public bool selected = false;

    float rotateSpeed;

    public bool end = false;

    LayerMask layerMask;

    void OnEnable()
    {
        GameManager.UnitClicked += GameManagerSaysUnitWasClicked;
    }

    void OnDisable()
    {
        GameManager.UnitClicked -= GameManagerSaysUnitWasClicked;
    }

    void GameManagerSaysUnitWasClicked(Unit unit)
    {
        if (unit == this)
        {
            selected = true;

            if (currentSelectEffect == null) // Instantiate the SE if not already present
            {
                currentSelectEffect = Instantiate(selectEffectPrefab, transform);
                currentSelectEffect.transform.localPosition = new Vector3(0, 4, 0);

                // Get the SelectEffect component from the instantiated prefab
                SelectEffect effect = currentSelectEffect.GetComponent<SelectEffect>();
                if (effect != null)
                {
                    effect.Initialize(selectedColor); // Pass the selected color to SE
                }
            }
        }
        else
        {
            selected = false;

            if (currentSelectEffect != null) // Destroy the SE if it exists
            {
                Destroy(currentSelectEffect);
            }
        }
    }


    void Start()
    {
        // Store the original y-position of the object
        originalY = transform.position.y;
        rand = Random.Range(0f, 1f);

        layerMask = LayerMask.GetMask("wall");

        GameManager.instance.units.Add(this);

        rotateSpeed = Random.Range(20, 60);
    }

    void OnDestroy()
    {
        GameManager.instance.units.Remove(this);
        if (currentSelectEffect != null) // Ensure cleanup of the SE
        {
            Destroy(currentSelectEffect);
        }
    }

    void Update()
    {
        // Use a sine wave to create an up-and-down hover effect
        float newY = originalY + Mathf.Sin((time + rand) * hoverSpeed) * hoverHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        time += Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        // Increment count if a unit enters the boundary
        if (other.CompareTag("Tree"))
        {
            end = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // Decrement count if a unit exits the boundary
        if (other.CompareTag("Tree"))
        {
            end = false;
        }
    }
    
    xpublic bool here()
    {
        return end;
    }
}
