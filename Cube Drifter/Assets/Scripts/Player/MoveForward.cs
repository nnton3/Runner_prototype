using UnityEngine;
using UniRx;

public class MoveForward : MonoBehaviour
{
    private float speed = 5f;
    public Vector3 Direction { get; private set; }
    public Vector3 CenterPoint { get; private set; }

    public void Initialize(float lvlspeed)
    {
        Direction = Vector3.forward;
        speed = lvlspeed * 0.05f;
#if UNITY_EDITOR
        speed = lvlspeed * 0.047f;
#endif
        Observable.EveryUpdate()
            .Subscribe(_ => transform.Translate(Vector3.forward * speed))
            .AddTo(this);
    }

    private  void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            speed = 0.047f * speed;
            Observable.EveryUpdate()
                .Subscribe(_ => transform.Translate(Vector3.forward * speed))
                .AddTo(this);
        }
    }

    public void Rotate(Vector3 newDirection, Vector3 turnPivot)
    {
        var rot = Quaternion.LookRotation(transform.TransformDirection(newDirection));
        var eulers = rot.eulerAngles;

        CenterPoint = turnPivot + Direction * 5;
        Direction = transform.TransformDirection(newDirection);

        iTween.RotateTo(gameObject, eulers, 2f);
    }
}
