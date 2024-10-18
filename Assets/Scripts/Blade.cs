using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera mainCam;
    private Collider bladeCollider;
    private bool slicing;
    private TrailRenderer bladeTrail;

    public Vector3 direction { get; private set; }
    public float sliceForce = 5f;
    public float minSliceVelocity = 0.02f;

    private void Awake()
    {
        mainCam = Camera.main;
        bladeCollider = GetComponent<Collider>();
        bladeTrail = GetComponentInChildren<TrailRenderer>();
    }
    private void OnEnable()
    {
        StopSlicing();
    }
    private void OnDisable()
    {
        StopSlicing();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        Vector3 position = mainCam.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;

        transform.position = position;

        slicing = true;
        bladeCollider.enabled = true;
        bladeTrail.enabled = true;
        bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        slicing = false;
        bladeCollider.enabled = false;
        bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 position = mainCam.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0f;

        direction = position - transform.position;

        float velocity = direction.magnitude / Time.deltaTime;
        bladeCollider.enabled = velocity > minSliceVelocity;

        transform.position = position;

    }
}
