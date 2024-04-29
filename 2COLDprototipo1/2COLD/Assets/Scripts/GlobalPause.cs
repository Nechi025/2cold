using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalPause : MonoBehaviour
{
    public static bool isPaused = false;

    public static bool IsPaused()
    {
        return isPaused;
    }

}
