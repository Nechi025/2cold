using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTablet : MonoBehaviour
{
    
    public int Level;
    public Animator playerAnim;
    private string currentState;
    const string Tutorial = "TutorialLevel";
    const string PilotTutorial1 = "PilotTutorial1";
    const string GameTutorial = "GameTutorial";
    const string NoText = "Downed";


    void Start()
    {
        
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        playerAnim.Play(newState);
        currentState = newState;
    }

    
    void Update()
    {

        if (Level == 0)
        {
            ChangeAnimationState(NoText);
        }
        else if(Level == 1)
        {
            ChangeAnimationState(Tutorial);
        }
        else if (Level == 2)
        {
            ChangeAnimationState(PilotTutorial1);
        }
        else if (Level == 3)
        {
            ChangeAnimationState(GameTutorial);
        }

    }
}
