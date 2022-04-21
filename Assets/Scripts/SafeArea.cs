using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    RectTransform PanelRectTransform;
    Rect LastSafeArea = new Rect(0, 0, 0, 0);
    // Start is called before the first frame update
    void Awake()
    {
        PanelRectTransform = GetComponent<RectTransform>();
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    void Refresh()
    {
        Rect safeArea = GetSafeArea();

        if (safeArea != LastSafeArea)
        {
            ApplySafeArea(safeArea);
        }
    }

    Rect GetSafeArea()
    {
        return Screen.safeArea;
    }

    void ApplySafeArea(Rect r)
    {
        LastSafeArea = r;

        // Convert safe area rectangle from absolute pixels to normalised anchor coordinates
        Vector2 safeAnchorMin = r.position;
        Vector2 safeAnchorMax = r.position + r.size;
        safeAnchorMin.x /= Screen.width;
        safeAnchorMin.y /= Screen.height;
        safeAnchorMax.x /= Screen.width;
        safeAnchorMax.y /= Screen.height;
        PanelRectTransform.anchorMin = safeAnchorMin;
        PanelRectTransform.anchorMax = safeAnchorMax;

        Debug.LogFormat("New safe area applied to {0}: x={1}, y={2}, w={3}, h={4} on full extents w={5}, h={6}",
            name, r.x, r.y, r.width, r.height, Screen.width, Screen.height);
    }
}
