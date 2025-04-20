using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverDetector : MonoBehaviour
{
    [SerializeField] private GameObject mouseObject;
    //[SerializeField] private float movementStep = 0.01f; 
    [SerializeField] private float maxLeft = -5f; 
    [SerializeField] private float maxRight = 5f;
    [SerializeField] private float maxUp = 5f; 
    [SerializeField] private float maxDown = -5f; 
    [SerializeField] private LayerMask hoverLayerMask;

    private bool isHovering = false;
    private Vector2 previousMousePos;

    void Start()
    {
        previousMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hits = Physics2D.OverlapPointAll(mousePos, hoverLayerMask);

        bool isCurrentlyHovering = false;

        foreach (var hit in hits)
        {
            if (hit.gameObject == gameObject)
            {
                isCurrentlyHovering = true;
                break;
            }
        }

        if (isCurrentlyHovering && !isHovering)
        {
            Debug.Log("Mouse entered the object");
        }
        else if (!isCurrentlyHovering && isHovering)
        {
            Debug.Log("Mouse exited the object");
        }

        isHovering = isCurrentlyHovering;

        if (isHovering)
        {
            // Calculate the movement direction
            Vector3 direction = mousePos - previousMousePos;

            // Incremental movement
            Vector3 newPosition = mouseObject.transform.position + direction * 0.55f;

            // Check boundaries
            newPosition.x = Mathf.Clamp(newPosition.x, maxLeft, maxRight);
            newPosition.y = Mathf.Clamp(newPosition.y, maxDown, maxUp);

            // Update the position of mouseObject
            mouseObject.transform.position = newPosition;

            // Save the current mouse position
            previousMousePos = mousePos;
        }

    }
}
