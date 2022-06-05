using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL = 200f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Hero player;

    private Vector3 lastEndPosition;
    void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        SpawnLevel();
        SpawnLevel();
        int startingSpawnLevels = 2;
        for (int i = 0; i < startingSpawnLevels; i++)
        {
            SpawnLevel();
        }

    }

    private void Update()
    {
        if (Vector3.Distance(player.GetPosition(), lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL)
        {
            SpawnLevel();
        }
    }

    private void SpawnLevel()
    {
        Transform chosenLevel = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevel(chosenLevel,lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevel(Transform levelPart,Vector3 spawnPositon)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPositon, Quaternion.identity);
        return levelPartTransform;
    }
}
