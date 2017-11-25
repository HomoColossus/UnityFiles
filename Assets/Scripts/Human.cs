using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Human : MonoBehaviour {

    // Attributes of human
    GameObject human;
    Rigidbody rigbod;
    Vector3 humanPosition;

    public float scalerValue;
    float scaleTime;
    Vector3 tempScaleVector;


    // Attributes of method MoveToTarget
    float movementSpeed;
    public Vector3 heading; // difference vector of target and human
    float distance; 
    Vector3 direction;


    // Attributes of target
    public GameObject target;
    Vector3 targetPosition;
    bool haveTarget;


    // OnClick display
    public GameObject onClickDisplay;



    void Start ()
    {
        InitializeHuman();
    }

    void Update()
    {

        // Move if haveTarget = true
        if (haveTarget == true)
        {
            //RotateTowardsTarget();
            MoveToTarget();
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

/*
    public void SetTargetPosition (Vector3 position)
    {
        targetPosition = position;
        //print ("food position from Human.cs: " +targetPosition);
        haveTarget = true;
        print("inside SetTargetPos with Vector3");

    }
*/
    void OnTouchDown ()
    {


        print("human touch down");
        if (onClickDisplay)
        {
            // Already instantiated, do nothing.
        }
        else 
        {
            onClickDisplay = Instantiate (Resources.Load("Prefabs/planeOnClick", typeof(GameObject))) as GameObject;
            onClickDisplay.transform.position = new Vector3(6.43f, 3.65f, -0.27f);
        }
    }



    public void SetTargetPosition (GameObject foodTarget)
    {
        target = foodTarget;
        targetPosition = foodTarget.transform.position;
        haveTarget = true;

    }


    void RotateTowardsTarget ()
    {


        heading = targetPosition - transform.position;
        distance = heading.magnitude;
        direction = heading/distance;


        // Rotate human towards target
        float rotSpeed = 1;
        float step = rotSpeed * Time.deltaTime;

        //print ("human rot: " +transform.rotation);
        //print("target")
        //print(heading);

        // human.transform.forward is backwards. 
        //Transform tempForward = new GameObject().GetComponent<Transform>().Translate(transform.forward.x + 180, transform.forward.y, transform.forward.z);

        //GameObject tempGO = new GameObject();
        //tempGO.transform.Translate(transform.forward.x + 180, transform.forward.y, transform.forward.z);

        //transform.forward = Vector3.RotateTowards(transform.position.back , heading, step, 0.0f);

        //Vector3 tempRotator = Vector3.RotateTowards(transform.forward, heading, step, 0.0f);
        //transform.forward = Vector3.RotateTowards(transform.forward, heading, step, 0.0f);
        transform.LookAt(target.transform);

        //turretTransform.rotation = Quaternion.Euler (0, lookRot.eulerAngles.y, 0);



    }


    void MoveToTarget ()
    {

        heading = targetPosition - transform.position;
        distance = heading.magnitude;
        direction = heading/distance;



        // human arrived at target position
        if (distance < 1.5) // Scale this by the human's size, because it won't reach it otherwise
        {
            //print ("human IS AT TARGET position: " +transform.position);
            //targetPosition = null; Do this in some other way
            haveTarget = false;

            //print("Reached target");
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


    public void ScaleHandle (float scaler, bool start)
    {
        if (start)
        {
            StartCoroutine(Scaler(scaler));
        }
        else
        {
            StopCoroutine(Scaler(scaler));
        }
    }


    public IEnumerator Scaler (float scaler = 0)
    {
        scaler = scaler + 1; // because of the 0 case. 



        float duration = 1.5f;
        float timeLeft = duration;
        float totalTime = duration;
        //print("scale before: " +transform.localScale);

        Vector3 a = transform.localScale; 




        // treat scaler before using it in Vector3 b
        Vector3 b = new Vector3 (a.x * scaler, a.y * scaler, a.z * scaler);



        float i = 5.0f;



        while (timeLeft > 0)
        {

            timeLeft -= Time.deltaTime;

            i = (totalTime - timeLeft) / totalTime;

            transform.localScale = Vector3.Lerp(a, b, i);
            //print (i);

            yield return null;
        }

        //print("scale after: " +transform.localScale);

    }



}
