using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    public enum Mode { Rotate, Scale }
    public Mode currentMode = Mode.Rotate;

    public float rotationSpeed = 0.2f;
    public float scaleSpeed = 0.01f;
    public float minScale = 0.5f;
    public float maxScale = 2f;

    private float previousDistance = 0f;

    void Update()
    {
        if (Input.touchCount == 1 && currentMode == Mode.Rotate)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float deltaX = touch.deltaPosition.x;
                transform.Rotate(0f, -deltaX * rotationSpeed, 0f, Space.World);
            }
        }
        else if (Input.touchCount == 2 && currentMode == Mode.Scale)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Find the distance between the two touches in the current frame and the previous frame.
            float currentDistance = Vector2.Distance(touch0.position, touch1.position);

            if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                if (previousDistance != 0)
                {
                    float deltaDistance = currentDistance - previousDistance;
                    float scaleFactor = 1 + deltaDistance * scaleSpeed;

                    Vector3 newScale = transform.localScale * scaleFactor;
                    newScale = ClampScale(newScale, minScale, maxScale);
                    transform.localScale = newScale;
                }

                previousDistance = currentDistance;
            }

            if (touch0.phase == TouchPhase.Ended || touch1.phase == TouchPhase.Ended)
            {
                previousDistance = 0f;
            }
        }
    }

    private Vector3 ClampScale(Vector3 scale, float min, float max)
    {
        float clampedX = Mathf.Clamp(scale.x, min, max);
        float clampedY = Mathf.Clamp(scale.y, min, max);
        float clampedZ = Mathf.Clamp(scale.z, min, max);
        return new Vector3(clampedX, clampedY, clampedZ);
    }

    // Optional: Call this from a UI button to switch modes
    public void SetMode(int modeIndex)
    {
        currentMode = (Mode)modeIndex;
    }
}