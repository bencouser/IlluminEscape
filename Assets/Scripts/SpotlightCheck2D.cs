using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpotlightCheck2D : MonoBehaviour {
    
    public event EventHandler OnIllumination;
    [SerializeField] private LayerMask obstacleMask; // Using Default Layer Mask
    [SerializeField] private GameObject player;
    [SerializeField] private float spotAngle = 110;  // Set the spotlight angle in degrees
    private float checkRate = 1f;  // Rate at which to check for illumination (checks per second)
    private float nextCheck;

    private void Update() {
        if (Time.time > nextCheck) {
            nextCheck = Time.time + 1f / checkRate;
            CheckIllumination();
        }
    }

    public void CheckIllumination() {
        Vector3 playerDisplacement = player.transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(-transform.up, playerDisplacement.normalized);  // Assuming light direction is -transform.up

        // Check if player is within light cone
        if (angleToPlayer < spotAngle / 2) {
            float distanceToPlayer = playerDisplacement.magnitude;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDisplacement, distanceToPlayer, obstacleMask);
            if (hit.collider != null) {
                if (hit.collider.gameObject == player) {
                    OnIllumination?.Invoke(this, EventArgs.Empty);
                }
                else {
                    //Debug.Log("Player is not illuminated (obstacle in the way)");
                }
            }
            else {
                //Debug.Log("Player is not illuminated");
            }
        } else {
            //Debug.Log("Player Not in Spotlight Angle");
        }

        // Draw rays indicating the spotlight angle
        float halfAngle = spotAngle / 2;
        Vector3 rightBoundaryDirection = Quaternion.Euler(0, 0, halfAngle) * -transform.up;
        Vector3 leftBoundaryDirection = Quaternion.Euler(0, 0, -halfAngle) * -transform.up;

        Debug.DrawRay(transform.position, rightBoundaryDirection * 10, Color.yellow);  // Adjust length as needed
        Debug.DrawRay(transform.position, leftBoundaryDirection * 10, Color.yellow);  // Adjust length as needed
    }
}
