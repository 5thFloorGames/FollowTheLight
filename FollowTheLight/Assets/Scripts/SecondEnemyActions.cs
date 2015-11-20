﻿using UnityEngine;
using System.Collections;

public class SecondEnemyActions : MonoBehaviour {
	
	EnemyManager em;
	
	GameObject aoePrefab;
	NavMeshAgent nva;
	Animator animator;
	
	float actionTime;
	int actionDamage;
	float movementTime;
	
	void Start () {
		aoePrefab = (GameObject)Resources.Load("AreaDamage");
		nva = GetComponent<NavMeshAgent> ();
		animator = gameObject.GetComponentInChildren<Animator>();
		
		actionTime = 1.0f;
		actionDamage = 5;
		
		movementTime = 1.0f;
	}
	
	void Update () {
		
	}
	
	public void InitActions(GameObject manager) {
		em = manager.GetComponent<EnemyManager>();
	}
	
	public void TriggerActions () {
		// Debug.Log (gameObject.name + " enemy used an ability");
		MoveTowardsPlayer ();
		Invoke("StopMovingThenCast", movementTime);
	}
	
	void MoveTowardsPlayer() {
		nva.Resume ();
		nva.destination = GameState.activeCharacter.transform.position;
	}
	
	void CastAreaDamage() {
		animator.SetTrigger ("Attack");
		GameObject spawnedAreaDamage = (GameObject)Instantiate(aoePrefab, transform.position, Quaternion.identity);
		spawnedAreaDamage.name = gameObject.name + "AOE";
		AreaDamageBehavior adb = spawnedAreaDamage.GetComponent<AreaDamageBehavior> ();
		// time, size, damage for the aoe effect and start casting it
		float animationDelay = 1.0f;
		adb.Init (actionTime, 20f, actionDamage, animationDelay);
		Invoke ("ActionsCompletedInformManager", actionTime + animationDelay);
	}
	
	void ActionsCompletedInformManager() {
		em.EnemyActionsCompleted();
	}
	
	void StopMovingThenCast() {
		nva.Stop ();
		CastAreaDamage ();
	}
}