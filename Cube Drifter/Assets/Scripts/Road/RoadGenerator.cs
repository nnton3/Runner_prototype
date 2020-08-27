using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    #region Variables
    [SerializeField] private float speed = 1f;
    [SerializeField] private int cyrcleLength = 4;
    [SerializeField] private GameObject pointsPref;
    [SerializeField] private GameObject obstaclePref;
    [SerializeField] private GameObject turnLeft;
    [SerializeField] private GameObject turnRight;
    [SerializeField] private GameObject resetTrigger;
    private MoveForward moveForward;
    private RoadTilePool tilePool;
    private ObstacleFactory obstacleFactory;
    private Vector3 currentPosition = Vector3.zero;
    private int tileNumerator;
    private Vector3 direction = Vector3.forward;
    int numerator = 0;
    #endregion

    public void Initialize(float lvlSpeed)
    {
        tilePool = FindObjectOfType<RoadTilePool>();
        obstacleFactory = FindObjectOfType<ObstacleFactory>();
        moveForward = FindObjectOfType<MoveForward>();

        speed = lvlSpeed;
    }

    private void Start()
    {
        GenerateExampleRoad();
    }

    private void GenerateExampleRoad()
    {
        DirectRoad(10);

        AddTurnLeft();

        DirectRoad(10);

        AddTurnRight();

        DirectRoad(10);

        AddTurnLeft();

        DirectRoad(10);

        AddResetTrigger();
    }

    private void AddResetTrigger()
    {
        var trigger = Instantiate(resetTrigger);
        trigger.transform.position = currentPosition;
        trigger.transform.rotation = Quaternion.LookRotation(direction);
    }

    private void DirectRoad(int length)
    {
        for (int i = 0; i < length; i++)
        {
            GenerateRoadTile();
            if (i % 5 == 0)
            {
                AddObstacle(numerator);
                numerator++;
                if (numerator > 2) numerator = 0;
            }
            else
                AddFloorPoints();
        }
    }

    private void AddTurnRight()
    {
        var tile = Instantiate(turnRight);
        tile.SetActive(true);
        tile.transform.position = currentPosition;
        tile.transform.rotation = Quaternion.LookRotation(direction);
        currentPosition += direction * 5;
        direction = GetNewDir();
        currentPosition += direction * 5;
        tileNumerator++;
    }

    private void AddTurnLeft()
    {
        var tile = Instantiate(turnLeft);
        tile.SetActive(true);
        tile.transform.position = currentPosition;
        tile.transform.rotation = Quaternion.LookRotation(direction);
        currentPosition += direction * 5;
        direction = GetNewDir();
        currentPosition += direction * 5;
        tileNumerator++;
    }

    private void GenerateRoadTile()
    {
        var tile = tilePool.GetTile(TileType.Default);
        tile.SetActive(true);
        tile.transform.position = currentPosition;
        tile.transform.rotation = Quaternion.LookRotation(direction);
        currentPosition += direction * 10;
        tileNumerator++;
    }

    private void AddObstacle(int type)
    {
        GameObject obs = null;
        if (type == 0)
            obs = obstacleFactory.GetDefaultObstacle(3);
        else if (type == 1)
            obs = obstacleFactory.GetPerforatedObstacle(6);
        else if (type == 2)
            obs = obstacleFactory.GetWheelsObstacle(5);

        if (obs == null) return;
        obs.transform.position = currentPosition;
        obs.transform.rotation = Quaternion.LookRotation(direction);
    }

    private void AddFloorPoints()
    {
        var contentPos = currentPosition;

        if (direction == Vector3.forward || direction == Vector3.back)
            contentPos.x += moveForward.CenterPoint.x + UnityEngine.Random.Range(-4.5f, 4.5f);

        if (direction == Vector3.left || direction == Vector3.right)
            contentPos.z += moveForward.CenterPoint.z + UnityEngine.Random.Range(-4.5f, 4.5f);

        var rot = Quaternion.LookRotation(direction);
        var instance = Instantiate(pointsPref, contentPos, rot);
    }

    private Vector3 GetNewDir()
    {
        if (direction == Vector3.forward)
            return Vector3.left;
        if (direction == Vector3.left)
            return Vector3.forward;
        return Vector3.zero;
    }
}
