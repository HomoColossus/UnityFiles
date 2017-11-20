using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class RealHuman : MonoBehaviour {

	// Attributes of human
	GameObject human;
	Rigidbody rigbod;
	Vector3 humanPosition;
	Animator anim;
	public float scalerValue;
	float scaleTime;
	Vector3 tempScaleVector;


	// Attributes of method MoveToTarget
	float movementSpeed;
	Vector3 heading; // difference vector of target and human
	float distance; 
	Vector3 direction;


	// Attributes of target
	GameObject target;
	Vector3 targetPosition;
	bool haveTarget;



	void Start ()
	{
		InitializeHuman();
		anim = GetComponent<Animator> ();
	}

	void Update()
	{

		// Move if haveTarget = true
		if (haveTarget == true)
		{

			MoveToTarget();
			anim.Play (Animator.StringToHash ("Walk"));
		}

	}


	void InitializeHuman ()
	{
		transform.SetParent (GameObject.FindGameObjectWithTag("Plane").transform);
		transform.position = new Vector3(Random.Range(-3.0f, 3.0f), 0.5f, Random.Range(-3.0f, 3.0f));
		movementSpeed = 2;
		haveTarget = false;

		scalerValue = 1;

	}


	public void SetTargetPosition (Vector3 position)
	{
		targetPosition = position;
		//print ("food position from Human.cs: " +targetPosition);
		haveTarget = true;

	}


	void MoveToTarget ()
	{

		heading = targetPosition - transform.position;
		distance = heading.magnitude;
		direction = heading/distance;

		// human arrived at target position
		if (distance < 0.5) // Scale this by the human's size, becaus
		{
			//print ("human IS AT TARGET position: " +transform.position);
			//targetPosition = null; Do this in some other way
			haveTarget = false;

			print("Reached target");
			GameObject.FindGameObjectWithTag("WorldScript").GetComponent<WorldScript>().ReachedTarget();
			// then destroy target (from worldscript)

		}

		// human have not yet arrived at target position
		else 
		{
			transform.Translate (direction * Time.deltaTime * movementSpeed);
			haveTarget = true;


		}
	}

	/*
    public void ScaleHandle (float scaler)
    {

        StartCoroutine(Scaler());
    }


    IEnumerator Scaler ()
    {
        float timeLeft = 3.0f;
        Vector3 a = transform.localPosition; 
        Vector3 b = new Vector3 (a.x*0.2f, a.y*0.2f, a.z*0.2f);
        float i = ;


        while (timeLeft > 0)
        {

            timeLeft -= Time.deltaTime;

            // vad beror i på?


            print("timeLeft: " +timeLeft); 

            transform.localScale = Vector3.Lerp(a, b, i);


            yield return null;
        }


    }

*/



	/*
	Vector3 tempScaleVector = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z); 
	tempScaleVector += new Vector3(1.0f*scaler, 1.0f*scaler, 1.0f*scaler);

	scaleTime = 5.0f;
	transform.localScale = Vector3.Lerp(transform.localScale, tempScaleVector, scaleTime);
	*/

	//StartCoroutine(LerpScale(2.0f)); 






	/*
    // Starts a timer
	IEnumerator LerpScale(float scaleTime)
	{

		while (time > 0.0f)
		{
			time -= Time.deltaTime;
		}

		transform.localScale = Vector3.Lerp(transform.localScale, tempTargetScale, time / scaleTime);

		yield return new WaitForSeconds(timerValue);
		// Do some stuff
		print ("spawn time!");
	}
	*/




}

