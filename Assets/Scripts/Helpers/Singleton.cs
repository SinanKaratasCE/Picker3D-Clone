using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Main;

    #region Singletons

    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public UIManager uiManager;
    [HideInInspector] public SaveManager saveManager;

    #endregion

    private void Awake()
    {
        if (Main == null)
        {
            Main = this;
            DontDestroyOnLoad(gameObject);
            GetSingletons();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GetSingletons()
    {
        gameManager = GetComponentInChildren<GameManager>();
        uiManager = GetComponentInChildren<UIManager>();
        saveManager = GetComponentInChildren<SaveManager>();
    }
}