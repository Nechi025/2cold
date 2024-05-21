using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public static GameManager Enemy;
    //public static GameManager Boss;
    //public static GameManager MasterCoins;
    //public static GameManager Trophy;
    //public int trophy;
    public int coins;
    public int coinsLevel2;
    public int coinsLevel3;
    //public int mastercoins;
    public int enemys;
    //public int boss;
    public static GameManager CIO;
    public int cio;



    // Start is called before the first frame update
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        if (Enemy == null)
        {
            Enemy = this;
        }
        else
        {
            Destroy(this);
        }

    }
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {


        if (enemys <= 0)
        {
            //yield return new WaitForSeconds(4);
            //LoadingManager.Instance.LoadScene(4, 5);


            //yield return new WaitForSeconds(4f);
            //CanvasVolver.SetActive(false);
        }



        //if (trophy <= 0)
        //{
        //    LoadingManager.Instance.LoadScene(2, 3);
        //}

        if (coins <= 0)
        {
            // print("gane");
            //SceneManager.LoadScene("TopDown");
            LoadingManager.Instance.LoadScene(2, 3);

        }
        
        if (coinsLevel2 <= 0)
        {
            // print("gane");
            //SceneManager.LoadScene("TopDown");
            LoadingManager.Instance.LoadScene(3, 4);

        }
        
        if (coinsLevel3 <= 0)
        {
            // print("gane");
            //SceneManager.LoadScene("TopDown");
            LoadingManager.Instance.LoadScene(4, 5);

        }

        if (cio <= 0)
        {
            //LoadingManager.Instance.LoadScene(3, 7);
            SceneManager.LoadScene(6);
        }

    }
}
