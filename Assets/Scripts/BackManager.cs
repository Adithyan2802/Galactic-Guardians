using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackManager : MonoBehaviour
{
    private Collider rocketCollider;
    public Button bckBtn;

    private void Awake()
    {
        rocketCollider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        bckBtn.GetComponent<GameOver>().StartMFill();
        bckBtn.Select();
    }

    private void OnTriggerExit(Collider other)
    {
        bckBtn.GetComponent<GameOver>().StopFill();
        EventSystem.current.SetSelectedGameObject(null);
    }
}
