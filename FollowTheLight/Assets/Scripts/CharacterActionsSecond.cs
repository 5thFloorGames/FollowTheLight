﻿using UnityEngine;
using System.Collections;

public class CharacterActionsSecond : MonoBehaviour {

	public int slashDamage;
    public int maxActions;

    bool enemyInAim;
	
	bool inCharacter;
	bool dead;
	int actions;

	float actionCooldown;
	float previousActionTime;

	CharacterSoundController cas;
    Transform cameraTf;
    GameObject overlay;

	ParticleSystem ps;
	Animator handAnimator;
	GameObject weaponPivot;
	MeleeWeaponDamages mwd;
	MeleeRangeInformer slashRange;

	UserInterfaceManager uim;
	
	void Awake() {
		handAnimator = transform.FindChild("Camera").FindChild("Overlay").FindChild("Hands").GetComponent<Animator>();
		ps = transform.FindChild("Particles").GetComponent<ParticleSystem>();
		dead = false;
		uim = GameObject.Find("UserInterface").GetComponent<UserInterfaceManager>();
		cas = GetComponentInChildren<CharacterSoundController>();
        cameraTf = transform.FindChild("Camera");
		overlay = cameraTf.FindChild ("Overlay").gameObject;
		weaponPivot = cameraTf.FindChild ("WeaponPivot").gameObject;
		mwd = weaponPivot.GetComponentInChildren<MeleeWeaponDamages> ();
		slashRange = GetComponentInChildren<MeleeRangeInformer> ();
		weaponPivot.SetActive (false);
		actionCooldown = 0.7f;
		previousActionTime = Time.time;
        enemyInAim = false;
	}
	
	void Start () {
		ps.Stop();
		updateActionsToUI();
	}
	
	void Update () {
		if (GameState.playersTurn && inCharacter && actions > 0 && !dead && enemyInAim) {
			if (Time.time - previousActionTime >= actionCooldown) {
				if (Input.GetButtonDown ("Fire1")){
					Slash();
					updateActionsToUI();
				}
			}
		}
	}

    public void EnemiesEnteredAimRange() {
        enemyInAim = true;
    }

    public void NoEnemiesInAimRange() {
        enemyInAim = false;
    }

	void Slash() {
		ps.Play ();
		actions -= 1;
		handAnimator.SetBool ("Casting", true);
		previousActionTime = Time.time;
		weaponPivot.SetActive (true);
		mwd.damageAmount = slashDamage;
		
		weaponPivot.transform.localRotation = Quaternion.Euler (0f, -89.99f, 90f);
		iTween.RotateTo (weaponPivot, iTween.Hash (
			"y", 89.99f,
			"time", 0.5f,
			"islocal", true,
			"oncomplete", "PutWeaponAway",
			"oncompletetarget", gameObject));
	}

	void PutWeaponAway() {
		ps.Stop ();
		handAnimator.SetBool ("Casting", false);
		weaponPivot.SetActive (false);
	}
	
	void updateActionsToUI() {
		uim.UpdateActionPoints(gameObject.name, actions, maxActions);
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
		slashRange.ActivateTheHitList ();
		inCharacter = true;
		overlay.SetActive (true);
	}
	
	void LeaveCharacter() {
		slashRange.DeactivateTheHitList ();
		inCharacter = false;
		overlay.SetActive (false);
        
	}
}
