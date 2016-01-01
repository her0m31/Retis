using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : SingletonMonoBehaviour<GameManager> {
	private Nortification<int> score;
	private Nortification<GameState> state;

	public static Nortification<int> Score {
		get {return Instance.score;}
	}

	public static Nortification<GameState> State {
		get {return Instance.state;}
	}

	public enum GameState {
		Title, Ready, Playing, GameOver, Restart
	}

	public static bool IsPlay() {
		return State.Value == GameState.Playing ? true : false;
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
