using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private RectTransform crosshairRectTransform;

    void Start()
    {
        Cursor.visible = false;

        crosshairRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        
        Vector2 mousePosition = Input.mousePosition;

        
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                crosshairRectTransform.parent as RectTransform,
                mousePosition,
                null,
                out Vector2 localPoint))
        {
            crosshairRectTransform.localPosition = localPoint;
        }
    }
}

