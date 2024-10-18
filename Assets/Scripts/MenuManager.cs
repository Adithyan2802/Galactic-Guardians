using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private Collider rocketCollider;
    public Button startButton;
    public GameObject filler;

    private void Awake()
    {
        rocketCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        filler.GetComponent<Menu>().StartFill();
        startButton.Select();
    }

    private void OnTriggerExit(Collider other)
    {
        filler.GetComponent<Menu>().StopFill();
        EventSystem.current.SetSelectedGameObject(null);
    }

}
