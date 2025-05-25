using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private InputManager inputManager;
    private int direction = 0;
    [SerializeField] private int moveDistance = 1;
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (!isMoving && inputManager.direction != 0)
        {
            direction = inputManager.direction;
            SetTargetPosition();
            inputManager.direction = 0;
        }

        MoveToTarget();
    }

    private void SetTargetPosition()
    {
        Vector3 offset = Vector3.zero;

        switch (direction)
        {
            case 1:
                offset = new Vector3(0, 0, moveDistance);
                break;
            case 2:
                offset = new Vector3(moveDistance, 0, 0);
                break;
            case 3:
                offset = new Vector3(0, 0, -moveDistance);
                break;
            case 4:
                offset = new Vector3(-moveDistance, 0, 0);
                break;
        }

        targetPosition = transform.position + offset;
        isMoving = true;
    }

    private void MoveToTarget()
    {
        if (!isMoving) return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }
}
