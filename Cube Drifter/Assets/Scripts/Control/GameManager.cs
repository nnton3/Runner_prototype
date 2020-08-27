using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public LvlConfig mainConfig;
    private RoadTilePool tilePool;
    private RoadGenerator roadGenerator;
    private PlayerMover playerMover;
    private MoveForward moveForward;
    private Treasure treasure;
    private PlayerTowerController towerController;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Initialize();
    }

    private void Initialize()
    {
        moveForward = FindObjectOfType<MoveForward>();
        if (moveForward != null) moveForward.Initialize(mainConfig.Speed);

        tilePool = GetComponent<RoadTilePool>();
        if (tilePool != null) tilePool.Initialize(mainConfig.Speed);

        roadGenerator = GetComponent<RoadGenerator>();
        if (roadGenerator != null) roadGenerator.Initialize(mainConfig.Speed);

        playerMover = FindObjectOfType<PlayerMover>();
        if (playerMover != null) playerMover.Initialize(mainConfig.Speed);

        treasure = FindObjectOfType<Treasure>();
        if (treasure != null) treasure.Initialize();

        towerController = FindObjectOfType<PlayerTowerController>();
        if (towerController != null) towerController.Initialize();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
