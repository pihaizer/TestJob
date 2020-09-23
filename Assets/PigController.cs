using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public float moveSpeed;
    public Joystick joystick;
    public GameObject bombPrefab;
    public Transform bombsObject;

    private const float angleOfSlope = 0.11929325f; // in radians

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
        Instantiate(bombPrefab, transform.position, new Quaternion(), bombsObject);
    }
}
