using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSanityMeter : MonoBehaviour
{

    public event EventHandler OnSanityChanged;

    public class OnSanityChangedEventArgs : EventArgs {
        public float sanityMeterNormalized;
    }

    [SerializeField] private float sanityMeter = 10f; // Max sanity
    [SerializeField] private Transform respawnPoint;
    private float sanityMeterMax = 10f; // Max sanity
    private float sanityRestoreCooldown = 1f; // In Seconds
    private float lastSanityRestoreTime;
    private float insanityRate = 1f; // Rate of sanity loss

    private void Start() {
        // Retrieve all instances of SpotlightCheck2D
        SpotlightCheck2D[] spotlights = FindObjectsOfType<SpotlightCheck2D>();
        foreach (SpotlightCheck2D spotlight in spotlights) {
            spotlight.OnIllumination += SpotlightCheck2D_OnIllumination;
        }
    }

    private void Update() {
        sanityMeter -= insanityRate * Time.deltaTime;

        OnSanityChanged?.Invoke(this, new OnSanityChangedEventArgs{
            sanityMeterNormalized = sanityMeter / sanityMeterMax
        });

        if (sanityMeter <= 0) {
            Debug.Log("Player has gone MAD!");
            Respawn();
        }
        
    }

    private void Respawn() {
        transform.position = respawnPoint.position;
        sanityMeter = sanityMeterMax;
        lastSanityRestoreTime = Time.time;
    }

    private void SpotlightCheck2D_OnIllumination(object sender, EventArgs e) {
        if (Time.time - lastSanityRestoreTime >= sanityRestoreCooldown && sanityMeter > 0.01) {
            sanityMeter = sanityMeterMax;
            lastSanityRestoreTime = Time.time;
            Debug.Log("Sanity Restored");
        }
/* 
        if (sanityMeter > 0.001) {
            Debug.Log("Restoring");
            sanityMeter += 2 * insanityRate * Time.deltaTime;
        } */
    }

    // Unsubscribe when destroyed.
    private void OnDestroy() {
        SpotlightCheck2D[] spotlights = FindObjectsOfType<SpotlightCheck2D>();
        foreach (SpotlightCheck2D spotlight in spotlights) {
            spotlight.OnIllumination -= SpotlightCheck2D_OnIllumination;
        }
    }

}
