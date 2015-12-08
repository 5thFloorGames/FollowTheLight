﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	CharacterManager cm;
	EnemyManager em;
	UserInterfaceManager uim;
	bool levelCompleted;

    LevelObjective objective;

    bool initialized = false;

    void Awake() {
		DontDestroyOnLoad (gameObject);
        GameState.SetLevel(Application.loadedLevel);
    }

	void Start () {
        #if !UNITY_EDITOR
            Cursor.visible = false;
        #endif
        cm = gameObject.GetComponent<CharacterManager> ();
		em = gameObject.GetComponent<EnemyManager> ();
        uim = GameObject.Find("UserInterface").GetComponent<UserInterfaceManager>();

        if (!initialized) {
            initialized = true;
            levelCompleted = false;
            Invoke("LateStart", 0.1f);
        }
	}

    void OnLevelWasLoaded(int level) {

        if (level == 0) {
            Destroy(gameObject);
        }

        if (!initialized && level != 0) {
            initialized = true;
            levelCompleted = false;
            Invoke("LateStart", 0.1f);
        }
    }

    void LateStart () {
		StartPlayerTurn ();
        initialized = false;
	}

	void Update () {

        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.N)) {
            LevelComplete();
        }
        #endif

        if (Input.GetButton ("Cancel")) {
			QuitGame();
		}
		if (GameState.playersTurn) {
			if (Input.GetKeyDown (KeyCode.Tab)) {
				StartEnemyTurn();
			}
		}
	}


    public void LevelComplete() {
		if (!levelCompleted) {
			levelCompleted = true;

			uim.ShowLevelCompletedUI();
			GameState.playersTurn = false;
            StartCoroutine(LevelCompletedLoadNextIn(3.0f));
		}
	}

    IEnumerator LevelCompletedLoadNextIn(float seconds) {
        yield return new WaitForSeconds(seconds);
        uim.HideLevelCompletedUI();
        if (GameState.GetLevel() == GameState.GetLastLevel()) {
            QuitGame();
        } else {
            GameState.LevelComplete();
            LoadNextLevel();
        }
    }

    public void LevelFailed() {
        if (!levelCompleted) {
            levelCompleted = true;

            uim.HideEnemyUI();
            uim.ShowLevelFailedUI();
            GameState.playersTurn = false;
            StartCoroutine(LevelFailedLoadSameIn(3.0f));
        }
    }

    IEnumerator LevelFailedLoadSameIn(float seconds) {
        yield return new WaitForSeconds(seconds);
        uim.HideLevelFailedUI();
        LoadNextLevel();
    }



    public void AllCharactersDead() {
        LevelFailed();
    }

    public void AllCharactersInLevelEnd() {
        if (objective == LevelObjective.GetToLevelEnd) {
            LevelComplete();
        }
    }

    public void AllEnemiesDestroyed() {
        if (objective == LevelObjective.DestroyEnemies) {
            LevelComplete();
        }
    }



	void StartEnemyTurn() {
		GameState.playersTurn = false;
		uim.ShowEnemyUI ();
		em.TriggerEnemyActions ();
	}

    public void EnemyTurnOver() {
        StartCoroutine(StartPlayerTurnIn(0.1f));
    }

    IEnumerator StartPlayerTurnIn(float seconds) {
        yield return new WaitForSeconds(seconds);
        StartPlayerTurn();
    }

    void StartPlayerTurn() {
		uim.HideEnemyUI ();
        cm.PlayersTurnActivated ();
		em.PlayersTurnActivated ();
		GameState.playersTurn = true;
	}



    public void SetLevelObjective(LevelObjective obj) {
        objective = obj;
        Debug.Log("Objective: " + objective.ToString());
    }

    void LoadNextLevel() {
        Application.LoadLevel(GameState.GetLevel());
    }

    void QuitGame() {
        GameState.Reset();
        Application.LoadLevel(0);
    }

}
