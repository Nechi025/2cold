using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTablet : MonoBehaviour
{
    // Start is called before the first frame update
    public int Level;
    public Animator playerAnim;
    private string currentState;
    const string Tutorial1 = "Level1";
    const string Tutorial2 = "Level2";
    const string Tutorial3 = "Level3";
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

    // Update is called once per frame
    void Update()
    {

        if (Level == 0)
        {
            ChangeAnimationState(NoText);
        }
        else if(Level == 1)
        {
            ChangeAnimationState(Tutorial1);
        }
        else if (Level == 2)
        {
            ChangeAnimationState(Tutorial2);
        }
        else if (Level == 3)
        {
            ChangeAnimationState(Tutorial3);
        }

    }
}
