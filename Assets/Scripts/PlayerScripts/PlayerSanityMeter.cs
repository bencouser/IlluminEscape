using System;
using UnityEngine;

public class PlayerSanityMeter : MonoBehaviour
{

    public event EventHandler OnSanityChanged;

    public class OnSanityChangedEventArgs : EventArgs {
        public float sanityMeterNormalized;
    }
    
    public event EventHandler OnSanityZero;

    [SerializeField] private Transform respawnPoint;
    private float sanityMeter = 10f; // Max sanity
    private float sanityMeterMax = 10f; // Max sanity
    private float sanityRestoreCooldown = 0.25f; // In Seconds
    private float lastSanityRestoreTime;
    private float insanityRate = 1f; // Rate of sanity loss
    private bool isInLight = true;

    private void Start() {
        // Retrieve all instances of SpotlightCheck2D
        SpotlightCheck2D[] spotlights = FindObjectsOfType<SpotlightCheck2D>();
        foreach (SpotlightCheck2D spotlight in spotlights) {
            spotlight.OnIllumination += SpotlightCheck2D_OnIllumination;
        }
    }

    private void Update() {
        // Check if we are in light or not (bool value)
        // bool value determins sign of sanity change
        if (isInLight && sanityMeter < sanityMeterMax) {
            Debug.Log("Restoring");
            sanityMeter += 10 * insanityRate * Time.deltaTime;
        } else  if (!isInLight && sanityMeter > 0) {
            sanityMeter -= insanityRate * Time.deltaTime;
        }

        OnSanityChanged?.Invoke(this, new OnSanityChangedEventArgs{
            // Want to push 1 if sanityMeter is greater than max
            sanityMeterNormalized = sanityMeter / sanityMeterMax
        });

        if (sanityMeter <= 0) {
            Debug.Log("Player has gone MAD!");
            Respawn();
            // Send Event to begin minigame
            OnSanityZero?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Respawn() {
        transform.position = respawnPoint.position;
        sanityMeter = sanityMeterMax;
        lastSanityRestoreTime = Time.time;
    }

    private void SpotlightCheck2D_OnIllumination(object sender, SpotlightCheck2D.OnIlluminationEventArgs e) {
        isInLight = e.IsInLight;

        if (!isInLight && sanityMeter > 0.001) {
            SanityRestoreCooldown();
        }
    }

    private void SanityRestoreCooldown() {
        // Need to find a way to delay this but only trigger out of the light
        isInLight = false;
    }

    public void GoMadAndRespawn() {
        sanityMeter = 0;
        Respawn();
    }

    // Unsubscribe when destroyed.
    private void OnDestroy() {
        SpotlightCheck2D[] spotlights = FindObjectsOfType<SpotlightCheck2D>();
        foreach (SpotlightCheck2D spotlight in spotlights) {
            spotlight.OnIllumination -= SpotlightCheck2D_OnIllumination;
        }
    }
}
