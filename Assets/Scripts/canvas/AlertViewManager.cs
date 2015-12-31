using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlertViewManager : ViewController {
	[SerializeField] private Text titleLabel;
	[SerializeField] private Button cancelButton;
	[SerializeField] private Text cancelButtonLabel;
	[SerializeField] private Button okButton;
	[SerializeField] private Text okButtonLabel;

	private static GameObject prefab = null;

	private System.Action cancelButtonDelegate;
	private System.Action okButtonDelegate;

	public static AlertViewManager Show() {
		if(prefab == null) {
			prefab = Resources.Load("AlertView") as GameObject;
		}

		GameObject obj = Instantiate(prefab) as GameObject;
		AlertViewManager alertView = obj.GetComponent<AlertViewManager>();

		return alertView;
	}

	public void Dismiss() {
		Destroy(gameObject);
	}

	public void OnPressCancelButton() {
		if(cancelButtonDelegate != null) {
			cancelButtonDelegate.Invoke();
		}
		Dismiss();
	}

	public void OnPressOKButton() {
		if(okButtonDelegate != null) {
			okButtonDelegate.Invoke();
		}
		Dismiss();
	}


}
