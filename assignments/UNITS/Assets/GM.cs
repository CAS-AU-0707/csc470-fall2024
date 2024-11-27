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

    int here = 0;
    int linesP = 0;
    int stars = 1;
    public List<Lines2> lines = new List<Lines2>();
    public TMP_Text starText;

    public TMP_Text r;
    public int rock = 0;
    public int uRock = 1;

    public TMP_Text turn;
    int turns =11;

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
        turn.text = "Clicks " + turns.ToString();
        popUpWindow.SetActive(false);
        // Set up LayerMask for ground and units
        layerMask = (1 << LayerMask.NameToLayer("ground")) | (1 << LayerMask.NameToLayer("unit"));
    }

    void Update()
    {
        turn.text = "Clicks " + turns.ToString();
        here = 0;
        linesP = 0;
        foreach (Unit u in units)
        {
            if (u.here() == true)
            {
                here++;
            }
        }

        if (here >= 4)
        {
            foreach (Lines2 x in lines)
            {
                if(x.checkPassed() == true)
                {
                    linesP++;
                }
            }

            if (linesP >= 2)
            {
                stars = 2;
                if (linesP >= 4)
                {
                    stars = 3;
                }
            }

            starText.text = "You got " + stars.ToString() + " / 3 Stars";
            popUpWindow.SetActive(true);
        }

        // Update rock count text
        //r.text = "Rocks: " + rock.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpacebarPressed?.Invoke();
        }

        if (Input.GetMouseButtonDown(0) && turns >= 1)
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
            turns--;
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
