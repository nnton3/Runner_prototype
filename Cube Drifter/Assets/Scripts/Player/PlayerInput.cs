using Lean.Touch;
using UnityEngine;

public class PlayerInput : LeanDragTranslate
{
    private MoveForward moveForward;

    protected override void Awake()
    {
        base.Awake();
        moveForward = FindObjectOfType<MoveForward>();
    }

    protected override void Translate(Vector2 screenDelta)
    {
        // Make sure the camera exists
        var camera = LeanTouch.GetCamera(Camera, gameObject);

        if (camera != null)
        {
            // Screen position of the transform
            var screenPoint = camera.WorldToScreenPoint(transform.position);

            // Add the deltaPosition
            screenPoint += (Vector3)screenDelta;

            Vector3 pos = transform.position;
            Vector3 targetPoint = camera.ScreenToWorldPoint(screenPoint);

            if (Mathf.Abs(moveForward.Direction.z) > 0.5f)
            {
                pos.x = targetPoint.x;
                if (!InRange(pos.x, moveForward.CenterPoint.x)) return;
            }
            else if (Mathf.Abs(moveForward.Direction.x) > 0.5f)
            {
                pos.z = targetPoint.z;
                if (!InRange(pos.z, moveForward.CenterPoint.z)) return;
            }

            transform.position = pos;
        }
        else
        {
            Debug.LogError("Failed to find camera. Either tag your camera as MainCamera, or set one in this component.", this);
        }
    }

    private bool InRange(float targetValue, float centerValue)
    {
        if (targetValue < centerValue - 4.5f) return false;
        if (targetValue > centerValue + 4.5f) return false;

        return true;
    }
}