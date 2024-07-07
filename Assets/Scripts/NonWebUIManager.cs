using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonWebUIManager : MonoBehaviour
{
    [SerializeField] GameObject[] mobileUI;
    [SerializeField] bool showInEditor = true;

    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer || (Application.isEditor && !showInEditor))
        {
            foreach (GameObject ui in mobileUI)
            {
                ui.SetActive(false);
            }
        }
    }
}