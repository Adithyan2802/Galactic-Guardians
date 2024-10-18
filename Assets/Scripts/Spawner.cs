using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;

    public GameObject levelDisplay;

    public GameObject[] fruitPrefabs;

    public GameObject[] bombPrefabs;

    // [Range(0f, 1f)]
    public float bombChance = 0.05f;

    public float minSpawnDelay = 1.2f;
    public float maxSpawnDelay = 2.4f;

    public float minAngle = -12f;
    public float maxAngle = 12f;

    public float minForce = 5f;
    public float maxForce = 7f;

    public float maxLife = 5f;

    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        levelDisplay.SetActive(true);
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(7f);
        levelDisplay.SetActive(false);
        while (enabled)
        {
            GameObject prefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];

            if (Random.value < bombChance)
            {
                prefab = bombPrefabs[Random.Range(0, bombPrefabs.Length)];
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(-10, 10);
            position.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            position.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy(fruit, maxLife);

            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }

}
