using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Title:
 				gameObject.SetActive(true);
				break;
			case GameManager.GameState.GameOver:
				gameObject.SetActive(true);
				break;
			default:
				gameObject.SetActive(false);
				break;
		}
	}

	void Start () {
		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}
}
