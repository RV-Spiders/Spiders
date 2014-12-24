using UnityEngine;
using System.Collections;

public class SpiderDisappearWhenHumanClose : MonoBehaviour {

	public GameObject human;

	// Use this for initialization
	void Start () {

		human = GameObject.Find("First Person Controller");
	
	}
	
	// Update is called once per frame
	void Update () {
	
		float dist = Mathf.Abs(Vector3.Distance (gameObject.transform.localPosition, human.transform.localPosition));
		if (dist <= 7.0f) {
		
			gameObject.SetActive(false);
		
		}
		gameObject.transform.LookAt (new Vector3(human.transform.localPosition.x, 1, human.transform.localPosition.z));

	}
}
