using UnityEngine;
using System.Collections;

public class SpiderHaunt : MonoBehaviour {

	// Use this for initialization

	GameObject human;
	NavMeshAgent navMeshAgent;

	const float timeout = 5f;
	float delta = 0f;
	AudioSource[] audioSources;


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

		audioSources = gameObject.GetComponents<AudioSource> ();
		gameObject.transform.localPosition = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.localPosition;

		hasReachedHumanDelta = 0.0f;
		pathRecalcDelta = 0.0f;

		bool dontPlayAudio = false;
		foreach (var asource in audioSources)
			if (asource.isPlaying)
				dontPlayAudio = true;
		
		if (!dontPlayAudio)
			audioSources[Random.Range(0,audioSources.Length)].Play(); // play sound to attract attention

	}
	
	// Update is called once per frame
	void Update () {

		if (!navMeshAgent.pathPending)
		{
			navMeshAgent.Stop();
			navMeshAgent.SetDestination (human.transform.localPosition);//new Vector3 (human.transform.localPosition.x, 1.0f, human.transform.localPosition.z));
		}

		if (Vector3.Distance(human.transform.localPosition, gameObject.transform.localPosition) <= 5.0f)
		{
			gameObject.transform.localScale = new Vector3(0, 0, gameObject.transform.localScale.z);
			gameObject.transform.localPosition = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.localPosition;

			hasReachedHumanDelta = 0.0f; // spider reached human, no need to reset, since it already has been

			bool dontPlayAudio = false;
			foreach (var asource in audioSources)
				if (asource.isPlaying)
					dontPlayAudio = true;
			
			if (!dontPlayAudio)
				audioSources[Random.Range(0,audioSources.Length)].Play(); // play sound to attract attention

		}
		else
		{
			float scale = 0.5f;
			gameObject.transform.localScale = new Vector3(scale, scale, scale);

		}

		if (delta <= 0.0) {

			delta = timeout;
		} 
		else 
		{
			delta -= Time.deltaTime;

			if (Vector3.Distance(human.transform.localPosition, gameObject.transform.localPosition) > 5f)
			{
				gameObject.animation.Play("run");
			}
		}

		if (hasReachedHumanDelta >= hasReachedHumanTime) 
		{
			hasReachedHumanDelta = 0.0f;

			float minDist = float.MaxValue;
			GameObject minDistSpawnLocation = null;

			foreach (var spawnLocation in spawnLocations)
			{
				var dist = Vector3.Distance(spawnLocation.transform.localPosition, human.transform.localPosition);
				if (dist < minDist)
				{
					minDist = dist;
					minDistSpawnLocation = spawnLocation;
				}
			}

			gameObject.transform.localPosition = spawnLocations[Random.Range(0, spawnLocations.Length)].transform.localPosition;
		} 
		else 
		{
			hasReachedHumanDelta += Time.deltaTime;
		}

		if (pathRecalcDelta >= pathRecalcTime) 
		{
			pathRecalcDelta = 0.0f;
		}
		else
		{
			pathRecalcDelta += Time.deltaTime;
		}

		gameObject.transform.LookAt (new Vector3 (human.transform.localPosition.x, 1.0f, human.transform.localPosition.z));
	}
}
