using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public Grid grid; // Assign your Grid component in the inspector
    private Vector3 targetPosition;

    public float moveSpeed = 5f; // Speed of movement

    void Start()
    {
        // Snap to the nearest cell's bottom-left
        Vector3Int cellPos = grid.WorldToCell(transform.position);
        targetPosition = grid.CellToWorld(cellPos); 
        transform.position = targetPosition;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            // Only take input when fully in a cell
            Vector3Int currentCell = grid.WorldToCell(targetPosition);

            if (Input.GetKeyDown(KeyCode.W))
                currentCell += Vector3Int.up;
            else if (Input.GetKeyDown(KeyCode.S))
                currentCell += Vector3Int.down;
            else if (Input.GetKeyDown(KeyCode.A))
                currentCell += Vector3Int.left;
            else if (Input.GetKeyDown(KeyCode.D))
                currentCell += Vector3Int.right;

            targetPosition = grid.CellToWorld(currentCell); // bottom-left of new cell
        }

        // Smooth movement
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
