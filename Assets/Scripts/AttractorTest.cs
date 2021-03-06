﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorTest : MonoBehaviour
{
	GameObject player;
	//PlayerMovement playerMovement;
	Rigidbody2D playerRb;
	Rigidbody2D rb;
	public float G = 2;

	public bool isAttractorACtive = false;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerRb = player.GetComponent<Rigidbody2D>();
		//playerMovement = player.GetComponent<PlayerMovement>();
		rb = this.gameObject.GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        //if(isAttractorACtive == true)
			//Attract();
	}

    void Attract()
	{
		if (playerRb == null || rb == null)
			return;

		Vector2 direction = (Vector2)this.gameObject.transform.position - playerRb.position;
		float distance = direction.magnitude;

		if (distance == 0f)
			return;

		float forceMagnitude = G * (rb.mass * playerRb.mass) / Mathf.Pow(distance, 2);
		Vector3 force = direction.normalized * forceMagnitude;
		Debug.Log(this.gameObject.name + "   Force is :   " + force );
		playerRb.AddForce(force);
	}
}
