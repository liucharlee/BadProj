﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Blobs are objects whose behaviour is controlled by a state machine. They are destroyed when the player clicks on them.
 * This class manages the blob state machine and provides a link to the master game controller,
   to allow the blob to interact with the game world.*/
public class Blob : MonoBehaviour
{
    private BlobState currentState; // Current blob state (unique to each blob)
    private GameController controller;  // Cached connection to game controller component

    private float isShrinking; //check if isShrinking

    void Start()
    {
        ChangeState(new BlobStateMoving(this)); // Set initial state.
        controller = GetComponentInParent<GameController>();

        isShrinking = 0;

    }

    void Update()
    {
        currentState.Run(); // Run state update.
    }

    // Change state.
    public void ChangeState(BlobState newState)
    {
        if (currentState != null) currentState.Leave();
        currentState = newState;
        currentState.Enter();
    }

    // Change blobs to shrinking state when clicked.
    void OnMouseDown()
    {
        if(isShrinking == 0){
            ChangeState(new BlobStateShrinking(this)); 
            isShrinking = 1;
        }
    }

    // Destroy blob gameObject and remove it from master blob list.
    public void Kill()
    {
        controller.RemoveFromList(this);
        Destroy(gameObject);
        controller.AddScore(10);
    }

    public void addPoint()
    {
        controller.AddScore(1);
    }
}
