using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    [SerializeField] GameObject pointA;
    [SerializeField] GameObject pointB;
    [SerializeField] float enemySpeed;
    [SerializeField] new Rigidbody2D rigidbody2D;
    private Transform currentPoint;

    private void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

/*     private void Update() {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform) {
            rigidbody2D.velocity = new Vector2(enemySpeed, 0);
        } else {
            rigidbody2D.velocity = new Vector2(-enemySpeed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform) {
            Debug.Log("Hit Point B");
            currentPoint = pointA.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform) {
            Debug.Log("Hit Point A");
            currentPoint = pointB.transform;
        }
    } */
/* 
    private void FixedUpdate() {
        if (movingToPointB) {
            MoveToPoint(pointB.transform.position);
        } else {
            MoveToPoint(pointA.transform.position);
        }
    }

    private void MoveToPoint(Vector2 target) {
        Vector2 position = rigidbody2D.position;
        Vector2 direction = (target - position).normalized;
        rigidbody2D.velocity = direction * enemySpeed;

        if (Vector2.Distance(position, target) < 0.5f) {
            movingToPointB = !movingToPointB; // Toggle the direction
            rigidbody2D.velocity = Vector2.zero; // Stop the enemy
        }
    }
 */

}
