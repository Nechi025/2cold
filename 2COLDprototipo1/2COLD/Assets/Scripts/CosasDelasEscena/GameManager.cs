using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string defeatScreen;
    public string winScreen;
    public string LevelTutorial;
    public string Level2;
    public string Level3;
    public string Level4;

    public static GameManager Instance;
    public static GameManager Enemy;
    //public static GameManager Boss;
    //public static GameManager MasterCoins;
    //public static GameManager Trophy;
    //public int trophy;
    public int coins;
    public int coinsLevel2;
    public int coinsLevel3;
    public int coinsLevel4;
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
            StartCoroutine(GotoLevel(Level2));

        }

        if (coinsLevel2 <= 0)
        {
            // print("gane");
            //SceneManager.LoadScene("TopDown");
            StartCoroutine(GotoLevel(Level3));

        }

        if (coinsLevel3 <= 0)
        {
            // print("gane");
            //SceneManager.LoadScene("TopDown");
            StartCoroutine(GotoLevel(Level4));

        }

        if (coinsLevel4 <= 0)
        {
            // print("gane");
            //SceneManager.LoadScene("TopDown");
            StartCoroutine(GotoLevel(winScreen));

        }

        if (cio <= 0)
        {
            //LoadingManager.Instance.LoadScene(3, 7);
            StartCoroutine(GotoLevel(defeatScreen));
        }

    }

    IEnumerator GotoLevel(string scene)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
