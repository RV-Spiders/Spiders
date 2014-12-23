using UnityEngine;
using System.Collections;

public class SpiderErraticMovement : MonoBehaviour {


	private CharacterController controller;
	private Transform target;

	// Use this for initialization
	void Start () {

		target = new GameObject ().transform;
		ChangeTarget ();

		controller = GetComponent<CharacterController>();
		StartCoroutine ("MoveToTarget", target);
	}

	// Update is called once per frame
	void Update () {

		animation.Play ("run");

	}

	IEnumerator MoveToTarget(Transform target)
	{
		while (true) {
		
			while (Vector3.Distance(transform.position, target.position) >= 0.05f) {
				
				transform.position = Vector3.MoveTowards (transform.position, target.position, Time.deltaTime);

				yield return null;

			}

			ChangeTarget ();
			transform.LookAt(target);
			yield return null;
		}

	}
	void ChangeTarget()
	{
		target.localPosition = new Vector3 (Random.Range (-10, 10), 0, Random.Range (-10, 10));
	}

	void OnCollisionEnter()
	{
		ChangeTarget ();
	}
}
