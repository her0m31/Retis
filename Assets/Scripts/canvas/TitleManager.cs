using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {
	private AudioSource titleBgm;
	private AudioSource gameOverBgm;
	private AudioSource outSound;

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Title:
 				gameObject.SetActive(true);
				titleBgm.PlayOneShot(titleBgm.clip);
				break;
			case GameManager.GameState.GameOver:
				gameObject.SetActive(true);
				gameOverBgm.PlayOneShot(gameOverBgm.clip);
				outSound.PlayOneShot(outSound.clip);
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

	void Awake() {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		titleBgm = audioSources[0];
		gameOverBgm = audioSources[1];
		outSound = audioSources[2];
	}
}
