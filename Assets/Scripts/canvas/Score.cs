using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using	UnityEngine.EventSystems;

public class Score : UIBehaviour {
	private Text thisText;

	public Text text {
		get {
			return thisText == null ? thisText = base.GetComponent<Text>() : thisText;
		}
	}

	void UpdateScoreText(int score) {
		text.text = score.ToString();
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Playing:
				gameObject.SetActive(true);
				break;
			default:
				gameObject.SetActive(false);
				break;
		}
	}

	protected override void OnDestroy() {
		base.OnDestroy();
		if(GameManager.Instance != null) {
			GameManager.Score.RemoveListener(UpdateScoreText);
			GameManager.State.RemoveListener(OnChangeGameState);
		}
	}

	protected override void Start() {
		base.Start();
		UpdateScoreText(GameManager.Score.Value);
		OnChangeGameState(GameManager.State.Value);

		GameManager.Score.AddListener(UpdateScoreText);
		GameManager.State.AddListener(OnChangeGameState);
	}

	protected override void Awake() {
		base.Awake();
		thisText = base.GetComponent<Text>();
	}
}
