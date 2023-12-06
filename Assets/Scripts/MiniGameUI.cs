using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;

public class MinigameUI : MonoBehaviour
{
    private float delayTimer = 0f;
    private float delayTimerMax = 3f;
    private bool isWaiting = false;

    private void Start() {
        PlayerSanityMeter playerSanityMeter = FindObjectOfType<PlayerSanityMeter>();
        if (playerSanityMeter != null) {
            playerSanityMeter.OnSanityZero += HandleSanityZero;
        }
    }

    private void Update() {
        if (isWaiting) {
            delayTimer += Time.deltaTime;
            if (delayTimer >= 3f) {
                Debug.Log("Minigame over");
                isWaiting = false;
                delayTimer = 0f;
                ToggleChildren(false);
            }
        }
    }

    public void HandleSanityZero(object sender, EventArgs e) {
        Debug.Log("Time to play a game!");
        ToggleChildren(true);
        StartDelay(delayTimerMax);
    }

    private void ToggleChildren(bool activate = true) {
        Transform canvasTransform = GetComponent<Transform> ();
        Debug.Log(activate);

        foreach (Transform child in canvasTransform) {
            child.gameObject.SetActive(activate);
        }
    }

    private void StartDelay(float duration) {
        isWaiting = true;
        delayTimer = 0f;
    }

    private void OnDestroy() {
        PlayerSanityMeter playerSanityMeter = FindObjectOfType<PlayerSanityMeter>();
        if (playerSanityMeter != null) {
            playerSanityMeter.OnSanityZero -= HandleSanityZero;
        }
    }
}
