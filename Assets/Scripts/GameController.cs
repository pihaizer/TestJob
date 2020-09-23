using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Vector2[] gridCorners = new Vector2[3];
    public Vector2Int gridSize;
    public Vector2Int firstStonePosition;
    public Vector2Int numberOfStones;
    public Vector2Int tilesBetweenStones;
    public GameObject stonePrefab;
    public GameObject bombPrefab;
    public float angleOfSlope { get; private set; }
    private Vector2 tileSize;
    private float columnOffset;
    private List<GameObject>[,] objectsGrid;
    private Transform bombsObjectTransform;
    void Start() {
        if (Instance != null) {
            Debug.LogError("Multiple Game Controllers");
        }
        Instance = this;

        if (gridCorners.Length != 3) {
            Debug.LogError("There must be 3 grid corners - " + gameObject.name);
        }
        tileSize = new Vector2();
        tileSize.x = (gridCorners[2].x - gridCorners[1].x) / gridSize.x;
        tileSize.y = (gridCorners[1].y - gridCorners[0].y) / gridSize.y;
        columnOffset = (gridCorners[1].x - gridCorners[0].x) / gridSize.y;
        angleOfSlope = Mathf.Atan(columnOffset / tileSize.y);

        objectsGrid = new List<GameObject>[gridSize.x, gridSize.y];
        for (int i = 0; i < gridSize.x; i++) {
            for (int j = 0; j < gridSize.y; j++) {
                objectsGrid[i,j] = new List<GameObject>();
            }
        }

        PlaceStones();

        bombsObjectTransform = new GameObject("Bombs").transform;
    }
    public static GameController Instance { get; private set; }
    private GameObject InstantiateOnGrid(GameObject prefab, Vector2Int tile, Transform parent = null) {
        GameObject newObject = Instantiate(prefab, GetTileCenter(tile), new Quaternion(), parent);
        objectsGrid[tile.x, tile.y].Add(newObject);
        return newObject;
    }
    public void PlaceBomb(Transform transform) {
        Vector2Int tile = GetTileFromWorldPosition(transform.position);
        if (!TileIsOccupied(tile))
            InstantiateOnGrid(bombPrefab, tile, bombsObjectTransform);
    }
    private void PlaceStones() {
        Transform stonesObjectTransform = new GameObject("Stones").transform;
        for (int i = 0; i < numberOfStones.x; i++) {
            for (int j = 0; j < numberOfStones.y; j++) {
                InstantiateOnGrid(stonePrefab, firstStonePosition + new Vector2Int(i, j) * (tilesBetweenStones + Vector2Int.one), 
                    stonesObjectTransform);
            }
        }
    }
    private Vector2Int GetTileFromWorldPosition(Vector2 worldPosition) {
        worldPosition -= gridCorners[0];
        float rawY = worldPosition.y / tileSize.y;
        worldPosition.x -= rawY * columnOffset;
        float rawX = worldPosition.x / tileSize.x;
        return new Vector2Int(Mathf.FloorToInt(rawX), Mathf.FloorToInt(rawY));
    }
    private Vector2 GetTileCenter(Vector2Int tile) {
        return new Vector2(gridCorners[0].x + (tile.x +0.5f) * tileSize.x + tile.y * columnOffset,
            gridCorners[0].y + (tile.y + 0.5f) * tileSize.y);
    }
    private bool TileIsOccupied(Vector2Int tile) {
        try {
            return (objectsGrid[tile.x, tile.y].Count > 0);
        } catch (IndexOutOfRangeException) {
            return true;
        }
    }
}
