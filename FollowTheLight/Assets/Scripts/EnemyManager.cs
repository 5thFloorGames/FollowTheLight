﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType { Exploder, Shooter, Boss };
public enum SecondaryEnemyType { Normal, Little, Big };

public class EnemyManager : MonoBehaviour {

    UserInterfaceManager uim;
	GameManager gm;
	List<GameObject> enemies;

	int enemyActionCounter;
	
	void Start () {
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        uim = GameObject.Find("UserInterface").GetComponent<UserInterfaceManager>();
		OnLevelWasLoaded (GameState.GetLevel());
	}

	void Update () {
		
	}

	void OnLevelWasLoaded(int level) {
        GetEnemiesInScene();
        GiveEnemyCountToUI();
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

	void EnemyBasicAssignments(GameObject obj) {

		EnemyState es = obj.GetComponent<EnemyState> ();
		es.Init (gameObject);

		obj.SendMessage ("InitActions", gameObject);

		enemies.Add (obj);
	}

	public void DeleteEnemyFromList(GameObject enemyobj) {
		enemies.Remove (enemyobj);
        if (enemies.Count == 0) {
            gm.AllEnemiesDestroyed();
        }
        GiveEnemyCountToUI();
    }

	void GetEnemiesInScene() {
        enemies = new List<GameObject>();
		GameObject[] additionalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in additionalEnemies) {
			if (enemy.GetComponent<EnemyState>() != null) {
				EnemyBasicAssignments(enemy);
			}
		}
	}

	void AllEnemyActionsCompleted() {
		gm.EnemyTurnOver ();
	}

    void GiveEnemyCountToUI() {
        uim.UpdateEnemyCount(enemies.Count);
    }

}
