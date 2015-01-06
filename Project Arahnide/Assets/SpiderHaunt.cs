using UnityEngine;
using System.Collections;

public class SpiderHaunt : MonoBehaviour {

	// Use this for initialization

	GameObject human;
	NavMeshAgent navMeshAgent;

	float timeout = 5f;
	float delta = 0f;
	private GameObject[] spawnLocations;
	AudioSource[] audioSources;

	const float hasReachedHumanTime = 10f;
	float hasReachedHumanDelta;

	void Start () {
		hasReachedHumanDelta = 0.0f;

		human = GameObject.Find ("First Person Controller");
		spawnLocations = GameObject.FindGameObjectsWithTag ("Level2SpawnLocation"); 
		gameObject.transform.localPosition = spawnLocations [Random.Range (0, spawnLocations.Length)].transform.localPosition;
		audioSources = (AudioSource[])gameObject.GetComponents<AudioSource> ();

		gameObject.AddComponent<NavMeshAgent>();
		navMeshAgent = (NavMeshAgent)gameObject.GetComponent<NavMeshAgent> ();

		audioSources[Random.Range(0, audioSources.Length)].Play(); // hiss to attract attention

	}
	
	// Update is called once per frame
	void Update () {
	
		if (delta <= 0) {

			delta = timeout;
	
		} 
		else 
		{
			delta -= Time.deltaTime;

			if (Vector3.Distance(human.transform.localPosition, gameObject.transform.localPosition) > 5f)
			{
				gameObject.animation.Play("run");
				if (!navMeshAgent.pathPending)
				{
					navMeshAgent.SetDestination (new Vector3(human.transform.localPosition.x, /*gameObject.transform.localPosition.y*/ 1, human.transform.localPosition.z));
				}
			}
			else
			{
				audioSources[Random.Range(0, audioSources.Length)].Play(); // hiss to attract attention
				gameObject.transform.localPosition = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.localPosition;

				navMeshAgent.ResetPath();
			}
		}

		if (hasReachedHumanDelta >= hasReachedHumanTime) // if spider hasn't reached human, it was maybe because of shoddy pathing => reset spider
		{
			hasReachedHumanDelta = 0.0f;
			
			gameObject.transform.localPosition = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.localPosition;
		} 
		else 
		{
			hasReachedHumanDelta += Time.deltaTime;
		}

		gameObject.transform.LookAt (new Vector3 (human.transform.localPosition.x, 1, human.transform.localPosition.z));
	}
}
