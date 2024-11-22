using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static Action SpacebarPressed;
    public static Action<Unit> UnitClicked;

    public static GameManager instance;

    public Camera mainCamera;

    public Unit selectedUnit;

    public List<Unit> units = new List<Unit>();

    public GameObject popUpWindow;

    public TMP_Text nameText;
    public TMP_Text bioText;
    public TMP_Text statText;
    public Image portraitImage;

    LayerMask layerMask;

    public TMP_Text r;
    public int rock = 0;

    void OnEnable()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        // Set up LayerMask for ground and units
        layerMask = (1 << LayerMask.NameToLayer("ground")) | (1 << LayerMask.NameToLayer("unit"));
    }

    void Update()
    {
        // Update rock count text
        r.text = "Rocks: " + rock.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpacebarPressed?.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(mousePositionRay, out hitInfo, Mathf.Infinity, layerMask))
            {
                if (UnityEngine.AI.NavMesh.SamplePosition(hitInfo.point, out UnityEngine.AI.NavMeshHit navHit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
                {
                    if (hitInfo.collider.CompareTag("ground") && selectedUnit != null)
                    {
                        selectedUnit.nma.SetDestination(navHit.position);
                    }
                    else if (hitInfo.collider.CompareTag("Unit"))
                    {
                        SelectUnit(hitInfo.collider.gameObject.GetComponent<Unit>());
                    }
                }
            }
        }
    }

    public void OpenCharacterSheet()
    {
        if (selectedUnit == null) return;

        nameText.text = selectedUnit.unitName;
        bioText.text = selectedUnit.bio;
        statText.text = selectedUnit.stats;

        popUpWindow.SetActive(true);
    }

    public void SelectUnit(Unit unit)
    {
        UnitClicked?.Invoke(unit);
        selectedUnit = unit;
    }

    public void ClosePopUpWindow()
    {
        popUpWindow.SetActive(false);
    }
}
