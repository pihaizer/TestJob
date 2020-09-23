using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public float moveSpeed;
    public Joystick joystick;
    public GameObject bombPrefab;
    public Transform bombsObject;
    public List<GameObject> adjacentBombs = new List<GameObject>();

    private const float angleOfSlope = 0.11929325f; // in radians
    private List<GameObject> adjacentStones = new List<GameObject>();

    private void Start() {
        if (bombsObject == null) {
            Debug.LogWarning("No bombs object set - " + transform.name);
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = joystick.Direction;
        direction = SnapDirection(direction);
        GetComponent<Rigidbody2D>().velocity = direction.normalized * Time.fixedDeltaTime * moveSpeed;
    }
    private Vector2 SnapDirection(Vector2 direction) {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            return new Vector2(direction.x, 0);
        } else {
            return new Vector2(Mathf.Tan(angleOfSlope) * direction.y, direction.y);
        }
    }
    public void DropBomb() {
        if(adjacentBombs.Count == 0) {
            Vector2 bombPosition = CalculateBombPosition();
            Instantiate(bombPrefab, bombPosition, new Quaternion(), bombsObject);
        }
    }
    private Vector2 CalculateBombPosition() {
        Vector3 position = new Vector2();
        if(adjacentStones.Count > 2) {
            foreach(var stone in adjacentStones) {
                position += stone.transform.position;
            }
            position /= adjacentStones.Count;
        } else {
            Vector2Int offsetFromStone = GetOffsetFromStoneInTiles(adjacentStones[0]);
            position = (Vector2)adjacentStones[0].transform.position + TilesToWorld(offsetFromStone);
        }
        return position;
    }
    private Vector2Int GetOffsetFromStoneInTiles(GameObject stone) {
        Vector2Int offset = new Vector2Int();
        float rawOffsetY = (transform.position.y - stone.transform.position.y);
        offset.y = Mathf.RoundToInt(rawOffsetY);
        offset.x = Mathf.RoundToInt(
            (transform.position.x - stone.transform.position.x - Mathf.Tan(angleOfSlope) * rawOffsetY) 
            / GameController.Instance.tileSize.x);
        return offset;
    }
    private Vector2 TilesToWorld(Vector2Int tiles) {
        Vector2 world = tiles * GameController.Instance.tileSize;
        world.x += Mathf.Tan(angleOfSlope) * tiles.y;
        return world;
    }
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Stone")) {
            adjacentStones.Add(collision.gameObject);
        }
    }
    public void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Stone")) {
            adjacentStones.Remove(collision.gameObject);
        }
    }
}
