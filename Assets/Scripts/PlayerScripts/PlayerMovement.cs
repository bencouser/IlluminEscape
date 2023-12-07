using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private float playerJumpForce = 10f;
    [SerializeField] private new Rigidbody2D rigidbody2D;
    [SerializeField] private Transform finishPoint;
    private bool isGrounded;
    private string GROUND_TAG = "Ground";

    private void Update() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector2 playerVelocity = new Vector2(horizontalInput, 0f) * playerSpeed * Time.deltaTime;

        transform.Translate(playerVelocity);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, playerJumpForce);
        }

        float distanceToFinish = Vector3.Distance(this.transform.position, finishPoint.transform.position);

        if (distanceToFinish < 1f) {
            print("Level Complete");
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag(GROUND_TAG)) {
            foreach (ContactPoint2D contact in col.contacts) {
                if (contact.normal.y > 0.5f) {  // Adjust the value as needed for your game
                    isGrounded = true;
                    break; // Exit the loop as soon as one valid ground contact is found
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.CompareTag(GROUND_TAG)) {
            isGrounded = false;
        }
    }

}
