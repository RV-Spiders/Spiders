using UnityEngine;
using System.Collections;

public class SpiderChargeAttack : MonoBehaviour {
	
	GameObject human;
	NavMeshAgent navMeshAgent;
	
	float hissTimeout = 5f;
	float hissDelta = 0f;
	AudioSource[] asources;
	
	
	const float hasReachedHumanTime = 10f;
	float hasReachedHumanDelta;
	const float pathRecalcTime = 2.0f;
	float pathRecalcDelta;
	private GameObject[] spawnLocations;
	void Start () {
		
		human = GameObject.Find ("First Person Controller");
		
		spawnLocations = GameObject.FindGameObjectsWithTag ("Level2SpawnLocation"); 
		
		gameObject.AddComponent<NavMeshAgent>();
		navMeshAgent = (NavMeshAgent)gameObject.GetComponent<NavMeshAgent> ();
		
		asources = gameObject.GetComponents<AudioSource> ();
		gameObject.transform.localPosition = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.localPosition;
		
		hasReachedHumanDelta = 0.0f;
		pathRecalcDelta = pathRecalcTime;

		bool dontPlayAudio = false;
		foreach (var asource in asources)
			if (asource.isPlaying)
				dontPlayAudio = true;
		
		if (!dontPlayAudio)
			asources[Random.Range(0,asources.Length)].Play(); // play sound to attract attention

		hissTimeout = Random.Range (3f, 15f);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (hissDelta <= 0.0) {

			bool dontPlayAudio = false;
			foreach (var asource in asources)
				if (asource.isPlaying)
					dontPlayAudio = true;
			
			if (!dontPlayAudio)
				asources[Random.Range(0,asources.Length)].Play(); // play sound to attract attention

			hissTimeout = Random.Range (3f, 15f);
			hissDelta = hissTimeout;

		} 
		else 
		{
			hissDelta -= Time.deltaTime;
			
			if (Vector3.Distance(human.transform.localPosition, gameObject.transform.localPosition) > 5f)
			{
				navMeshAgent.Resume();
				gameObject.animation.Play("run");
			}
			else
			{			
				navMeshAgent.Stop();
				hasReachedHumanDelta = 0.0f; // spider reached human, no need to reset, since it already has been
				gameObject.animation.Play("taunt");
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
		
		gameObject.transform.LookAt (new Vector3 (human.transform.localPosition.x, 1.0f, human.transform.localPosition.z));
		
		if (pathRecalcDelta >= pathRecalcTime) 
		{
			if (!navMeshAgent.pathPending)
				navMeshAgent.SetDestination (new Vector3 (human.transform.localPosition.x, 1.0f, human.transform.localPosition.z));
			pathRecalcDelta = 0.0f;
		}
		else
		{
			pathRecalcDelta += Time.deltaTime;
		}
	}
}
