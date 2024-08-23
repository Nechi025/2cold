using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;  // Prefab de la bala
    public int poolSize = 10;  // Tama�o inicial del pool

    private Queue<GameObject> pool;

    void Awake()
    {
        pool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    // M�todo para obtener un objeto del pool
    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // Si el pool est� vac�o, instanciamos un nuevo objeto
            GameObject obj = Instantiate(prefab);
            return obj;
        }
    }

    // M�todo para devolver un objeto al pool
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
