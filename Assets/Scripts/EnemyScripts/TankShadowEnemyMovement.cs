using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShadowEnemyMovement : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D enemyRigidbody;
    private PlayerSanityMeter playerSanityMeter;
    private float enemySpeed = 0.25f;
    //private Vector3 StartingPosition;

    private void Start() {
        //StartingPosition = enemyRigidbody.transform.position;
        // Retrive all instances of SpotlightCheck2D
        SpotlightCheck2D[] spotlights = FindObjectsOfType<SpotlightCheck2D>();
        foreach (SpotlightCheck2D spotlight in spotlights) {
            spotlight.OnIllumination += SpotlightCheck2D_OnIllumination;
        }
    }

    private void Update() {
        float moveStep = enemySpeed * Time.deltaTime;
        enemyRigidbody.transform.Translate(moveStep, 0, 0, Space.World);
    }

    private void SpotlightCheck2D_OnIllumination(object sender, EventArgs e) {
        // If the player is lit then this effects the enemy movement
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag(player.name)) {
            playerSanityMeter.GoMadAndRespawn();
        }
    }

    // Unsubscribe when destroyed.
    private void OnDestroy() {
        SpotlightCheck2D[] spotlights = FindObjectsOfType<SpotlightCheck2D>();
        foreach (SpotlightCheck2D spotlight in spotlights) {
            spotlight.OnIllumination -= SpotlightCheck2D_OnIllumination;
        }
    }
}
