using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using	UnityEngine.EventSystems;

public class GOScore : UIBehaviour {

	private Text thisText;
	public Text text {
		get {return thisText == null ? thisText = base.GetComponent<Text>() : thisText;}
	}

	void UpdateScoreText(int score) {
		text.text = text.text + score.ToString();
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.GameOver:
			gameObject.SetActive(true);
			UpdateScoreText(GameManager.Score.Value);
			break;
			default:
			gameObject.SetActive(false);
			break;
		}
	}

	protected override void OnDestroy() {
		base.OnDestroy();

		if(GameManager.Instance != null) {
			GameManager.State.RemoveListener(OnChangeGameState);
		}
	}

	protected override void Start() {
		base.Start();
		
		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}
}
