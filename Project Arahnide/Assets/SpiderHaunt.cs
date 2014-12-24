using UnityEngine;
using System.Collections;

public class SpiderHaunt : MonoBehaviour {

	// Use this for initialization

	GameObject human;

	float timeout = 5f;
	float delta = 0f;

	void Start () {
	
		human = GameObject.Find ("First Person Controller");

		gameObject.transform.localPosition = human.transform.localPosition - new Vector3(7, 0, 7);

	}
	
	// Update is called once per frame
	void Update () {
	
		if (delta <= 0) {

			delta = timeout;

			gameObject.audio.Play(); // hiss to attract attention

			if (Random.Range(0, 10) <= 3)
			{

			}
			else
			{
				//gameObject.animation.Play("taunt");
			}
		} 
		else 
		{
			delta -= Time.deltaTime;
		}
		gameObject.animation.Play ("run");
		///gameObject.rigidbody.velocity = new Vector3 (3, 0, 0);
		gameObject.transform.LookAt (new Vector3(human.transform.localPosition.x, 2, human.transform.localPosition.z));
		gameObject.transform.localPosition += gameObject.transform.forward * 0.05f;  // Vector3.Slerp (gameObject.transform.localPosition, new Vector3(human.transform.localPosition.x, gameObject.transform.localPosition.y, human.transform.localPosition.z), Time.deltaTime);
	}
}
