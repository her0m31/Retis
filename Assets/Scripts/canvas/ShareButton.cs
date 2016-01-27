using UnityEngine;
using UnityEngine.UI;
using	UnityEngine.EventSystems;
using System.Collections;

public class ShareButton : MonoBehaviour {
	[SerializeField] private Text buttonText;
	[SerializeField] private Blinker blink;
	private Button thisButton;
	private AudioSource clickSound;

	public Button button {
		get {return thisButton == null ? thisButton = base.GetComponent<Button>() : thisButton;}
	}

	IEnumerator Share() {
		yield return new WaitForSeconds(0.5f);
		if(Everyplay.IsRecording()) {
			Everyplay.StopRecording();
			Everyplay.SetMetadata("score", GameManager.Score.Value);
		}
		Everyplay.ShowSharingModal();
	}

	void OnClick() {
		clickSound.PlayOneShot(clickSound.clip);
		StartCoroutine(Share());
	}

	void OnDestroy() {
		GetComponent<Button>().onClick.RemoveListener(OnClick);
	}

	void Awake() {
		GetComponent<Button>().onClick.AddListener(OnClick);
		clickSound = GetComponent<AudioSource>();
	}
}
