using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [SerializeField] private int maxObjects = 5;
    [SerializeField] private List<GameObject> movingObjectPrefabs;
    [SerializeField] private List<GameObject> nonMovingObjectPrefabs;
    [SerializeField] private Transform movingRespawnPoint;
    [SerializeField] private Transform nonMovingRespawnArea;
    [SerializeField] private Vector2 nonMovingAreaSize = new Vector2(5f, 5f);

    private Collider2D areaCollider;
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

    private void Start()
    {
        areaCollider = GetComponent<Collider2D>();
        if (areaCollider == null)
        {
            Debug.LogError("RespawnManager requires a Collider2D component on the GameObject.");
        }

        // Store the original positions of the moving objects
        foreach (var obj in movingObjectPrefabs)
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
                    // For moving objects, use the moving respawn point
                    spawnPosition = new Vector3(movingRespawnPoint.position.x, originalPositions[prefab].y, movingRespawnPoint.position.z);
                }
                else
                {
                    // For non-moving objects, respawn within the non-moving area
                    spawnPosition = GetRandomPositionInArea(nonMovingRespawnArea.position, nonMovingAreaSize);
                }

                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private Vector3 GetRandomPositionInArea(Vector3 center, Vector2 size)
    {
        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        return new Vector3(randomX, randomY, center.z);
    }
}
