using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Vector3 moveVector;
    private float direction;
    private float sensitive = 5f;

    public void Initialize(float lvlSpeed)
    {
        sensitive = lvlSpeed * 1.5f;
    }

    private void Start()
    {
        SetPlayerMovespeed();
    }

    private void SetPlayerMovespeed()
    {
        Observable.EveryUpdate()
            .Subscribe(_ => TryToMove())
            .AddTo(this);
    }

    private void TryToMove()
    {
        moveVector = Vector3.zero;

        if (direction != 0f)
            moveVector = (direction > 0) ? Vector3.right : Vector3.left;
        
        if (!CanMove(moveVector.x))
            moveVector = Vector3.zero;

        moveVector = transform.TransformDirection(moveVector);
        transform.Translate(moveVector * sensitive * Time.fixedDeltaTime);
    }

    public void MoveTo(float _direction)
    {
        direction = _direction;
    }

    private bool CanMove(float _moveVector)
    {
        if (_moveVector > 0f && transform.localPosition.x > 4.5f) return false;
        if (_moveVector < 0f && transform.localPosition.x < -4.5f) return false;

        return true;
    }
}
