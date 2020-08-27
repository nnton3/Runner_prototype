using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private PlayerTowerController towerController;

    private void Awake()
    {
        towerController = FindObjectOfType<PlayerTowerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
            towerController.RemoveFloor(other.gameObject);
    }
}
