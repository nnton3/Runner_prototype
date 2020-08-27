using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : LeanDragTranslate
{
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

            // Convert back to world space
            Vector3 newPos = transform.position;
            newPos.x = camera.ScreenToWorldPoint(screenPoint).x;
            if (newPos.x < -4.5f) return;
            if (newPos.x > 4.5f) return;
            transform.position = newPos;
        }
        else
        {
            Debug.LogError("Failed to find camera. Either tag your camera as MainCamera, or set one in this component.", this);
        }
    }
}
