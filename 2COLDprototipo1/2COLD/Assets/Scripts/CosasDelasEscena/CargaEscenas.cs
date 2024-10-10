using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargaEscenas : MonoBehaviour
{
    
    public string sceneLoad;
    public Animator animator;
    private string currentTransition;
    const string EnterScene = "OpenScene";
    const string ExitScene = "CloseScene";

    void ChangeAnimationState(string newTransition)
    {
        if (currentTransition == newTransition) return;
        animator.Play(newTransition);
        currentTransition = newTransition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeAnimationState(ExitScene);
            StartCoroutine(WaitForAnimationAndLoadScene());
        }
    }

    

    private IEnumerator WaitForAnimationAndLoadScene()
    {
       
        AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(animStateInfo.length);

        
        SceneManager.LoadScene(sceneLoad);
    }
}