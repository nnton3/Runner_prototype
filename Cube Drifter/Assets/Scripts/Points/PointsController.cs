using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    private PlayerTowerController towerController;
    private int points;

    private void Awake()
    {
        towerController = FindObjectOfType<PlayerTowerController>();
        points = GetPoints();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < points; i++)
                towerController.AddFloor();
            
            Destroy(gameObject);
        }
    }

    public int GetPoints()
    {
        return transform.childCount;
    }
}
