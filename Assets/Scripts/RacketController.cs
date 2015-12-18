using UnityEngine;
using System.Collections;

public class RacketController : MonoBehaviour {
	private Vector2 basePoint = new Vector2(0.0f, 0.0f);
	private Vector2 nextPoint = new Vector2(0.0f, 0.0f);

	private float speed = 100.0f;
	private Vector2 start;
	private Vector2 end;

	void Awake() {

	}

	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			start = Camera.main.ScreenToWorldPoint(Input.mousePosition);


			nextPoint.x = 0.0f;
			nextPoint.y = 0.0f;
			basePoint.x = Input.mousePosition.x;
			basePoint.y = Input.mousePosition.y;
		}

		if(Input.GetMouseButton(0)) {
			end = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			transform.position = Vector2.MoveTowards(transform.position,
			new Vector2(transform.position.x+(start.x - end.x)/-30, transform.position.y),
			speed);


			nextPoint.x = basePoint.x - Input.mousePosition.x;
			nextPoint.y = basePoint.y - Input.mousePosition.y;

			basePoint.x = Input.mousePosition.x;
			basePoint.y = Input.mousePosition.y;

			Vector3 aular = new Vector3(0.0f, 0.0f, nextPoint.y);

			transform.Rotate(aular, Space.World);
		}
	}
}
