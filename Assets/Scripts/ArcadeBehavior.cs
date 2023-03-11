using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcadeBehavior : MonoBehaviour{

    #region Public Variables
    [Header("Cabinet Variables")]
    [Tooltip("This is the max amount of balls the player has")]
    public int MaxTokens = 50;

    [Tooltip("The Dropper Object")]
    public DropperBehavior Dropper;

    [Tooltip("The max amount of seconds to play the game")]
    public float MaxTime = 30f;

    [Header("UI Elements")]
    [Tooltip("The text that will show the score and stuff")]
    public Text ScoreAndTokenText;
    [Tooltip("The timer that will show the time")]
    public Image TimerImage;
    public Text TimerText;


    #endregion

    #region Private Variables
    [Header("Private Variables")]
    [SerializeField]
    [Tooltip("This is the score for the arcade machine")]
    private int Score = 0;
    
    [SerializeField]
    [Tooltip("This is if the game is active or not")]
    private bool CabinetIsActive = true;

    [SerializeField]
    [Tooltip("Current amount of tokens left")]
    //The token count that is currently being used
    private int TokenCount;

    [SerializeField]
    private float CurrentTimer;

    //Craetes an enum for the states of the cabinet
    private enum AracdeState {Idle, Playing, Ending};
    
    //Creates something so we can keep track of the cabinet states
    private AracdeState CurrentArcadeState = AracdeState.Idle;

    //Prevents state changing coroutines to run multiple times
    private bool ChangingStates = false;

    #endregion

    // Start is called before the first frame update
    void Start(){
        //Sets the Token count to the max tokens
        TokenCount = MaxTokens;

        //Creates a highscore variable or just finds one
        if(PlayerPrefs.GetInt("HighScore") <= 0)
            PlayerPrefs.SetInt("HighScore", 690);
        
        //Sets the current timer to the max timer count specified by user
        CurrentTimer = MaxTime;
    }

    // Update is called once per frame
    void Update(){
        CabinetStates();
        DropperStates();
        UpdateTimer();
    }

    #region Cabinet States

    /**
        This controls the cabinet states, so that way the cabinet can like
        do things like stop plaiyng and more
    **/
    private void CabinetStates(){
        //Has a switch case so you can determine the arcade states
        switch(CurrentArcadeState){

            //This is when the game is gonna start it'll say "press spcae to start!"            
            case AracdeState.Idle:
                //Sets the score to 0
                Score = 0;

                //Sets the cabinet to not active
                CabinetIsActive = false;
                //Disables dropper
                Dropper.SetDropperIsActive(false);

                //Prompts user to play
                ScoreAndTokenText.text = "Press Space to Start!";

                //Sets the amount of tokens back to the max
                TokenCount = MaxTokens;

                //Sets the current time to the max time
                CurrentTimer = MaxTime;

                //If space is pressed, then change the mode to playing
                if(Input.GetButtonDown("Use"))
                    CurrentArcadeState = AracdeState.Playing;

            break;

            //Set cabinet to active if and only if tokens are not 0
            //Set the game to be playing
            case AracdeState.Playing:
                //If there are tokens, then set it to true
                if(TokenCount > 0 && CurrentTimer > 0)
                    CabinetIsActive = true;
            
                //Shows the game text
                ShowGameplayText();

                //Runs the timer
                CountDownTimer();

                //If the timer runs out disable the dropper.
                if(CurrentTimer <= 0){
                    CabinetIsActive = false;
                }

                //If the token count is less than 0, then start the coroutine to delay
                //This means that there will be a delay to swap to the next stage
                if(TokenCount <= 0 || CurrentTimer <= 0)
                    StartCoroutine(SwapStatesWithDelay(3f, AracdeState.Ending));

            break;

            //Show just score and thats it
            case AracdeState.Ending:
                //Sets the cabinet to not active
                CabinetIsActive = false;
                //Disables dropper
                Dropper.SetDropperIsActive(false);

                //Checks for highscore and adjusts accordingly
                if(PlayerPrefs.GetInt("HighScore") < Score)
                    PlayerPrefs.SetInt("HighScore", Score);

                //Displays the text
                ScoreAndTokenText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString() +
                                        "\nScore: " + Score;

                StartCoroutine(SwapStatesWithDelay(5f, AracdeState.Idle));
            break;
        }
    }

    #region Coroutines

    /**
        Changes the states with a delay, meant for playing to another
    **/
    private IEnumerator SwapStatesWithDelay(float Time, AracdeState StateToChangeTo){
        //If it IS changing states, shut the fuck up don't do multiple
        if(ChangingStates)
            yield break;
        
        //Lets it know it is changing states
        ChangingStates = true;
        

        //This is the thing that does the delay idk what it means tho
        yield return new WaitForSeconds(Time);

        //Changes the states
        CurrentArcadeState = StateToChangeTo;

        //Makes it so it is no longer changing states
        ChangingStates = false;

    }

    #endregion

    #region Timer Functions
    
    /**
        This counts down the timer to 0, this is for the game, it's like the
        coroutines but not really
    **/
    private void CountDownTimer(){
        //If the time is bigger than 0, than tick down the time.
        if(CurrentTimer > 0)
            CurrentTimer -= Time.deltaTime;
    }
    
    #endregion

    #endregion

    #region Token functions

    /**
        When use is pressed, decrement the amount of tokens.
    **/
    private void DecrementTokens(){
        //When a person presses use it will drop
        if(Input.GetButtonDown("Use"))
            TokenCount--;
    }

    private void DropperStates(){
        //Decrement the tokens if the game is running
        if(CabinetIsActive){
            DecrementTokens();
            Dropper.SetDropperIsActive(true);
        }

        //If the cabiner isn't active, turn it off.
        if(!CabinetIsActive){
            Dropper.SetDropperIsActive(false);
        }

        if(TokenCount <= 0){
            CabinetIsActive = false;
        }
    }

    #endregion

    #region Text Update

    /**
        Updates the text of the machine so it shows how cool you are.
    **/
    private void ShowGameplayText(){
        ScoreAndTokenText.text = "Score: " + Score.ToString() + 
                                "\nTokens Left: " + TokenCount.ToString();
    }

    private void UpdateTimer(){
        TimerImage.fillAmount = CurrentTimer / MaxTime;

        TimerText.text = Mathf.RoundToInt(CurrentTimer).ToString();
    }

    #endregion


    #region Public Functions

    /**
        Adds a certain amount of score to the score
    **/
    public void AddScore(int ScoreAmount){
        Score += ScoreAmount;
    }

    #endregion

    #region Getters

    public int GetScore(){
        return Score;
    }

    public int GetTokenCount(){
        return TokenCount;
    }

    public bool GetCabinetIsActive(){
        return CabinetIsActive;
    }

    #endregion

}

