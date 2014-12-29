using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	LineRenderer line;

	// Use this for initialization
	void Start () 
	{
		line = gameObject.GetComponent<LineRenderer> ();
		line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Fire1")) 
		{
			StopCoroutine("FireLaser");
			StartCoroutine("FireLaser");
		}
	}

	IEnumerator FireLaser()
	{
		line.enabled = true;

		while (Input.GetButton("Fire1")) 
		{
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;

			line.SetPosition(0, ray.origin);
			if(Physics.Raycast(ray, out hit, 100))
			{
				var objectHit = hit.transform.gameObject;

				var objectHitName = objectHit.name;
				objectHitName = objectHitName.Replace("(Clone)", string.Empty);

				float hitCoeff = 1.0f;

				if ("SpiderBlack".Equals(objectHitName))
				{
					hitCoeff = 1.2f;
				}
				else if ("SpiderBrown".Equals(objectHitName))
				{
					hitCoeff = 1.2f;
				}
				else if ("DarkMinion".Equals(objectHitName))
				{
					hitCoeff = 1.3f;
				}
				else if ("DarkOne".Equals(objectHitName))
				{
					hitCoeff = 9.0f;
				}

				line.SetPosition(1, hit.point);
				if(hit.rigidbody)
				{
					hit.rigidbody.AddForceAtPosition(transform.forward * hitCoeff, hit.point);
				}
			}
			else
				line.SetPosition(1, ray.GetPoint(100));
			
			yield return null;
		}
		
		line.enabled = false;
	}
}
