﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {

	GameManager gm;
	List<GameObject> enemies;

	int enemyActionCounter;
	
	void Start () {
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
		OnLevelWasLoaded (GameState.GetLevel());
	}

	void Update () {
		
	}

	void OnLevelWasLoaded(int level) {
        GetEnemiesInScene();
	}

	public void PlayersTurnActivated() {

	}

	public void TriggerEnemyActions() {
		enemyActionCounter = 0;
		foreach (GameObject e in enemies) {
			e.BroadcastMessage("TriggerActions");
		}
		if (enemies.Count == 0) {
			Invoke("AllEnemyActionsCompleted", 0.2f);
		}
	}

	public void EnemyActionsCompleted() {
		enemyActionCounter += 1;
		if (enemyActionCounter == enemies.Count) {
			AllEnemyActionsCompleted();
		}
	}

	void EnemyBasicAssignments(GameObject obj, int health) {

		EnemyState es = obj.GetComponent<EnemyState> ();
		es.Init (health, gameObject);

		obj.SendMessage ("InitActions", gameObject);

		enemies.Add (obj);
	}

	void DeleteEnemyFromList(GameObject enemyobj) {
		enemies.Remove (enemyobj);
	}

	void GetEnemiesInScene() {
        enemies = new List<GameObject>();
		GameObject[] additionalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in additionalEnemies) {
			if (enemy.GetComponent<EnemyState>() != null) {
				EnemyBasicAssignments(enemy, 30);
			}
		}
	}

	void AllEnemyActionsCompleted() {
		gm.EnemyTurnOver ();
	}

}
