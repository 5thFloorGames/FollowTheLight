﻿using UnityEngine;
using System.Collections;

public class LookAtActiveCharacterReverse : MonoBehaviour {

    void Start() {

    }

    void Update() {
        if (GameState.activeCharacter != null) {
            transform.LookAt(GameState.activeCharacter.transform, Vector3.up);
        }
    }
}
