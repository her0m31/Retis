using UnityEngine;
using System.Collections;

public class GOManager : MonoBehaviour {

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.GameOver:
			gameObject.SetActive(true);
			break;
			default:
			gameObject.SetActive(false);
			break;
		}
	}

	void Start() {
		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}
}
