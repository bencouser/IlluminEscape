using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerSanityMeterUI : MonoBehaviour
{

    [SerializeField] private Light2D sanityLight;
    [SerializeField] private PlayerSanityMeter playerSanityMeter;

    private void Start() {
        if (playerSanityMeter != null) {
            playerSanityMeter.OnSanityChanged += SanityMeter_OnSanityChanged;
        }
    }

    private void SanityMeter_OnSanityChanged(object sender, EventArgs e) {
        if (e is PlayerSanityMeter.OnSanityChangedEventArgs args) {
            // Assuming you want the intensity to be proportional to the sanity meter.
            sanityLight.intensity = args.sanityMeterNormalized;
        }
    }

    private void OnDestroy() {
        if (playerSanityMeter != null) {
            playerSanityMeter.OnSanityChanged -= SanityMeter_OnSanityChanged;
        }
    }
}

