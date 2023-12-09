using UnityEngine;


public class RedShadowEnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerSanityMeter playerSanityMeter;
    [SerializeField] private float enemyIdleSpeed = 1f;
    [SerializeField] private float enemyAttackSpeed = 5f;
    [SerializeField] private float enemyRange = 10f;
    [SerializeField] private float moveDistance = 5f;

    public enum State {
        Idle,
        LockOn,
        Attack,
        Lost
    }

    private State state;
    private Vector3 startingPosition;
    private bool movingLeft = true;
    private float lockOnTimerMax = 5f;
    private float lockOnTimer = 0f;
    private float attackTimerMax = 5f;
    private float attackTimer = 0f;
    private Vector2 attackDirection;

    private void Start() {
        startingPosition = transform.position;
        state = State.Idle;
    }

    private void Update() {
        switch (state) {
            case State.Idle:
                MoveBackAndForth();
                if (Vector2.Distance(transform.position, player.position) < enemyRange) {
                    state = State.LockOn;
                }
                break;
            case State.LockOn:
                // Lock On Animation Player Here
                lockOnTimer += Time.deltaTime;
                if (lockOnTimer > lockOnTimerMax) {
                    state = State.Attack;
                    attackDirection = (player.position - transform.position).normalized;
                }
                break;
            case State.Attack:
                // Move towards lock on direction
                transform.Translate(attackDirection * enemyAttackSpeed * Time.deltaTime, Space.World);
                attackTimer += Time.deltaTime;
                if (attackTimer > attackTimerMax) {
                    state = State.Lost;
                }
                break;
            case State.Lost:
                // Object no longer needed
                DestroyThis();
                break;
        }
    }

    private void MoveBackAndForth() {
        float currentMoveDistance = Vector2.Distance(startingPosition, transform.position);

        if (currentMoveDistance >= moveDistance) {
            FlipDirection();
        }

        float moveStep = enemyIdleSpeed * Time.deltaTime * (movingLeft ? -1 : 1);
        transform.Translate(moveStep, 0, 0, Space.World);
    }

    private void FlipDirection() {
        movingLeft = !movingLeft;
        // Flip the enemy visually by rotating around the Y axis
        transform.Rotate(0f, 180f, 0f);
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player.name)) {
            playerSanityMeter.GoMadAndRespawn();
            DestroyThis();
        }
    }

    private void DestroyThis() => Destroy(gameObject);
}


