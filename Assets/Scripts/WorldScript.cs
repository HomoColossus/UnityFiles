using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldScript : MonoBehaviour {

    public Slider slider;
    private float sliderValue;

    public GameObject human;
    public GameObject currentTarget;

    public GameObject growHumanDisplay;
    public GameObject refHumanDisplay;

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

        // Spawn displays
        growHumanDisplay = Instantiate (Resources.Load("Prefabs/overviewDisplay", typeof(GameObject))) as GameObject;
        growHumanDisplay.transform.position = new Vector3(-2.8f, 0.01f, -2.95f);

        refHumanDisplay = Instantiate (Resources.Load("Prefabs/overviewDisplay", typeof(GameObject))) as GameObject;
        refHumanDisplay.transform.position = new Vector3(2.8f, 0.01f, -2.95f);





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
        SetKcalRow(sliderValue);

    }

    public float GetSliderValue ()
    {
        return sliderValue;
    }


    private void SetKcalRow (float value = 0)
    {
        TextMesh kcalFloat = growHumanDisplay.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMesh>();
        kcalFloat.text = sliderValue.ToString();
        print("inside SetKcalRow");
    }



    public void RecieveFoodGO(GameObject food)
    {
        // Denna food transform är fel
        currentTarget = food;
        //human.GetComponent<Human>().SetTargetPosition(food.GetComponent<Food>().GetFoodPosition());

        GameObject tempFoodGO = food.GetComponent<Food>().GetFoodGO();
        //print("tempFoodGO: " +tempFoodGO);
        human.GetComponent<Human>().SetTargetPosition(tempFoodGO);


        // Not working
        // Stop coroutine as new target is acquired whilst having an old unreacehd target
        //human.GetComponent<Human>().ScaleHandle(0, false);

    }


    public void ReachedTarget ()
    {
        Destroy(currentTarget);
        //print ("food destroyed");

        human.GetComponent<Human>().ScaleHandle(GetSliderValue(), true);


        human.GetComponent<Human>().scalerValue = GetSliderValue();


    }

}
