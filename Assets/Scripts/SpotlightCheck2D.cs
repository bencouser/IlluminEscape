using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightCheck2D : MonoBehaviour {
    
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private GameObject player;  // Assign your player GameObject in the Inspector
    private float checkRate = 0.5f;  // Rate at which to check for illumination (checks per second)
    private float nextCheck;

    // Assuming the spotlight angle and direction are handled by your custom 2D spotlight solution
    [SerializeField] private float spotAngle = 180;  // Set the spotlight angle in degrees

    private void Update() {
        if (Time.time > nextCheck) {
            nextCheck = Time.time + 1f / checkRate;
            CheckIllumination();
        }
    }

    private void CheckIllumination() {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(-transform.up, directionToPlayer.normalized);  // Assuming light direction is -transform.up

        // Check if player is within light cone
        if (angleToPlayer < spotAngle / 2) {
            float distanceToPlayer = directionToPlayer.magnitude;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask);
            if (hit.collider != null) {
                if (hit.collider.gameObject == player) {
                    Debug.Log("Player is illuminated");
                }
                else {
                    Debug.Log("Player is not illuminated (obstacle in the way)");
                    Debug.Log("Hit: " + hit.collider.gameObject.name);
                }
            }
            else {
                Debug.Log("Player is not illuminated");
                Debug.DrawRay(transform.position, directionToPlayer, Color.red, 1f);
            }
        } else {
            Debug.Log("angleToPlayer < spotAngle / 2");
        }
    }
}
