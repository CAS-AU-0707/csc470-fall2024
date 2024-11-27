using System.Collections;
using UnityEngine;

public class rocks : MonoBehaviour
{
    private int here = 0; // Tracks how many units are within the boundary
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the object has a trigger collider
        if (!GetComponent<Collider>().isTrigger)
        {
            Debug.LogError("Collider is not set as a trigger!");
        }

        // Reference the GameManager instance
        gameManager = GameManager.instance;

        // Start adding rocks based on units in the boundary
        StartCoroutine(AddRocksOverTime());
    }

    public void OnTriggerEnter(Collider other)
    {
        // Increment count if a unit enters the boundary
        if (other.CompareTag("Unit"))
        {
            here++;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // Decrement count if a unit exits the boundary
        if (other.CompareTag("Unit"))
        {
            here--;
        }
    }

    // Coroutine to add rocks over time
    private IEnumerator AddRocksOverTime()
    {
        while (true)
        {
            if (gameManager != null)
            {
                // Add 1 rock per unit per second
                gameManager.rock += here * gameManager.uRock;
            }
            yield return new WaitForSeconds(1f); // Wait 1 second before adding rocks again
        }
    }
}
