using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperBehavior : MonoBehaviour{

    #region Public Variables
    [Header("Drop Properties")]
    public float Speed = 1f;

    [Header("Game Objects")]
    [Tooltip("This is the object where it's going to spawn")]
    public Transform DropLocation;

    [Tooltip("This is the object it will drop")]
    public GameObject DroppedObject;

    [Tooltip("Allows it to work I think")]
    public bool DropperIsActive = true;

    [Header("SFX")]
    public AudioSource SoundEffect;


    #endregion

    #region Private Variables
    //Lol idk how quaternions work
    private Quaternion Rotato = Quaternion.Euler(90,0,0);
    #endregion

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        //Checks if the use button has been pressed, then will spawn the object
        if(Input.GetButtonDown("Use") && DropperIsActive)
            DropToken();
    }

    #region Object Droppin Functions

    /**
        Drops the token at the specified place
    **/
    private void DropToken(){
        SoundEffect.Play();
        //Spawns the object at the location of the block, or place it's thrown at i guess
        var TokenObject = Object.Instantiate(DroppedObject, DropLocation.position, Rotato);

        //Sets the velocity of the token when it spawns
        //Sets up the velocity vector
        Vector3 TokenSpeedVector = new Vector3(0,0, Speed);
        //Getting the velocity component and changing its vector to the speed vector
        TokenObject.GetComponent<Rigidbody>().velocity = TokenSpeedVector;
    }

    #endregion

    #region Setters

    public void SetDropperIsActive(bool ActiveVariable){
        DropperIsActive = ActiveVariable;
    }

    #endregion
}
