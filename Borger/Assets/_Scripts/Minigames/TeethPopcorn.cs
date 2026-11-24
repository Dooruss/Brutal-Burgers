using System.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Android;
using UnityEngine.InputSystem.iOS;
using UnityEngine.InputSystem.LowLevel;

public class TeethPopcorn : MonoBehaviour
{
    [Header("Tooth Spawning")]
    [SerializeField] private GameObject ToothPrefab;
    [SerializeField] private Transform ToothSpawnPoint;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float shakeThreshold = 1f;
    private bool isDragging;
    private Vector3 mouseOffset;
    private float dragDepth;
    private Vector3 previousPosition;
    private Vector3 previousMovement;
    private float shakeAmount;

    private void Update()
    {
        if (!isDragging || Camera.main == null) return;

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = dragDepth;

        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition) + mouseOffset;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector3 movement = transform.position - previousPosition;

        if (movement.sqrMagnitude > 0.000001f)
        {
            if (previousMovement.sqrMagnitude > 0.000001f && Vector3.Dot(movement, previousMovement) < 0f)
            {
                shakeAmount += movement.magnitude;
            }

            previousMovement = movement;
        }

        if (shakeAmount >= shakeThreshold)
        {
            LetToothFall();
            shakeAmount = 0f;
        }

        previousPosition = transform.position;
    }

    void LetToothFall()
    {
        if (ToothPrefab == null || ToothSpawnPoint == null) return;

        Instantiate(ToothPrefab, ToothSpawnPoint.position, Quaternion.identity);
    }

    private void OnMouseDown()
    {
        if (Camera.main == null) return;

        isDragging = true;
        shakeAmount = 0f;
        previousMovement = Vector3.zero;
        previousPosition = transform.position;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        dragDepth = screenPosition.z;
        mousePosition.z = dragDepth;

        mouseOffset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        shakeAmount = 0f;
        previousMovement = Vector3.zero;
    }
}
