using UnityEngine;
using System.Collections;

public class GameRunner : MonoBehaviour {
	
	private float timeout = 3f;
	private float delta = 0f;

	private int panicLevel;

	public GameObject human;
	public GameObject spiderBrown;
	public GameObject spiderBlack;
	public ArrayList spiders;

	private GameObject[] spawnLocations;
	Transform spiderLookAtPoint;

	// Use this for initialization
	void Start () {

		panicLevel = 1;
		delta = timeout;
		spiders = new ArrayList ();

		StartCoroutine ("SpawnSpider", spiderBrown.transform);
		//StartCoroutine ("HauntHuman", spiderBlack.transform);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.Alpha1))
		{
			panicLevel = 1;
		}
		else if (Input.GetKey(KeyCode.Alpha2))
		{
			panicLevel = 2;
		}
		else if (Input.GetKey(KeyCode.Alpha3))
		{
			panicLevel = 3; 
		}

		switch (panicLevel) {
				
		case 1:

			// spawn one spider in different locations, not too close to human
			// when human gets too close, spider disappears

			break;

		case 2:

			// two spiders
			// one randomly crawling on ceiling, walls etc.
			// one randomly appearing behind human and hissing

			break;

		case 3:

			// code for level 3

			break;

		default:
			break;
		}
	}

	// PANIC LEVEL 1
	IEnumerator SpawnSpider(Transform t)
	{
		// INITIALIZATION
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
				} while (dist <= 8f);

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

	IEnumerator HauntHuman(Transform t)
	{
		// LEVEL 2

		//INITIALIZATION
		GameObject lightObject = (GameObject)GameObject.Find ("Point light");
		lightObject.GetComponent<Light> ().light.range = 5;
		GameObject newSpider = (GameObject)Instantiate (spiderBlack);
		newSpider.transform.localScale = new Vector3 (2f, 2f, 2f);
		newSpider.AddComponent ("SpiderHaunt");

		// EXECUTION
		while (panicLevel == 2) {
				
			yield return null;

		}

		yield return null;
	}

}
