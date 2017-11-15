using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {



    // Main manager class
    // Track all the input that is occurring on the screen
    // Send messages out to all the colliders that are affected by it, informing them of the state of the touch input
    //


    public LayerMask touchInputMask;
	
    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;
    private RaycastHit hit;


	// Update is called once per frame
	void Update () 
    {
		

// To enable the touch to be used with mouse-presses in Unity. The code inside of here
// will only be compiled in the Unity Editor build. When we build this in iOS, this
// code won't even be considered. 
#if UNITY_EDITOR


        // if held down, or pressed down, or released
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            
            // Want to compare the old list of objects that were touched with a new list, to 
            // see what changes were made, i.e., what objects that are still being touched. 
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();
            Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);


            // If an object is hit with the touch
            if (Physics.Raycast (ray, out hit, touchInputMask))
            {

                GameObject recipient = hit.transform.gameObject;
                touchList.Add(recipient);

                // the phase of the mousepress
                if (Input.GetMouseButtonDown(0)) // if mousebutton 0 is pressed
                {
                    recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver); 
                }

                if (Input.GetMouseButtonUp(0)) // if mousebutton 0 is released
                {
                    recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver); 
                }

                if (Input.GetMouseButton(0)) // if mousebutton 0 is being held down
                {
                    recipient.SendMessage("OnTouchtay", hit.point, SendMessageOptions.DontRequireReceiver); 
                }
            }
           

            // 
            foreach (GameObject g in touchesOld)
            {

                // Which ones are not contained in the new list? Those
                // are no longer being held down. 

                if (!touchList.Contains(g))
                {
                    g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver); 
                }

            }


        }

#endif





        if (Input.touchCount > 0)
        {

            // Want to compare the old list of objects that were touched with a new list, to 
            // see what changes were made, i.e., what objects that are still being touched. 
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();


            // Go through all touches currently occurring
            foreach (Touch touch in Input.touches) 
            {
                Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);


                // If an object is hit with the touch
                if (Physics.Raycast (ray, out hit, touchInputMask))
                {
                    
                    GameObject recipient = hit.transform.gameObject;
                    touchList.Add(recipient);


                    // the phase of the touch
                    if (touch.phase == TouchPhase.Began)
                    {
                        recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver); 
                    }

                    if (touch.phase == TouchPhase.Ended)
                    {
                        recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver); 
                    }

                    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                    {
                        recipient.SendMessage("OnTouchtay", hit.point, SendMessageOptions.DontRequireReceiver); 
                    }

                    if (touch.phase == TouchPhase.Canceled)
                    {
                        recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver); 
                    }

                }
            }

            // 
            foreach (GameObject g in touchesOld)
            {

                // Which ones are not contained in the new list? Those
                // are no longer being held down. 

                if (!touchList.Contains(g))
                {
                    g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver); 
                }

            }


        }








	}







}
