using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

public class ButtonManager : UIBehaviour {
	[SerializeField] private Text buttonText;
	private Button thisButton;
	private AudioSource clickSound;

	public Button button {
		get {return thisButton == null ? thisButton = base.GetComponent<Button>() : thisButton;}
	}

	IEnumerator Ready() {
		yield return new WaitForSeconds(0.5f);
		GameManager.State.Value = GameManager.GameState.Ready;
	}

	IEnumerator Restart() {
		yield return new WaitForSeconds(0.5f);
		GameManager.State.Value = GameManager.GameState.Restart;
	}

	void OnClick() {
		switch(GameManager.State.Value) {
			case GameManager.GameState.Title:
				button.interactable = false;
				clickSound.PlayOneShot(clickSound.clip);
				StartCoroutine(Ready());
				break;
			case GameManager.GameState.GameOver:
				button.interactable = false;
				clickSound.PlayOneShot(clickSound.clip);
				StartCoroutine(Restart());
				break;
		}
	}

	void OnChangeGameState(GameManager.GameState state) {
		switch(state) {
			case GameManager.GameState.Title:
				gameObject.SetActive(true);
				button.interactable = true;
				buttonText.text = "-";
				buttonText.fontSize = 150;
				break;
			case GameManager.GameState.GameOver:
				gameObject.SetActive(true);
				button.interactable = true;
				buttonText.text = "2";
				buttonText.fontSize = 100;
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
		GetComponent<Button>().onClick.RemoveListener(OnClick);
	}

	protected override void Start() {
		base.Start();

		OnChangeGameState(GameManager.State.Value);
		GameManager.State.AddListener(OnChangeGameState);
	}

	protected override void Awake() {
		base.Awake();

		GetComponent<Button>().onClick.AddListener(OnClick);
		clickSound = GetComponent<AudioSource>();
	}
}
