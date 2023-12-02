using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MinigameUI : MonoBehaviour
{
    [SerializeField] private Canvas minigameCanvas;
    private float delayTimer = 0f;
    private float delayTimerMax = 3f;
    private bool isWaiting = false;

    private void Start() {
        PlayerSanityMeter playerSanityMeter = FindObjectOfType<PlayerSanityMeter>();
        if (playerSanityMeter != null) {
            playerSanityMeter.OnSanityZero += HandleSanityZero;
        }
    }

    private void HandleSanityZero(object sender, EventArgs e) {
        Debug.Log("Time to play a game!");
        minigameCanvas.gameObject.SetActive(true);
        StartDelay(delayTimerMax);
        if (isWaiting) {
            delayTimer += Time.deltaTime;

            if (delayTimer >= 3f) {
                isWaiting = false;
                delayTimer = 0f;

                minigameCanvas.gameObject.SetActive(false);
            }
        }
        
    }

    private void OnDestroy() {
        // Unsubscribe to prevent memory leaks
        PlayerSanityMeter playerSanityMeter = FindObjectOfType<PlayerSanityMeter>();
        if (playerSanityMeter != null) {
            playerSanityMeter.OnSanityZero -= HandleSanityZero;
        }
    }

    private void StartDelay(float duration) {
        isWaiting = true;
        delayTimer = duration;
    }
}

