using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player") && !collision.isTrigger) {
            collision.GetComponent<PigController>().adjacentBombs.Add(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !collision.isTrigger) {
            collision.GetComponent<PigController>().adjacentBombs.Remove(gameObject);
        }
    }
}
