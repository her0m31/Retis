using UnityEngine;
using System.Collections;

public class BackKey : MonoBehaviour {
	void Update() {
		if(Input.GetKey(KeyCode.Escape)) {
			if(GameManager.GameState.Title == GameManager.State.Value) {
				Application.Quit();
				return;
			}
			GameManager.State.Value = GameManager.GameState.Restart;
		}
		if(Input.GetKey(KeyCode.Home)) {
			Application.Quit();
			return;
		}
	}
}
