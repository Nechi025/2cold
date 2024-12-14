using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class LifeS : MonoBehaviour
{
    public int unitLifes;
    public int UnitLifes => unitLifes;

    [SerializeField] Animator playerAnim;
    private string currentState;
    const string BaseScreen = "BaseScreen";
    const string DamagedScreen = "DamagedScreen";
    public event Action<int> OnLifeChanged;


    void Start()
    {
        unitLifes = 100;
        GameManager.Instance.cio++;
        playerAnim = GetComponent<Animator>();
    }

    //Vida del player que recibe daño
    public void GetDamage(int value)
    {
        
        ChangeAnimationState(DamagedScreen);
        unitLifes -= value;
        OnLifeChanged?.Invoke(unitLifes);
        if (unitLifes <= 0)
        {
            SaveCurrentLevel();
            SceneManager.LoadScene(17); 
            GameManager.Instance.cio--;
            Destroy(gameObject);
        }
    }

    void ChangeAnimationState(string newState)
    {
        playerAnim.Play(newState);
        currentState = newState;
    }

    public void Death()
    {
        SaveCurrentLevel();
        SceneManager.LoadScene(8); 
        GameManager.Instance.cio--;
        Destroy(gameObject);
    }

    public void GetHealth(int value)
    {
        unitLifes += value;
    }

    private void SaveCurrentLevel()
    {
        // Guarda el nombre del nivel actual antes de cargar la escena de derrota
        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastLevel", currentSceneName);
    }

    private IEnumerator ResetDamagedAnimation()
    {
        yield return new WaitForSeconds(1.5f);
        ChangeAnimationState(BaseScreen);
    }
}
