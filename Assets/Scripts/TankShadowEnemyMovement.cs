using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShadowEnemyMovement : MonoBehaviour
{

    private void Start() {
        // Retrive all instances of SpotlightCheck2D
        SpotlightCheck2D[] spotlights = FindObjectsOfType<SpotlightCheck2D>();
        foreach (SpotlightCheck2D spotlight in spotlights) {
            spotlight.OnIllumination += SpotlightCheck2D_OnIllumination;
        }
    }

    private void SpotlightCheck2D_OnIllumination(object sender, EventArgs e) {
        // If the player is lit then this effects the enemy movement
    }

    private void Update() {
        // Something
    }

}
