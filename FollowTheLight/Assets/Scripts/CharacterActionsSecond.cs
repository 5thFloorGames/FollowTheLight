﻿using UnityEngine;
using System.Collections;

public class CharacterActionsSecond : MonoBehaviour {
	
	public int damage;
	public int maxActions;
	
	bool inCharacter;
	bool dead;
	int actions;

	float actionCooldown;
	float previousActionTime;

	GameObject cameraObj;
	GameObject hands;

	GameObject weaponPivot;
	MeleeWeaponDamages mwd;

	UserInterfaceManager uim;
	
	void Awake() {
		dead = false;
		uim = GameObject.Find("UserInterface").GetComponent<UserInterfaceManager>();
		hands = transform.FindChild ("Hands").gameObject;
		weaponPivot = transform.FindChild ("WeaponPivot").gameObject;
		mwd = weaponPivot.GetComponentInChildren<MeleeWeaponDamages> ();
		weaponPivot.SetActive (false);
		actionCooldown = 0.7f;
		previousActionTime = Time.time;
	}
	
	void Start () {
		updateActionsToUI();
	}
	
	void Update () {
		if (GameState.playersTurn && inCharacter && actions > 0 && !dead) {
			if (Time.time - previousActionTime >= actionCooldown) {
				if (Input.GetButtonDown ("Fire1")){
					Strike();
					updateActionsToUI();
				}
				if (Input.GetButtonDown ("Fire2")){
					Slash();
					updateActionsToUI();
				}
			}
		}
	}
	
	void Strike() {
		actions -= 1;
		previousActionTime = Time.time;
		weaponPivot.SetActive (true);
		mwd.damageAmount = 15;

		weaponPivot.transform.localRotation = Quaternion.Euler (-90f, 0f, 0f);
		iTween.RotateTo (weaponPivot, iTween.Hash (
			"x", 30f,
			"time", 0.6f,
			"islocal", true,
			"oncomplete", "PutWeaponAway",
			"oncompletetarget", gameObject));
	}

	void Slash() {
		actions -= 1;
		previousActionTime = Time.time;
		weaponPivot.SetActive (true);
		mwd.damageAmount = 5;

		weaponPivot.transform.localRotation = Quaternion.Euler (0f, -89f, 90f);
		iTween.RotateTo (weaponPivot, iTween.Hash (
			"y", 89f,
			"time", 0.5f,
			"islocal", true,
			"oncomplete", "PutWeaponAway",
			"oncompletetarget", gameObject));
	}

	void PutWeaponAway() {
		weaponPivot.SetActive (false);
	}
	
	void updateActionsToUI() {
		uim.UpdateActionPoints(gameObject.transform.parent.name, actions, maxActions);
	}


	
	// Character Manager calls these with a broadcast message
	
	void CharacterDied() {
		dead = true;
	}
	
	void CharacterResurrected() {
		dead = false;
		ResetActions();
	}
	
	void ResetActions() {
		actions = maxActions;
		updateActionsToUI();
	}
	
	void EnterCharacter() {
		inCharacter = true;
		hands.SetActive (true);
	}
	
	void LeaveCharacter() {
		inCharacter = false;
		hands.SetActive (false);
	}
}
