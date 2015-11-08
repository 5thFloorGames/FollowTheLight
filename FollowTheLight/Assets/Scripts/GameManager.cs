﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private CharacterManager cm;
	private EnemyManager em;
	private bool playersTurn;

	
	void Start () {
		cm = gameObject.GetComponent<CharacterManager> ();
		PlayerTurnStart ();
	}

	void Update () {
		if (Input.GetButton ("Cancel")) {
			QuitGame();
		}
		if (playersTurn) {
			if (Input.GetButton ("Submit")) {
				EnemyTurnStart();
			}
		}
	}

	public void QuitGame() {
		Application.LoadLevel (0);
	}

	void EnemyTurnStart() {
		playersTurn = false;
		cm.deactivatePlayer ();
		StartCoroutine(enemyTurn());
	}

	void PlayerTurnStart() {
		cm.activatePlayer ();
		playersTurn = true;
	}

	IEnumerator enemyTurn() {
		Debug.Log ("enemies' turn start");
		yield return new WaitForSeconds(5.0f);
		Debug.Log ("   player's turn again");
		PlayerTurnStart ();
	}
}
