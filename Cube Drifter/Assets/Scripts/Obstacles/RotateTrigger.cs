using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrigger : MonoBehaviour
{
    [SerializeField] private Vector3 dir;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<MoveForward>())
            other.GetComponent<MoveForward>().Rotate(dir, transform.position);
    }
}
