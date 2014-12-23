using UnityEngine;
using System.Collections;

public class GameRunner : MonoBehaviour {
	
	private float timeout = 3f;
	private float delta = 0f;

	private int state;

	public GameObject spider;
	public ArrayList spiders;
	private GameObject newSpider;
	// Use this for initialization

	Transform spiderLookAtPoint;

	public AudioSource audio1;
	public AudioSource audio2;

	void Start () {

		state = 1;
		delta = timeout;
		spiders = new ArrayList ();
		newSpider = (GameObject)Instantiate(spider);
		newSpider.AddComponent("SpiderStalk");

		StartCoroutine ("SpawnSpider", newSpider.transform);

		spiderLookAtPoint = GameObject.Find("SpiderLookAt").transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.Alpha1))
		{
			state = 1;
		}
		else if (Input.GetKey(KeyCode.Alpha2))
		{
			state = 2;
		}
		else if (Input.GetKey(KeyCode.Alpha3))
		{
			state = 3; 
		}

		switch (state) {
				
		case 1:

			spiderLookAtPoint = GameObject.Find("SpiderLookAt").transform;

			//newSpider.transform.LookAt(spiderLookAtPoint);

			// code for level 1
			/*
			if (delta <= 0.0f)
			{
				if (spiders.Count < 3) 
				{
					GameObject newSpider = (GameObject)Instantiate(spider, new Vector3(-3, 1, 5), Quaternion.identity);
					//MonoBehaviour script = (MonoBehaviour)GameObject.Find("SpiderErraticMovement.cs");
					newSpider.AddComponent("SpiderErraticMovement");
					spiders.Add(newSpider);
				}
				delta = timeout;
			}
			else 
			{
				delta -= Time.deltaTime;
			}*/

			// LEVEL 1, make spider follow person

			//newSpider.transform.LookAt(person.transform);
			//newSpider.transform.localPosition.Set(person.transform.localPosition.x, person.transform.localPosition.y, person.transform.localPosition.z);


			// LEVEL 1, spawn spider in random places, make a taunt, remove spider



			break;

		case 2:

			// code for level 2

			break;

		case 3:

			// code for level 3

			break;

		default:
			//throw new Exception();
			break;
		}
	}

	IEnumerator SpawnSpider(Transform t)
	{
		while (true) {
						
			GameObject[] spawnLocations = (GameObject[])GameObject.FindGameObjectsWithTag ("Level1SpawnLocation");
			if (spawnLocations.Length > 0)
			{
				GameObject location;
				do
				{
				  	location = spawnLocations [Random.Range (0, spawnLocations.Length)];
				}
				while (location.transform.localPosition == newSpider.transform.localPosition);
				newSpider.transform.localPosition = location.transform.localPosition;
				newSpider.transform.LookAt(spiderLookAtPoint);
				newSpider.animation.Play ("taunt");
				newSpider.audio.Play();
				/*if (Random.Range(0, 2) == 0)
				{
					audio1.Play();
				}
				else
				{
					audio2.Play();
				}*/
			}
			yield return (new WaitForSeconds(4f));
		}


	}
}
