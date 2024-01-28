using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private int maxObjects = 5;
    [SerializeField] private List<GameObject> movingObjectPrefabs;
    [SerializeField] private List<GameObject> nonMovingObjectPrefabs;
    [SerializeField] private Transform respawnPoint;

    private Collider2D areaCollider;
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

    private void Start()
    {
        areaCollider = GetComponent<Collider2D>();
        if (areaCollider == null)
        {
            Debug.LogError("RespawnManager requires a Collider2D component on the GameObject.");
        }

        // Store the original positions of all objects
        foreach (var obj in movingObjectPrefabs)
        {
            if (obj != null)
            {
                originalPositions[obj] = obj.transform.position;
            }
        }

        foreach (var obj in nonMovingObjectPrefabs)
        {
            if (obj != null)
            {
                originalPositions[obj] = obj.transform.position;
            }
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

            int objectsToRespawn = maxObjects - objectCount;
            if (objectsToRespawn > 0)
            {
                RespawnRandomObjects(objectsToRespawn);
            }
        }
    }

    private void RespawnRandomObjects(int numberOfObjects)
    {
        List<GameObject> combinedList = new List<GameObject>(movingObjectPrefabs);
        combinedList.AddRange(nonMovingObjectPrefabs);

        for (int i = 0; i < numberOfObjects; i++)
        {
            if (combinedList.Count > 0)
            {
                GameObject prefab = combinedList[Random.Range(0, combinedList.Count)];
                Vector3 spawnPosition;

                if (movingObjectPrefabs.Contains(prefab))
                {
                    // If it's a moving object, maintain its original Y position
                    spawnPosition = new Vector3(respawnPoint.position.x, originalPositions[prefab].y, respawnPoint.position.z);
                }
                else
                {
                    // If it's a non-moving object, use its original position
                    spawnPosition = originalPositions[prefab];
                }

                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}
