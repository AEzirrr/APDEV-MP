using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCoordinates : MonoBehaviour
{
    public static UnitCoordinates Instance { get; private set; }

    public Vector2Int unitCoord;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            Tile tile = other.GetComponent<Tile>();
            if (tile != null)
            {
                unitCoord = tile.cords;
                Debug.Log($"Unit entered Tile with coordinates: {unitCoord}");
            }
            else
            {
                Debug.LogError("Tile component not found on the collided object.");
            }
        }
    }

    public Vector2Int GetUnitCoord()
    {
        return unitCoord;
    }
}
