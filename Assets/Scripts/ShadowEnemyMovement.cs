using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ShadowEnemyMovement : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float enemySpeed = 1f;
    [SerializeField] private Transform groundDetection;
    [SerializeField] private Rigidbody2D enemyRigidbody;

    private float enemyRange = .7f;
    private bool movingRight = true;

    private void Update()
    {
        enemyRigidbody.velocity = new Vector2(movingRight ? enemySpeed : -enemySpeed, enemyRigidbody.velocity.y);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 1f, groundLayer);
        if (groundInfo.collider == null)
        {
            // If no ground is detected, turn around
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0); // Flip the enemy to face left
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0); // Flip the enemy back to face right
                movingRight = true;
            }
        }

        if (Vector2.Distance(this.transform.position, player.position) < enemyRange ) {
            Debug.Log("Contact");
            DestroyThis();
        }
    }
    
    private void DestroyThis() => Destroy(gameObject);
}

