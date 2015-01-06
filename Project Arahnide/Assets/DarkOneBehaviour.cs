using UnityEngine;
using System.Collections;

public class DarkOneBehaviour : MonoBehaviour {
	
	GameObject human;
	NavMeshAgent navMeshAgent;
	
	const float timeout = 5f;
	float delta = 0f;
	AudioSource[] audioSources;
	
	
	const float hasReachedHumanTime = 60f;
	float hasReachedHumanDelta;
	const float pathRecalcTime = 0.5f;
	float pathRecalcDelta;
	private GameObject[] spawnLocations;
	private GameObject[] minions;
	private int minionCount;

	void Start () {

		minionCount = 0;
		minions = new GameObject[10];	// this is not used because, for now, I have no idea how to destroy the minions when the big one is destroyed

		human = GameObject.Find ("First Person Controller");
		spawnLocations = GameObject.FindGameObjectsWithTag ("DarkOneSpawner");

		gameObject.AddComponent<NavMeshAgent>();
		navMeshAgent = (NavMeshAgent)gameObject.GetComponent<NavMeshAgent> ();
		
		audioSources = gameObject.GetComponents<AudioSource> ();
		gameObject.transform.localPosition = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.localPosition;
		
		hasReachedHumanDelta = 0.0f;
		pathRecalcDelta = pathRecalcTime;

		audioSources[0].Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (delta <= 0.0) {
			
			delta = timeout;

		} 
		else 
		{
			delta -= Time.deltaTime;
			
			if (Vector3.Distance(human.transform.localPosition, gameObject.transform.localPosition) > 6f)
			{
				if (!gameObject.animation.IsPlaying("taunt"))
					gameObject.animation.Play("run");

				if (!navMeshAgent.pathPending)
				{
					navMeshAgent.SetDestination (new Vector3 (human.transform.localPosition.x, 1.0f, human.transform.localPosition.z));
				}

			}
			else
			{			
				hasReachedHumanDelta = 0.0f; // spider reached human, no need to reset, since it already has been
				if (!gameObject.animation.IsPlaying("taunt") && !audioSources[0].isPlaying)
				{
					gameObject.animation.Play("taunt");
					audioSources[0].Play ();
				}

				navMeshAgent.ResetPath();
			}
		}

		if (Random.Range (0, 1000) < 1) // 0.1% chance to stop and taunt 
		{ 			
			navMeshAgent.Stop();
			audioSources[0].Play();
			gameObject.animation.Play("taunt");

			if (!gameObject.animation.IsPlaying("taunt") && !audioSources[0].isPlaying)
				navMeshAgent.Resume ();
/*
			minions[minionCount] = (GameObject)Instantiate(GameObject.Find("DarkMinion"));
			minions[minionCount].AddComponent("SpiderChargeAttack");
			minions[minionCount].transform.localPosition = gameObject.transform.localPosition;*/
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
		
	/*	if (pathRecalcDelta >= pathRecalcTime)	// recalculate pathing
		{
			if (!navMeshAgent.pathPending)
			{
				navMeshAgent.ResetPath();
				navMeshAgent.SetDestination (new Vector3 (human.transform.localPosition.x, 1.0f, human.transform.localPosition.z));
			}
			pathRecalcDelta = 0.0f;
		}
		else
		{
			pathRecalcDelta += Time.deltaTime;
		}*/
	}
}