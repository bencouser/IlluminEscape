using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSanityTimer : MonoBehaviour
{

    [SerializeField] private float sanityMeter = 10f; // Max sanity
    [SerializeField] private float sanityRestoreCooldown = 1f; // In Seconds
    private float lastSanityRestoreTime;
    private float insanityRate = 1f; // Rate of sanity loss

    private void Start() {
    // Retrieve all instances of SpotlightCheck2D
    SpotlightCheck2D[] spotlights = FindObjectsOfType<SpotlightCheck2D>();
    foreach (SpotlightCheck2D spotlight in spotlights) {
        spotlight.OnIllumination += SpotlightCheck2D_OnIllumination;
    }
    Debug.Log("PST Start");
    }

    private void Update() {
        sanityMeter -= insanityRate * Time.deltaTime;
        if (sanityMeter <= 0) {
            Debug.Log("Player has gone MAD!");
            this.enabled = false;
        }
        
    }

    private void SpotlightCheck2D_OnIllumination(object sender, EventArgs e) {
        if (Time.time - lastSanityRestoreTime >= sanityRestoreCooldown && sanityMeter > 0.01) {
            sanityMeter = 30f;
            lastSanityRestoreTime = Time.time;
            Debug.Log("Sanity Restored");
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
