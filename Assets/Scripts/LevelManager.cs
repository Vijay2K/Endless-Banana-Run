using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] platformPrefabs = null;
    [SerializeField] private int numberOfPlatforms = 5;
    [SerializeField] private Transform playerTransform = null;
    [SerializeField] private List<GameObject> activePlatforms = new List<GameObject>();

    private float zOffset = 34.75f;
    private float platformLength = 34.75f;

    private void Start() {
        for(int i = 0; i < numberOfPlatforms; i++) {
            SpawnPlatform(Random.Range(1, platformPrefabs.Length));
        }
    }

    private void Update() {
        if(playerTransform.position.z - 35 > zOffset - (numberOfPlatforms * platformLength)) {
            SpawnPlatform(Random.Range(1, platformPrefabs.Length));
            DeletePlatforms();
        }
    }

    private void DeletePlatforms() {
        Destroy(activePlatforms[0]);
        activePlatforms.RemoveAt(0);
    }

    private void SpawnPlatform(int platformIndex) {
        GameObject spawnedplatforms = Instantiate(platformPrefabs[platformIndex], new Vector3(-2.15f, 0.45f, zOffset), Quaternion.identity);
        activePlatforms.Add(spawnedplatforms);
        zOffset += platformLength;
    }

}
