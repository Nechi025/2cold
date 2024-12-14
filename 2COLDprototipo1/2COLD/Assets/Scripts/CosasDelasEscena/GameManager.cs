using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string defeatScreen;
    

    public static GameManager Instance;
    public static GameManager Enemy;
    
    public int enemys;
   
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


        

        if (cio <= 0)
        {
            //LoadingManager.Instance.LoadScene(3, 7);
            StartCoroutine(GotoLevel(defeatScreen));
        }

    }

    IEnumerator GotoLevel(string scene)
    {
      

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
