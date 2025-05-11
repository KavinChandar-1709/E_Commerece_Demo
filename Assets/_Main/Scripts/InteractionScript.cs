using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    [Header("Toggle Interaction")]
    public bool isInteractionActive = true;

    [Header("Rotation Settings")]
    public float rotationSpeed = 0.2f;

    [Header("Scaling Settings")]
    public float scaleSpeed = 0.01f;
    public float minScale = 0.5f;
    public float maxScale = 2f;

    private float previousPinchDistance = 0f;
    private Vector3 previousMousePosition;

    void Update()
    {
        if (!isInteractionActive) return;

#if UNITY_EDITOR
        HandleEditorInput();
#else
        HandleTouchInput();
#endif
    }

    void HandleEditorInput()
    {
        // Rotate with mouse drag
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - previousMousePosition;
            transform.Rotate(0f, -delta.x * rotationSpeed, 0f, Space.World);
        }
        previousMousePosition = Input.mousePosition;

        // Scale with scroll
        float scroll = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scroll) > 0.01f)
        {
            float scaleFactor = 1 + scroll * scaleSpeed;
            Vector3 newScale = transform.localScale * scaleFactor;
            transform.localScale = ClampScale(newScale);
        }
    }

    void HandleTouchInput()
    {
        // Rotate with one finger
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = touch.deltaPosition.x;
                transform.Rotate(0f, -deltaX * rotationSpeed, 0f, Space.World);
            }
        }

        // Scale with two fingers
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            float currentDistance = Vector2.Distance(touch0.position, touch1.position);

            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                if (previousPinchDistance != 0f)
                {
                    float deltaDistance = currentDistance - previousPinchDistance;
                    float scaleFactor = 1 + deltaDistance * scaleSpeed;

                    Vector3 newScale = transform.localScale * scaleFactor;
                    transform.localScale = ClampScale(newScale);
                }
                previousPinchDistance = currentDistance;
            }

            if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended)
            {
                previousPinchDistance = 0f;
            }
        }
    }

    Vector3 ClampScale(Vector3 scale)
    {
        float x = Mathf.Clamp(scale.x, minScale, maxScale);
        float y = Mathf.Clamp(scale.y, minScale, maxScale);
        float z = Mathf.Clamp(scale.z, minScale, maxScale);
        return new Vector3(x, y, z);
    }

}