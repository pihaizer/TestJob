using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Vector2Int numberOfStones;
    public Vector2 firstStonePosition;
    public Vector2 distanceBetweenStones;
    public float columnOffset;
    public GameObject stonePrefab;
    void Start() {
        PlaceStones();
    }

    private void PlaceStones() {
        GameObject stonesObject = new GameObject("Stones");
        for (int i = 0; i < numberOfStones.x; i++) {
            for (int j = 0; j < numberOfStones.y; j++) {
                Vector2 stonePosition = firstStonePosition +
                    new Vector2(distanceBetweenStones.x * i + columnOffset * j, distanceBetweenStones.y * j);
                Instantiate(stonePrefab, stonePosition, new Quaternion(), stonesObject.transform);
            }
        }
    }
}
