using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RestartManager : MonoBehaviour
{
    private Collider rocketCollider;
    public Button restartBtn;

    private void Awake()
    {
        rocketCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        restartBtn.GetComponent<GameOver>().StartRFill();
        restartBtn.Select();
    }

    private void OnTriggerExit(Collider other)
    {
        restartBtn.GetComponent<GameOver>().StopFill();
        EventSystem.current.SetSelectedGameObject(null);
    }
}
