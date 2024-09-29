using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeS : MonoBehaviour
{
    public int unitLifes;
    public int UnitLifes => unitLifes;

    [SerializeField] Animator playerAnim;

    void Start()
    {
        unitLifes = 100;
        GameManager.Instance.cio++;
        playerAnim = GetComponent<Animator>();
    }

    //Vida del player que recibe daño
    public void GetDamage(int value)
    {
        //SoundManager.Instance.PlaySound("Body_Impact");
        playerAnim.SetBool("Damaged", true);
        unitLifes -= value;
        if (unitLifes <= 0)
        {
            SaveCurrentLevel();
            SceneManager.LoadScene(8); // Reemplaza 8 con el índice de la escena de derrota
            GameManager.Instance.cio--;
            Destroy(gameObject);
        }
    }

    public void Death()
    {
        SaveCurrentLevel();
        SceneManager.LoadScene(8); // Reemplaza 8 con el índice de la escena de derrota
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
        playerAnim.SetBool("Damaged", false);
    }
}
