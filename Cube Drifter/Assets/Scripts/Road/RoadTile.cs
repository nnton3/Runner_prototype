using System.Collections;
using UnityEngine;

public class RoadTile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    //private void OnEnable()
    //{
    //    StartCoroutine(LifeTime());
    //}

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }
}
