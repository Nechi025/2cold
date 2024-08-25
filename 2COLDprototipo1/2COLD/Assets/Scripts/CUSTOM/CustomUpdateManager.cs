using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUpdateManager : MonoBehaviour
{
    public List<ManagedUpdateBehavior> updateBehaviors = new List<ManagedUpdateBehavior>();

    // Register a new ManagedUpdateBehavior
    public void Register(ManagedUpdateBehavior behavior)
    {
        if (!updateBehaviors.Contains(behavior))
        {
            updateBehaviors.Add(behavior);
        }
    }

    // Unregister an existing ManagedUpdateBehavior
    public void Unregister(ManagedUpdateBehavior behavior)
    {
        updateBehaviors.Remove(behavior);
    }

    // Call UpdateMe on all registered behaviors
    private void Update()
    {
        foreach (var behavior in updateBehaviors)
        {
            behavior.UpdateMe();
        }
    }
}
