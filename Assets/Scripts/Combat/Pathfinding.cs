using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] Vector2Int startCords;
    public Vector2Int StartCords { get { return startCords; } }

    [SerializeField] Vector2Int targetCords;
    public Vector2Int TargetCords { get { return targetCords; } }

    Node startNode;
    Node targetNode;
    Node currentNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    Vector2Int[] searchOrder = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
        }
    }

    public List<Node> GetNewPath(Vector2Int start, Vector2Int target)
    {
        startCords = start;
        targetCords = target;
        gridManager.ResetNodes();
        BreadthFirstSearch(startCords);
        return BuildPath();
    }

    public void SetNewDestination(Vector2Int startCoordinates, Vector2Int targetCoordinates)
    {
        startCords = startCoordinates;
        targetCords = targetCoordinates;
        GetNewPath(startCoordinates, targetCoordinates);
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        if (!grid.ContainsKey(coordinates) || !grid.ContainsKey(targetCords))
        {
            Debug.LogError($"Invalid coordinates. Start: {coordinates}, Target: {targetCords}");
            return;
        }

        startNode = grid[coordinates];
        targetNode = grid[targetCords];

        frontier.Clear();
        reached.Clear();

        frontier.Enqueue(startNode);
        reached.Add(coordinates, startNode);

        while (frontier.Count > 0)
        {
            currentNode = frontier.Dequeue();
            currentNode.explored = true;
            ExploreNeighbors();
            if (currentNode.cords == targetCords)
            {
                currentNode.walkable = false;
                break;
            }
        }
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in searchOrder)
        {
            Vector2Int neighborCords = currentNode.cords + direction;

            if (grid.ContainsKey(neighborCords))
            {
                neighbors.Add(grid[neighborCords]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.cords) && neighbor.walkable)
            {
                neighbor.connectTo = currentNode;
                reached.Add(neighbor.cords, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNode;

        if (currentNode == null)
        {
            Debug.LogError("Target node is null.");
            return path;
        }

        while (currentNode != null)
        {
            path.Add(currentNode);
            Debug.Log($"Adding node to path: {currentNode.cords}");
            currentNode = currentNode.connectTo;
        }

        path.Reverse();
        return path;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
