﻿using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {

	LineRenderer lr;

	// Use this for initialization
	void Start () {
		lr = GetComponent <LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		lr.SetPosition(0, transform.position);
	}

	public void ShootLaser(Vector3 targetPosition) {
		SetLaserTarget (targetPosition);
		StartCoroutine (ShowLaserFor(0.5f));
	}

	public void HealLaser(Vector3 targetPosition) {
		SetLaserTarget (targetPosition);
		StartCoroutine (ShowLaserFor(0.5f));
	}

	void SetLaserTarget(Vector3 targetPosition) {

		lr.SetPosition(1, targetPosition);
	}

	IEnumerator ShowLaserFor(float seconds) {
		lr.enabled = true;
		yield return new WaitForSeconds (seconds);
		ResetLaser ();
	}

	void ResetLaser() {
		lr.enabled = false;
	}
}