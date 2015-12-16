using UnityEngine;
using System.Collections;

public class patt : MonoBehaviour {
	private ParticleSystem pat;
	private Vector2 start;
	private Vector2 end;

	// Use this for initialization
	void Start () {
		pat = GetComponent<ParticleSystem>();
	}

	// Update is called once per frame
	void Update () {
		float distance = 3;

		if (Input.GetMouseButtonDown(0)) {
			start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			transform.position = start;
			pat.Play(true);
		}

		if (Input.GetMouseButton(0)) {
			end = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			ParticleSystem.Particle[] particles = new ParticleSystem.Particle[GetComponent<ParticleSystem>().particleCount];

			int count = GetComponent<ParticleSystem>().GetParticles(particles);

			for(int i = 0; i < count; i++) {
				particles[i].velocity = new Vector2((start.x - end.x)*-1, (start.y - end.y)*-1);
			}

			GetComponent<ParticleSystem>().SetParticles(particles, count);
		}

		if (Input.GetMouseButtonUp(0)) {
			pat.Stop(true);
		}

	}
}
