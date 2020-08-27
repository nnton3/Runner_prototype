using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PoolData
{
    public int count;
    public GameObject tilePref;
    public List<GameObject> tileList;
}

public enum TileType { Default = 0, Turn = 1 }

public class RoadTilePool : MonoBehaviour
{
    [SerializeField] private List<PoolData> tilePools;

    public void Initialize(float lvlSpeed)
    {
        FillPools(lvlSpeed);
    }

    private void FillPools(float lvlSpeed)
    {
        foreach (var pool in tilePools)
            for (int i = 0; i < pool.count; i++)
            {
                var instance = Instantiate(pool.tilePref);
                pool.tileList.Add(instance);
                instance.SetActive(false);
            }
    }

    public GameObject GetTile(TileType type)
    {
        return GetTile(tilePools[(int)type]);
    }

    private GameObject GetTile(PoolData poolData)
    {
        GameObject freeTile = null;
        poolData.tileList.ForEach(tile =>
        {
            if (!tile.activeSelf)
                freeTile = tile;
        });

        if (freeTile == null)
        {
            freeTile = Instantiate(poolData.tilePref);
            poolData.tileList.Add(freeTile);
        }

        return freeTile;
    }
}
