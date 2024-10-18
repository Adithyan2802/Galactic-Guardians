using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Collider bombCollider;

    private void Awake()
    {
        bombCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bombCollider.enabled = false;
            FindObjectOfType<GameManager>().Explode(other);
            Destroy(gameObject);
        }
    }
}
