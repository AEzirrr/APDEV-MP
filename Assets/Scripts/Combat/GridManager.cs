using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int unityGridSize; 
    public int UnityGridSize { get { return unityGridSize; } }

    public Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    [SerializeField] List<GameObject> tiles = new List<GameObject>();
    [SerializeField] GameObject playerPrefab;  
    [SerializeField] GameObject enemyPrefab;   

    public GameObject playerInstance; 
    public GameObject enemyInstance;  

    private void Awake()
    {
        CreateGrid();
        InitializeTiles();
        PrintGridContents();
        SpawnPlayerAndEnemy();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        return grid.ContainsKey(coordinates) ? grid[coordinates] : null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].walkable = false;
        }
    }

    public void UnblockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].walkable = true;
        }
    }

    public void ResetNodes()
    {
        foreach (var entry in grid)
        {
            entry.Value.connectTo = null;
            entry.Value.explored = false;
            entry.Value.path = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        return new Vector2Int(Mathf.RoundToInt(position.x / unityGridSize), Mathf.RoundToInt(position.z / unityGridSize));
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        foreach (GameObject tile in tiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            if (tileComponent != null && tileComponent.cords == coordinates)
            {
                return tile.transform.position + Vector3.up * 0.5f; 
            }
        }

        Debug.LogError($"No tile found with coordinates ({coordinates})");
        return Vector3.zero;
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int cords = new Vector2Int(x, y);
                grid.Add(cords, new Node(cords, true));
            }
        }
    }

    private void InitializeTiles()
    {
        foreach (GameObject tile in tiles)
        {
            Tile tileComponent = tile.GetComponent<Tile>();
            if (tileComponent != null && tileComponent.blocked)
            {
                BlockNode(tileComponent.cords);
            }
        }
    }

    private void SpawnPlayerAndEnemy()
    {
        Vector2Int playerCoords = new Vector2Int(1, gridSize.y / 2);
        Vector2Int enemyCoords = new Vector2Int(gridSize.x - 2, gridSize.y / 2);

        Vector3 playerPosition = GetPositionFromCoordinates(playerCoords);
        Vector3 enemyPosition = GetPositionFromCoordinates(enemyCoords);

      
        playerInstance = Instantiate(playerPrefab, playerPosition, Quaternion.identity);
        enemyInstance = Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);

       
        playerInstance.GetComponent<PlayerStats>().characterName = "Player";
        enemyInstance.GetComponent<EnemyStats>().characterName = "Enemy";
    }

    public void PrintGridContents()
    {
        foreach (var entry in grid)
        {
            Debug.Log($"Coordinates: {entry.Key} - Walkable: {entry.Value.walkable}");
        }
    }
}
