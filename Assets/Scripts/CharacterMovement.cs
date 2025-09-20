using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float waitTime = 0.2f;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap walkableTilemap;

    private Vector3Int currentCell;
    private float moveCoolDown = 0f;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private bool canMove = true;

    private void Start()
    {
        if (grid == null)
        {
            Debug.LogError("Grid not assigned!");
            return;
        }
        currentCell = grid.WorldToCell(transform.position);
        targetPosition = grid.GetCellCenterWorld(currentCell);
        transform.position = targetPosition;
    }

    private void Update()
    {
        if (!canMove) return;

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                isMoving = false;
                moveCoolDown = waitTime;
            }
            return;
        }
        if (moveCoolDown > 0f)
        {
            moveCoolDown -= Time.deltaTime;
            return;
        }

        Vector3Int move = Vector3Int.zero;

        if (Input.GetKeyDown(KeyCode.W)) move = new Vector3Int(0, 1, 0);
        if (Input.GetKeyDown(KeyCode.S)) move = new Vector3Int(0, -1, 0);
        if (Input.GetKeyDown(KeyCode.A)) move = new Vector3Int(-1, 0, 0);
        if (Input.GetKeyDown(KeyCode.D)) move = new Vector3Int(1, 0, 0);

        if (move != Vector3Int.zero)
        {
            Vector3Int targetCell = currentCell + move;
            if (IsWalkable(targetCell))
            {
                currentCell = targetCell;
                targetPosition = grid.GetCellCenterWorld(currentCell);
                isMoving = true;
            }
        }
    }

    public void Stun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator StunCoroutine(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }

    private bool IsWalkable(Vector3Int cellPosition)
    {
        return walkableTilemap.HasTile(cellPosition);
    }
}
