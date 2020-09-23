using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public Vector2Int numberOfStones;
    public Vector2 firstStonePosition;
    public Vector2Int tilesBetweenStones;
    public Vector2 tileSize;
    public float columnOffset;
    public GameObject stonePrefab;
    void Start() {
        if (Instance != null) {
            Debug.LogError("Multiple Game Controllers");
        }
        Instance = this;

        PlaceStones();
    }
    public static GameController Instance { get; private set; }

    private void PlaceStones() {
        GameObject stonesObject = new GameObject("Stones");
        for (int i = -1; i <= numberOfStones.x; i++) {
            for (int j = -1; j <= numberOfStones.y; j++) {
                Vector2 stonePosition = firstStonePosition +
                    new Vector2(tileSize.x * Mathf.Sign(tilesBetweenStones.x) * (Mathf.Abs(tilesBetweenStones.x) + 1) * i
                    + Mathf.Sign(tilesBetweenStones.y) * (Mathf.Abs(tilesBetweenStones.y) + 1) * columnOffset * j, 
                    tileSize.y * Mathf.Sign(tilesBetweenStones.y) * (Mathf.Abs(tilesBetweenStones.y) + 1) * j);
                GameObject stone = Instantiate(stonePrefab, stonePosition, new Quaternion(), stonesObject.transform);
                if(i == -1 || i == numberOfStones.x || j == -1 || j == numberOfStones.y) {
                    stone.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }
}
