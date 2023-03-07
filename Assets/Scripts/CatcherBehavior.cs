using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherBehavior : MonoBehaviour{

    #region Public Variables
    [Header("Behavior Toggles")]
    [Tooltip("Check this off if it will count score")]
    public bool ScoreKeeper;

    [Tooltip("This is how much score you'll add when you get in the thing")]
    public int ScoreAmountToBeAdded = 1;

    //[Header("SFX")]
    //public AudioSource SoundEffect;

    #endregion

    #region Private Variables
    #endregion

    // Start is called before the first frame update
    void Start(){
        //Double Precaution, if the thing is not a score keeper, then just not have score to be added
        if(!ScoreKeeper)
            ScoreAmountToBeAdded = 0;
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    #region Trigger Functions

    /**
        Trigger collider checker function thingy wowe
    **/
    private void OnTriggerEnter(Collider ObjectCollided) {
        //If the object that touches it is a game token
        if(ObjectCollided.tag == "Game Token"){

            //If the catcher is not a score keeper, then delte the tokebs
            if(!ScoreKeeper)
                //Destroy the object. No More tokeb.
                Object.Destroy(ObjectCollided.gameObject);

            //This gets the token behavior component of the tokens
            var TokenObject = ObjectCollided.GetComponent<TokenBehavior>();

            //If it IS a score keeper, add score, if not, just don't do anything
            //Wow you wanna know what this is?
            //Score keepers are the things that add score to the thing
            //Hi future neko its me, i actually added more to this script
            //Basically now the tokens have a behvior script so they don't immediately poof
            //If the token is not activated, then it should add score
            //Yeah thats it
            //If it's not this should function as a generic delete thingy
            if(ScoreKeeper && !TokenObject.GetHasActivated()){
                AddScoreToArcade();
                TokenObject.SetHasActivated(true);
            }
        }

        //SoundEffect.Play();
    }

    #endregion

    #region Add Score Functions

    private void AddScoreToArcade(){
        //Finds the object that is the score function, which should be the screen.
        ArcadeBehavior ArcadeCabinet = GameObject.Find("Arcade Screen Function").GetComponent<ArcadeBehavior>();
        //Sends a score function, hopefully only once.
        ArcadeCabinet.AddScore(ScoreAmountToBeAdded);
    }

    #endregion

}
