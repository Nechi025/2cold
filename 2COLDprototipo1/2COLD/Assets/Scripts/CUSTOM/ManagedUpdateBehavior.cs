using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagedUpdateBehavior : MonoBehaviour
{
    private CustomUpdateManager updateManager;

    protected virtual void Start()
    {
        updateManager = FindObjectOfType<CustomUpdateManager>();
        if (updateManager != null)
        {
            updateManager.Register(this);
        }
    }

    protected virtual void OnDestroy()
    {
        if (updateManager != null)
        {
            updateManager.Unregister(this);
        }
    }

    public virtual void UpdateMe()
    {
        
    }
}

