using UnityEngine;
using System.Collections;

public class PuniCon : MonoBehaviour {
	private float speed = 100.0f;
	private Vector2 start;
	private Vector2 end;

	// Use this for initialization
	void Start () {
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		if (Input.GetMouseButton(0)) {
			end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = Vector2.MoveTowards(transform.position,
			new Vector2(transform.position.x+(start.x - end.x)/-30, transform.position.y+(start.y - end.y)/-30 ),
			speed * Time.deltaTime);
		}
	}

}
