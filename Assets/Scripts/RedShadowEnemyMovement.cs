using System;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class RedShadowEnemyMovement : MonoBehaviour
{


    [SerializeField] private Transform player;
    [SerializeField] private float enemySpeed = 5f;
    [SerializeField] private float enemyRange = 10f;

    public enum State {
        Idle,
        LockOn,
        Attack,
        Lost
    }

    private float lockOnTimerMax = 5f;
    private float lockOnTimer = 0f;
    private float attackTimerMax = 5f;
    private float attackTimer = 0f;
    private Vector2 attackDirection;
    

    private State state;

    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        Debug.Log(state);
    
        switch (state) {
            case State.Idle:
                if (Vector2.Distance(this.transform.position, player.transform.position) < enemyRange) {
                    state = State.LockOn;
                }
                break;
            case State.LockOn:
                // Lock On Animation Player Here
                lockOnTimer += Time.deltaTime;
                if (lockOnTimer > lockOnTimerMax) {
                    Debug.Log("Angle: " + attackDirection);
                    state = State.Attack;
                    // Get Direction to player
                    attackDirection = (player.position - transform.position).normalized;
                }
                break;
            case State.Attack:
                // Move towards lock on direction
                transform.Translate(attackDirection * enemySpeed * Time.deltaTime, Space.World);
                attackTimer += Time.deltaTime;
                if (attackTimer > attackTimerMax) {
                    state = State.Lost;
                }
                break;
            case State.Lost:
                // Object no longer needed
                Destroy(this.gameObject);
                break;
        }

    }

}

