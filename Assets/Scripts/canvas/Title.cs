using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

public class Title : UIBehaviour {
	private AudioSource countdownSound;
	private AudioSource playingBgm;

	private Color red;
	private Color blue;

	private Text thisText;
	public Text text {
		get {return thisText == null ? thisText = base.GetComponent<Text>() : thisText;}
	}

	IEnumerator CountdownFontSize() {
		for(int i = 0; i < 45; i++) {
			text.fontSize += 4;
			yield return new WaitForSeconds(0.01f);
		}
	}

	IEnumerator CountdownCoroutine() {
		text.color = red;
		string[] count = new string[] {"3", "2", "1"};
		for(int i = 0; i < 3; i++) {
			countdownSound.PlayOneShot(countdownSound.clip);
			text.text = count[i];
			text.fontSize = 0;
			StartCoroutine(CountdownFontSize());
			yield return new WaitForSeconds(1.0f);
		}
		text.fontSize = 50;
		text.color = blue;
		GameManager.State.Value = GameManager.GameState.Playing;
	}

	private string EndWord() {
		string[] EndWords = new string[] {
			"   GameOver...",
			"Oops!",
			"Hmm....",
			"Cheer up!",
		};

		return EndWords[Random.Range(0, EndWords.Length)];
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Title:
			text.text = "Retis.";
			text.fontSize = 70;
			text.enabled  = true;
			break;
			case GameManager.GameState.Ready:
			StartCoroutine(CountdownCoroutine());
			break;
			case GameManager.GameState.GameOver:
			playingBgm.Stop();
			text.text = EndWord();
			text.fontSize = 45;
			text.enabled  = true;
			break;
			case GameManager.GameState.Playing:
			text.enabled = false;
			playingBgm.PlayOneShot(playingBgm.clip);
			break;
			default:
			text.enabled = false;
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

	protected override void Awake() {
		red  = new Color32(231, 76, 60, 200);
		blue = new Color32(52, 73, 94, 255);

		AudioSource[] audioSources = GetComponents<AudioSource>();
		countdownSound = audioSources[0];
		playingBgm = audioSources[1];
	}
}
