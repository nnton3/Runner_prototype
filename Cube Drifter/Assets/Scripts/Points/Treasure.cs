using UnityEngine;
using System.Collections;
using UniRx;

public class Treasure : MonoBehaviour
{
    public ReactiveProperty<int> Points;

    public void Initialize()
    {
        Points = new ReactiveProperty<int>(0);
    }

    public void AddPoints(int points)
    {
        Points.Value += points;
    }

    public void RemovePoints(int points) => Points.Value -= points;
}
