using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float wallSegmentsCount;
    public GameObject wallSegmentPrefab;
    public GameObject swordWallSegmentPrefab;
    public GameObject spikyWallLeft, spikyWallRight;
    public GameObject swordPrefab;
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

    private string trapsOnLevel;
    private List<string> tempObstaclesList = new List<string>();
    private string[] tempTrapsOnLevel = new string[0];
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("level", 1);
        wallSegmentsCount += currentLevel;

        trapsOnLevel = PlayerPrefs.GetString("trapsOnLevels", "");

        InstantiateWalls();
        InstantiateObstacles();
        InstantiateFloor();
        InstantiateBackground();
    }

    private void InstantiateWalls()
    {
        for (int i = 0; i < wallSegmentsCount; i++)
        {
            Transform wallSegmentRight;
            Transform wallSegmentLeft;

            if (i % 7 == 0 && Random.Range(0, 101) > 90 && i < wallSegmentsCount - 5)
            {
                wallSegmentRight = Instantiate(swordWallSegmentPrefab, new Vector3(3.25f, -0.5f * i, -0.2f), Quaternion.identity).transform;
                wallSegmentLeft = Instantiate(swordWallSegmentPrefab, new Vector3(-3.25f, -0.5f * i, -0.2f), Quaternion.identity).transform;
                wallSegmentLeft.GetComponent<SwordThrow>().sword = swordPrefab;
            }
            else if(Random.Range(0, 101) > 80 && i < wallSegmentsCount - 5)
            {
                wallSegmentRight = Instantiate(spikyWallRight, new Vector3(3.25f, -0.5f * i, 0), Quaternion.identity).transform;
                wallSegmentLeft = Instantiate(spikyWallLeft, new Vector3(-3.25f, -0.5f * i, 0), Quaternion.identity).transform;
            }
            else
            {
                wallSegmentRight = Instantiate(wallSegmentPrefab, new Vector3(3.25f, -0.5f * i, 0), Quaternion.identity).transform;
                wallSegmentLeft = Instantiate(wallSegmentPrefab, new Vector3(-3.25f, -0.5f * i, 0), Quaternion.identity).transform;
            }

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

                Transform newObstacle;

                if (trapsOnLevel.Length == 0)
                {
                    //int obstacleIndexFromString = i / 10;

                    tempObstaclesList.Add(randomObstacle.ToString());
                    if (randomObstacle == 1) //BOILER
                    {
                        randomPosX = Random.Range(minObstaclePosX / 1.1f, maxObstaclePosX / 1.1f);
                        newObstacle = Instantiate(obstacles[randomObstacle], new Vector3(randomPosX, firstObstaclePosition - i / 2, 0), Quaternion.identity).transform;
                    }
                    else if (randomObstacle == 2) //LASER
                    {
                        randomPosX = Random.Range(minObstaclePosX / 1.5f, maxObstaclePosX / 1.5f);
                        newObstacle = Instantiate(obstacles[randomObstacle], new Vector3(randomPosX, firstObstaclePosition - i / 2, 0), Quaternion.identity).transform;
                    }
                    else if (randomObstacle == 3) //SPIKY BOX
                    {
                        randomPosX = Random.Range(minObstaclePosX / 1.5f, maxObstaclePosX / 1.5f);
                        newObstacle = Instantiate(obstacles[randomObstacle], new Vector3(randomPosX, firstObstaclePosition - i / 2, 0), Quaternion.identity).transform;
                    }
                    else
                        newObstacle = Instantiate(obstacles[randomObstacle], new Vector3(randomPosX, firstObstaclePosition - i / 2, 0), Quaternion.identity).transform;

                    tempObstaclesList.Add(newObstacle.transform.position.x.ToString());
                }
                else
                {
                    tempTrapsOnLevel = trapsOnLevel.Split('|');
                    int obstacleIndexFromString = i / 10;
                    randomObstacle = int.Parse(tempTrapsOnLevel[obstacleIndexFromString * 2].ToString());
                    newObstacle = Instantiate(obstacles[randomObstacle], new Vector3(float.Parse(tempTrapsOnLevel[(obstacleIndexFromString * 2) + 1].ToString()), firstObstaclePosition - i / 2, 0), Quaternion.identity).transform;
                }

                newObstacle.parent = obstaclesHolder;
            }
        }
        if (trapsOnLevel.Length == 0)
        {
            tempTrapsOnLevel = new string[tempObstaclesList.Count];
            for(int i = 0; i < tempTrapsOnLevel.Length; i++)
            {
                tempTrapsOnLevel[i] = tempObstaclesList[i];
            }
            trapsOnLevel = string.Join("|", tempTrapsOnLevel);
        }
        PlayerPrefs.SetString("trapsOnLevels", trapsOnLevel);
    }

    private void InstantiateFloor()
    {
        Instantiate(floor, new Vector3(0, -wallSegmentsCount / 2 - 0.12f, 0), Quaternion.identity);
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
// 0    1    2     3    4    5     6     7
//[1],[0.2],[3],[2.24],[4],[2.1], [2], [1.1]
// 0         1          2          3