using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float wallSegmentsCount;
    public GameObject wallSegmentPrefab;
    public Transform wallSegmentsHolder;

    [Space]
    public GameObject obstacleCubePrefab;
    public Transform obstaclesHolder;
    public int firstObstaclePosition = -8;
    public int distanceBetweenObstacles = 5;
    public float minObstaclePosX = -2.25f;
    public float maxObstaclePosX = 2.25f;

    [Space]
    public GameObject floor;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateWalls();
        InstantiateObstacles();
        InstantiateFloor();
    }

    private void InstantiateWalls()
    {
        for (int i = 0; i < wallSegmentsCount; i++)
        {
            Transform wallSegmentRight = Instantiate(wallSegmentPrefab, new Vector3(3.25f, -0.5f * i, 0), Quaternion.identity).transform;
            Transform wallSegmentLeft = Instantiate(wallSegmentPrefab, new Vector3(-3.25f, -0.5f * i, 0), Quaternion.identity).transform;
            wallSegmentRight.parent = wallSegmentLeft.parent = wallSegmentsHolder;
        }
    }

    private void InstantiateObstacles()
    {
        for(int i = 0; i < wallSegmentsCount - distanceBetweenObstacles * 4; i++) //чтобы предотвратить появление препятствий в самом низу уровня делаем запас в два препятствия умножая на 4
        {
            if(i % 10 == 0)
            {
                float randomPosX = Random.Range(minObstaclePosX, maxObstaclePosX);
                Transform newCubeObstacle = Instantiate(obstacleCubePrefab, new Vector3(randomPosX, firstObstaclePosition - i / 2, 0), Quaternion.identity).transform;
                newCubeObstacle.parent = obstaclesHolder;
            }
        }
    }

    private void InstantiateFloor()
    {
        Instantiate(floor, new Vector3(0, -wallSegmentsCount / 2 - 0.25f, 0), Quaternion.identity);
    }
}
