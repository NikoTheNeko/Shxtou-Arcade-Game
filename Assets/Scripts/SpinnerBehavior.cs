using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerBehavior : MonoBehaviour{

    #region Public Variables
    [Header("Game Object Variables")]
    [Tooltip("This is the rigidbody plate object that will spin")]
    public Rigidbody Plate;

    [Tooltip("This is the transform of the object you wish to spin")]
    public Transform PlateTransform;


    [Header("Speed Variables")]
    [Tooltip("This adjusts the speed at which the plate rotates")]
    [Range(0.0f, 500f)]
    public float PlateSpinSpeed = 0.5f;

    #endregion

    #region Private Variables


    #endregion

    // Start is called before the first frame update
    void Start(){
        //Locks the plate in it's position
        AnchorThePlate();
    }

    // Update is called once per frame
    void Update(){
        //I know you *shouldn't* update it every frame, but i wanna spin faster brrr
        //SpinPlate();
        //Locks the plate in it's position
        AnchorThePlate();

        //Spins the plate hopefully
        SpinPlateTransform();

    }

    #region Spin funcitons

    /**
        I need a new spin plate function that works with transform
    **/
    private void SpinPlateTransform(){
        //Making a new vector that holds the Z angle of the plate
        Vector3 RotationVector =  PlateTransform.eulerAngles;
        //We're going to be adding more speed to the plate by time
        RotationVector.z -= PlateSpinSpeed * Time.deltaTime;

        //We assign the rotation vector made to the current
        PlateTransform.eulerAngles = RotationVector;
    }

    /**
        This spins the plate acording to the speed
    **/
    private void SpinPlate(){
        //Creates a direction to spin in with the speed given
        //It's like vector math you remember this right neko
        Vector3 SpinVelocityVector = new Vector3(PlateSpinSpeed, 0, 0);
        //Sets the the velocity to the spin vector from above
        Plate.angularVelocity = SpinVelocityVector * Time.deltaTime;
    }

    #endregion

    #region Anchor Functions

    /**
        This keeps the plate from spinning away forever please stop moving im begging
        It grabs the initial start point of the object and constantly sets it to that,
        please stop moving
    **/
    private void AnchorThePlate(){
        Plate.centerOfMass = Vector3.zero;
        Plate.inertiaTensorRotation = Quaternion.identity;
    }

    #endregion

}
