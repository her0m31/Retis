using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuitConfirmationView : ViewController {

	public void OnPressConfirmButton() {
		AlertViewManager.Show();
	}
}
