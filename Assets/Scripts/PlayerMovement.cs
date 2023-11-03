using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private float playerJumpForce = 10f;
    [SerializeField] private new Rigidbody2D rigidbody2D;
    private bool isGrounded;
    private string GROUND_TAG = "Ground";

    private void Update() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector2 playerVelocity = new Vector2(horizontalInput, 0f) * playerSpeed * Time.deltaTime;

        transform.Translate(playerVelocity);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, playerJumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag(GROUND_TAG)) {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        isGrounded = false;
    }

}
