using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidBody;
    private Collider[] fruitCollider;

    private void Awake()
    {
        fruitRigidBody = GetComponent<Rigidbody>();
        fruitCollider = GetComponents<Collider>();
    }
    private void Slice()
    {
        FindObjectOfType<GameManager>().IncreaseScore();

        whole.SetActive(false);
        sliced.SetActive(true);

        foreach (Collider c in fruitCollider)
        {
            c.enabled = false;
        }

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = fruitRigidBody.velocity;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Slice();
        }
    }
}
