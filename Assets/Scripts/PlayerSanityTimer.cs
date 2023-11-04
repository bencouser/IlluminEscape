using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSanityTimer : MonoBehaviour
{

    [SerializeField] private float sanityMeter = 30f; // Max sanity
    private float insanityRate = 1f; // Rate of sanity loss

    private void Start() {
        SpotlightCheck2D.Instance.OnIllumination += SpotlightCheck2D_OnIllumination;
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
        sanityMeter = 30f;
        Debug.Log("Sanity Restored");
    }

}
