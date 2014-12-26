using UnityEngine;
using System.Collections;

public class GameRunner : MonoBehaviour {
	
	private float timeout = 3f;
	private float delta = 0f;

	private int panicLevel;

	public GameObject human;
	public GameObject spiderBrown;
	public GameObject spiderBlack;
	public GameObject darkOne;
	public ArrayList spiders;

	private GameObject[] spawnLocations;
	Transform spiderLookAtPoint;

	// Use this for initialization
	void Start () {

		panicLevel = 0;
		delta = timeout;
		spiders = new ArrayList ();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Alpha1) && panicLevel != 1)
		{
			panicLevel = 1;
			//StopCoroutine ("SpawnSpider");
			StartCoroutine ("SpawnSpider", spiderBrown.transform);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) && panicLevel != 2)
		{
			panicLevel = 2;
			//StopCoroutine("HauntHuman");
			StartCoroutine ("HauntHuman");
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3) && panicLevel != 3)
		{						
			panicLevel = 3; 
			//StopCoroutine("ChargeAttackHuman");
			StartCoroutine ("ChargeAttackHuman");
		}
		else if (Input.GetKeyDown(KeyCode.Alpha4) && panicLevel != 4)
		{
			panicLevel = 4;
			//StopCoroutine("SummonDarkOne");
			StartCoroutine("SummonDarkOne");
		}
	}


	// PANIC LEVEL 1 - One little spider spawns randomly and disappears after a certain amount of time or when human gets close
	IEnumerator SpawnSpider()
	{
		// INITIALIZATION
		GameObject lightObject = (GameObject)GameObject.Find ("Point light");
		lightObject.GetComponent<Light> ().light.range = 25;	// change lighting

		GameObject newSpider;
		newSpider = (GameObject)Instantiate(spiderBrown);
		spawnLocations = (GameObject[])GameObject.FindGameObjectsWithTag ("Level1SpawnLocation");
		newSpider.AddComponent ("SpiderDisappearWhenHumanClose");
		spiderLookAtPoint = GameObject.Find("SpiderLookAt").transform;

		// EXECUTION
		while (panicLevel == 1) {
						
			if (spawnLocations.Length > 0)
			{
				GameObject location;
				float dist;

				do 
				{
					location = spawnLocations [Random.Range (0, spawnLocations.Length)];
					dist = Mathf.Abs(Vector3.Distance(human.transform.localPosition, location.transform.localPosition));
				} 
				while (dist <= 8f);

				newSpider.transform.localPosition = location.transform.localPosition;
				newSpider.transform.LookAt(spiderLookAtPoint);
				newSpider.animation.Play ("taunt");
				newSpider.audio.Stop ();
				newSpider.audio.Play();
				newSpider.SetActive(true);
			}

			yield return (new WaitForSeconds(6f));
		}

		// CLEANUP
		GameObject.Destroy (newSpider);

		yield return null;
	}

	// PANIC LEVEL 2 - One little black spider appears, runs towards human, disappears and reappears elsewhere if it gets too close
	IEnumerator HauntHuman()
	{
		//INITIALIZATION
		GameObject lightObject = (GameObject)GameObject.Find ("Point light");
		lightObject.GetComponent<Light> ().light.range = 18;	// dim light
		GameObject newSpider = (GameObject)Instantiate (spiderBlack);
		float scale = 0.5f;
		newSpider.transform.localScale = new Vector3 (scale, scale, scale);
		newSpider.AddComponent ("SpiderHaunt");

		// EXECUTION
		while (panicLevel == 2) {
				
			yield return null;

		}

		// CLEANUP
		GameObject.Destroy (newSpider);

		yield return null;
	}

	// PANIC LEVEL 3 - multiple big black spiders charge towards human; when close, they start taunting 
	IEnumerator ChargeAttackHuman()
	{
		//INITIALIZATION
		GameObject lightObject = (GameObject)GameObject.Find ("Point light");
		lightObject.GetComponent<Light> ().light.range = 10;	// dim light

		GameObject[] newSpiders = new GameObject[5]; 
		for (int i = 0; i < newSpiders.Length; i++)
		{
			newSpiders[i] = (GameObject)Instantiate (spiderBlack);
			newSpiders[i].transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
			newSpiders[i].AddComponent ("SpiderChargeAttack");

			// Note: set spiders to spawn in different locations maybe?
		}

		// EXECUTION
		while (panicLevel == 3) {
			
			yield return null;
			
		}
		
		// CLEANUP
		foreach (var spider in newSpiders) 
		{
			GameObject.Destroy (spider);
		}

		yield return null;
	}

	// PANIC LEVEL 4 - Warning: The creators of this application are not responsible for any emotional trauma you might experience from this point onward.
	IEnumerator SummonDarkOne()
	{
		// INITIALIZATION
		GameObject darkOneCopy = (GameObject)Instantiate (GameObject.Find ("DarkOne"));
		darkOneCopy.AddComponent ("DarkOneBehaviour");
		GameObject lightObject = (GameObject)GameObject.Find ("Point light");
		lightObject.GetComponent<Light> ().light.range = 5;	// dim light

		GameObject camera = (GameObject)GameObject.Find ("Main Camera");
		camera.audio.Play ();

		// EXECUTION
		while (panicLevel == 4) {
			
			yield return null;
			
		}

		// CLEANUP
		GameObject.Destroy (darkOneCopy);
		camera.audio.Stop ();
	}
}
