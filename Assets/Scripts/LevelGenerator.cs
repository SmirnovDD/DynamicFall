using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float wallSegmentsCount;
    public GameObject wallSegmentPrefab;
    public Transform wallSegmentsHolder;

    [Space]
    public GameObject[] obstacles;
    public Transform obstaclesHolder;
    public int firstObstaclePosition = -8;
    public int distanceBetweenObstacles = 5;
    public float minObstaclePosX = -2.25f;
    public float maxObstaclePosX = 2.25f;

    [Space]
    public GameObject floor;

    [Space]
    public GameObject background;
    public Transform backgroundHolder;

    [HideInInspector]
    public int currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("level", 1);
        wallSegmentsCount += currentLevel;

        InstantiateWalls();
        InstantiateObstacles();
        InstantiateFloor();
        InstantiateBackground();
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
                int randomObstacle = Random.Range(0, obstacles.Length);
                Transform newObstacle = Instantiate(obstacles[randomObstacle], new Vector3(randomPosX, firstObstaclePosition - i / 2, 0), Quaternion.identity).transform;
                newObstacle.parent = obstaclesHolder;
            }
        }
    }

    private void InstantiateFloor()
    {
        Instantiate(floor, new Vector3(0, -wallSegmentsCount / 2 - 0.15f, 0), Quaternion.identity);
    }

    private void InstantiateBackground()
    {
        Transform firstBg = Instantiate(background, new Vector3(0, 7.5f, 0.5f), Quaternion.identity).transform;
        firstBg.parent = backgroundHolder;

        for (int i = 0; i < wallSegmentsCount + 15; i++)
        {
            if(i % 15 == 0)
            {
                Transform newBg = Instantiate(background, new Vector3(0, -0.5f * i, 0.5f), Quaternion.identity).transform;
                newBg.parent = backgroundHolder;
            }
        }
    }
}
