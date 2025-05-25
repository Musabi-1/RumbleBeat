using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector3 position;
    public Node parent;
    public float gCost;
    public float hCost;
    public float fCost => gCost + hCost;
    public Node(Vector3 pos)
    {
        position = pos;
    }
}

public class enemyBehaviour : MonoBehaviour
{
    private GameObject targetObj;
    private Transform target;
    [SerializeField] private float moveSpeed = 2f;
    private TempoManager tempoManager;
    private Queue<Vector3> pathQueue = new Queue<Vector3>();
    private bool isMoving = false;
    private bool previousBeat = false;

    private void Start()
    {
        targetObj = GameObject.FindGameObjectWithTag("MainCharacter");
        tempoManager = GameObject.FindGameObjectWithTag("Tempo").GetComponent<TempoManager>();
        target = targetObj.transform;
        StartPathfinding();
    }

    private void Update()
    {
        target = targetObj.transform;
        if (tempoManager.enemybeat && !isMoving && !previousBeat)
        {
            if (pathQueue.Count == 0)
            {
                StartPathfinding();
            }
            if (pathQueue.Count > 0)
            {
                StartCoroutine(MoveOneStep(pathQueue.Dequeue()));
            }
        }

        previousBeat = tempoManager.enemybeat;
    }

    public void StartPathfinding()
    {
        List<Vector3> path = FindPath(transform.position, target.position);
        pathQueue = new Queue<Vector3>(path);
    }

    private IEnumerator MoveOneStep(Vector3 targetPos)
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }

    private List<Vector3> FindPath(Vector3 start, Vector3 end)
    {
        Vector3 startPos = RoundVector(start);
        Vector3 endPos = RoundVector(end);

        List<Node> openList = new List<Node>();
        HashSet<Vector3> closedSet = new HashSet<Vector3>();

        Node startNode = new Node(startPos);
        openList.Add(startNode);

        while (openList.Count > 0)
        {
            Node current = openList[0];
            foreach (Node node in openList)
            {
                if (node.fCost < current.fCost || (node.fCost == current.fCost && node.hCost < current.hCost))
                    current = node;
            }

            openList.Remove(current);
            closedSet.Add(current.position);

            if (current.position == endPos)
                return RetracePath(current);

            foreach (Vector3 neighborPos in GetNeighbors(current.position))
            {
                if (closedSet.Contains(neighborPos)) continue;

                float newG = current.gCost + 1;
                Node neighbor = openList.Find(n => n.position == neighborPos);
                if (neighbor == null)
                {
                    neighbor = new Node(neighborPos);
                    neighbor.gCost = newG;
                    neighbor.hCost = Heuristic(neighborPos, endPos);
                    neighbor.parent = current;
                    openList.Add(neighbor);
                }
                else if (newG < neighbor.gCost)
                {
                    neighbor.gCost = newG;
                    neighbor.parent = current;
                }
            }
        }

        return new List<Vector3>(); // No path found
    }

    private List<Vector3> RetracePath(Node endNode)
    {
        List<Vector3> path = new List<Vector3>();
        Node current = endNode;
        while (current != null)
        {
            path.Add(current.position);
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    private float Heuristic(Vector3 a, Vector3 b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.z - b.z);
    }

    private List<Vector3> GetNeighbors(Vector3 position)
    {
        List<Vector3> neighbors = new List<Vector3>
        {
            position + Vector3.right,
            position + Vector3.left,
            position + Vector3.forward,
            position + Vector3.back
        };

        // Optional: Filter out unwalkable nodes
        // neighbors.RemoveAll(pos => !IsWalkable(pos));
        return neighbors;
    }

    private Vector3 RoundVector(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), 0, Mathf.Round(v.z));
    }
}
