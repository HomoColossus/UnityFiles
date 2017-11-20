using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldScript : MonoBehaviour {

    public Slider slider;
    private float sliderValue;

    public GameObject human;
    public GameObject currentTarget;

    //private float timeLeft;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () 
    {

/*

        // Adds a listener to the designated slider and invokes a method when the value changes.
        // Also adds a coroutine (parallel timer) 
        slider.onValueChanged.AddListener(
            delegate {SetSliderValue(slider.value); StartCoroutine(FoodSpawnTimer(2.0f)); }
        );
*/
        // Adds a listener to the designated slider and invokes a method when the value changes.
        slider.onValueChanged.AddListener(
            delegate {SetSliderValue(slider.value);}
        );



        // Spawns human
        human = Instantiate (Resources.Load("Prefabs/human 1", typeof(GameObject))) as GameObject;
        //print ("human position from WorldScript: " +human.transform.position);
    }
	
	// Update is called once per frame
	void Update () 
    {

    }
        

  
/*  
    // Starts a timer
    IEnumerator FoodSpawnTimer(float timerValue)
    {
        yield return new WaitForSeconds(timerValue);
        // Do some stuff
        print ("spawn time!");
    }
*/

    // When button is pressed, the button should send its food object here.
    // Then, the food objects position can be sent from here to the human.<GetComponent>().SetTargetPosition ()



    private void SetSliderValue (float value)
    {
        sliderValue = value;
        print (value); 

    }

    public float GetSliderValue ()
    {
        return sliderValue;
    }

    public void RecieveFoodGO(GameObject food)
    {
        // Denna food transform är fel
        currentTarget = food;
        human.GetComponent<RealHuman>().SetTargetPosition(food.GetComponent<Food>().GetFoodPosition());
    }


    public void ReachedTarget ()
    {
        Destroy(currentTarget);
        print ("food destroyed");

//        human.GetComponent<Human>().ScaleHandle(GetSliderValue());
		human.GetComponent<RealHuman>().scalerValue = GetSliderValue();


    }

}
