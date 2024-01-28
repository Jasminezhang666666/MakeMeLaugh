using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private int maxObjects = 5;
    [SerializeField] private List<GameObject> objectPrefabs;
    [SerializeField] private Transform respawnPoint;

    private Collider2D areaCollider;

    private void Start()
    {
        areaCollider = GetComponent<Collider2D>();
        if (areaCollider == null)
        {
            Debug.LogError("RespawnManager requires a Collider2D component on the GameObject.");
        }
    }

    private void Update()
    {
        if (areaCollider != null)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(areaCollider.bounds.center, areaCollider.bounds.size, 0);
            int objectCount = 0;

            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Object"))
                {
                    objectCount++;
                }
            }

            if (objectCount < maxObjects)
            {
                RespawnObjects(maxObjects - objectCount);
            }
        }
    }

    private void RespawnObjects(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            if (objectPrefabs.Count > 0)
            {
                GameObject prefab = objectPrefabs[Random.Range(0, objectPrefabs.Count)];
                Vector3 spawnPosition = new Vector3(respawnPoint.position.x, respawnPoint.position.y + prefab.transform.position.y, respawnPoint.position.z);
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
