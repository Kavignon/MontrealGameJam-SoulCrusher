﻿using UnityEngine;
using System.Collections;

public class Healthy : Food {
    
	// Use this for initialization
	protected void Awake () {
        base.Awake();
        pointValue = 200;
        speedModifier = 2f;
        speedModifierDuration = 10f;
        healthRestored = 500;
	}
	
	// Update is called once per frame

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            TriggerEnter(col);
            Debug.Log("Healthy Pickup");
        }
    }
}
