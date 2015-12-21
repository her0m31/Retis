using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

public class ButtonManager : UIBehaviour {
	[SerializeField]
	Text buttonText;

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Title:
				gameObject.SetActive(true);
				buttonText.text = "START";
				break;
			case GameManager.GameState.GameOver:
				gameObject.SetActive(true);
				buttonText.text = "RESTART";
				break;
			case GameManager.GameState.Playing:
				gameObject.SetActive(false);
				break;
		}
	}

	void OnClick() {
		switch(GameManager.State.Value) {
			case GameManager.GameState.Title:
				GameManager.State.Value = GameManager.GameState.Playing;
				break;
			case GameManager.GameState.GameOver:
				GameManager.State.Value = GameManager.GameState.Restart;
				break;
		}
	}

	// Use this for initialization
	protected override void Awake() {
		base.Awake();

		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);

		GetComponent<Button>().onClick.AddListener(OnClick);
	}

	protected override void OnDestroy() {
		base.OnDestroy();

		if(GameManager.Instance != null) {
			GameManager.State.RemoveListener(OnChangeGameState);
		}

		GetComponent<Button>().onClick.RemoveListener(OnClick);
	}
}
