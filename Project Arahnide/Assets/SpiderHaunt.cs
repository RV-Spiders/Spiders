using UnityEngine;
using System.Collections;

public class SpiderHaunt : MonoBehaviour {

	// Use this for initialization

	GameObject human;
	NavMeshAgent navMeshAgent;

	float timeout = 5f;
	float delta = 0f;

	void Start () {
	
		human = GameObject.Find ("First Person Controller");

		gameObject.transform.localPosition = human.transform.localPosition - new Vector3(7, 0, 7);

		gameObject.AddComponent<NavMeshAgent>();
		navMeshAgent = (NavMeshAgent)gameObject.GetComponent<NavMeshAgent> ();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (delta <= 0) {

			delta = timeout;

			gameObject.audio.Play(); // hiss to attract attention

			if (Random.Range(0, 10) <= 3)
			{
				gameObject.transform.localScale = new Vector3(0, 0, gameObject.transform.localScale.z);
			}
			else
			{
				gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
			}
		} 
		else 
		{
			delta -= Time.deltaTime;

			if (Vector3.Distance(human.transform.localPosition, gameObject.transform.localPosition) > 5f)
			{
				gameObject.animation.Play("run");
				if (!navMeshAgent.pathPending)
					navMeshAgent.SetDestination (new Vector3(human.transform.localPosition.x, /*gameObject.transform.localPosition.y*/ 1, human.transform.localPosition.z));
			}
			else
			{
				navMeshAgent.Stop();
				gameObject.animation.Play("taunt");
			}
		}


		gameObject.transform.LookAt (new Vector3 (human.transform.localPosition.x, 1, human.transform.localPosition.z));
	}
}
