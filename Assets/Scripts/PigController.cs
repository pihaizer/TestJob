using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour {
    public float moveSpeed;
    public Joystick joystick;

    void FixedUpdate() {
        Vector2 direction = joystick.Direction;
        direction = SnapDirection(direction);
        GetComponent<Rigidbody2D>().velocity = direction.normalized * Time.fixedDeltaTime * moveSpeed;
    }
    private Vector2 SnapDirection(Vector2 direction) {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            return new Vector2(direction.x, 0);
        } else {
            return new Vector2(Mathf.Tan(GameController.Instance.angleOfSlope) * direction.y, direction.y);
        }
    }
}
