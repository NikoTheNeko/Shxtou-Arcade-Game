using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenBehavior : MonoBehaviour{

    #region Public Variables
        [Header("This is going to check if it's touched a catcher")]
        public bool HasActivated = false;
    #endregion

    #region Private Variables
    #endregion

    //Returns HasActivated
    public bool GetHasActivated(){
        return HasActivated;
    }

    //This sets the hasactivated to a certain value
    public void SetHasActivated(bool OnOrOff){
        HasActivated = OnOrOff;
    }
}
