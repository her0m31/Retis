using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	private Nortification<int> score;
	public static Nortification<int> Score {
		get {
			return Instance.score;
		}
	}

	private Nortification<GameState> state;
	public static Nortification<GameState> State {
		get {
			return Instance.state;
		}
	}

	public enum GameState {
		Title,
		Playing,
		GameOver,
		Restart
	}

	public static int CurrentScore {
		get {
			return Score.Value;
		}
		set {
			Score.Value = value;
		}
	}

	public static GameState CurrentState {
		get {
			return State.Value;
		}
		set {
			State.Value = value;
		}
	}

	public static bool IsPlaying() {
		return CurrentState == GameManager.GameState.Playing ? true : false;
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Restart:
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				break;
		}
	}

	void OnDestroy() {
		Score.DisposeOf();
		State.DisposeOf();
	}

	void Start () {
		State.AddListener(OnChangeGameState);
	}

	void Awake() {
		if(this != Instance) {
			Destroy(this);
			return;
		}
		score = new Nortification<int>(0);
		state = new Nortification<GameState>(GameState.Title);
	}
}
