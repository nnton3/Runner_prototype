using UnityEngine;
using UniRx;
using System.Collections.Generic;
using UnityEditor;

public class PlayerTowerController : MonoBehaviour
{
    public int TowerHeight { get; private set; }
    private List<GameObject> FloorsList = new List<GameObject>();
    [SerializeField] private GameObject floorPref;
    [SerializeField] private Transform tower;

    public void Initialize()
    {
        FloorsList.Add(GameObject.Find("Center"));
    }

    public void AddFloor()
    {
        foreach (var floor in FloorsList)
        {
            var pos = floor.transform.localPosition;
            pos.y++;
            pos.x = 0f;
            pos.z = 0f;
            floor.transform.localPosition = pos;
        }
        var instance = Instantiate(floorPref, tower);
        FloorsList.Add(instance);

        TowerHeight++;
    }

    public void RemoveFloor(GameObject floor)
    {
        floor.transform.SetParent(null);
        FloorsList.Remove(floor);
        TowerHeight--;
    }
}
