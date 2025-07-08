using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public Transform player; // Assign your player transform in the Inspector

    // Set these in the Inspector to match your wall/level bounds
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private float camHalfWidth;
    private float camHalfHeight;

    public BoxCollider2D levelBounds;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = cam.aspect * camHalfHeight;

        if (levelBounds != null)
        {
            Bounds bounds = levelBounds.bounds;
            minBounds = bounds.min;
            maxBounds = bounds.max;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPosition = player.position;
            newPosition.z = transform.position.z; // Keep original camera z

            // Clamp camera position so it doesn't exceed the bounds
            float clampedX = Mathf.Clamp(newPosition.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
            float clampedY = Mathf.Clamp(newPosition.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);

            transform.position = new Vector3(clampedX, clampedY, newPosition.z);
        }
    }
}