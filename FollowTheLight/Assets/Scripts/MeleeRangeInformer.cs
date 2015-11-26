﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AttackType { Strike, Slash };

public class MeleeRangeInformer : MonoBehaviour {
	public AttackType attackType;
	bool wouldHitEnemy;
	List<GameObject> hitList;

	void Start () {
		wouldHitEnemy = false;
		hitList = new List<GameObject> ();
	}

	void OnTriggerEnter (Collider other) {
		if ((other.GetType() == typeof(CapsuleCollider)) && other.tag == "Enemy") {
			GameObject enemyObj = other.gameObject.transform.parent.gameObject;
			hitList.Add(enemyObj);
			CheckHitListSize();
		}
	}

	void OnTriggerExit (Collider other) {
		if ((other.GetType() == typeof(CapsuleCollider)) && other.tag == "Enemy") {
			GameObject enemyObj = other.gameObject.transform.parent.gameObject;
			hitList.Remove(enemyObj);
			CheckHitListSize();
		}
	}

	void CheckHitListSize() {
		if (hitList.Count > 0) {
			if (!wouldHitEnemy) {
				wouldHitEnemy = true;
				UpdateHitStatus();
			}
		} else {
			if (wouldHitEnemy) {
				wouldHitEnemy = false;
				UpdateHitStatus();
			}
		}
	}

	void UpdateHitStatus() {

	}
}