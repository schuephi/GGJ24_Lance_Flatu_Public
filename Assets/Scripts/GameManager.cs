using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Singleton class. Always access via instance, do not use FindObjectOfType!
[RequireComponent(typeof(GameSceneManager))]
public class GameManager : MonoBehaviour
{
    public event Action<Vector2, Vector2> OnLaunchCar = delegate { };
    public event Action OnCarReachedGoal = delegate { };
    public event Action OnFlatuencesStart = delegate { };
    public event Action OnShowCredits = delegate { };
    public event Action OnHideCredits = delegate { };
    public event Action OnShowMenu = delegate { };
    public event Action OnHideMenu = delegate { };
    public event Action OnShowLooseScreen = delegate { };
    public event Action OnHideLooseScreen = delegate { };
    public static GameManager Instance;
    private GameSceneManager gameSceneManager;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            gameSceneManager = GetComponent<GameSceneManager>();
        }
    }

    public void StartNewGame()
    {
        gameSceneManager.LoadScene(GameSceneManager.Scene.IntroScene);
    }

    public void LoadLevel()
    {
        gameSceneManager.LoadScene(GameSceneManager.Scene.StealthLevel2Scene);
    }

    public void PrintCredits()
    {
        OnShowCredits();
    }

    public void HideCredits()
    {
        OnHideCredits();
    }

    public void ShowMenu()
    {
        OnShowMenu();
    }

    public void HideMenu()
    {
        OnHideMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowLooseScreen()
    {
        OnShowLooseScreen();
    }

    public void HideLooseScreen()
    {
        OnHideLooseScreen();
    }

    public void LaunchCar(Vector2 pos, Vector2 direction)
    {
        OnLaunchCar(pos, direction);
    }

    public void ReportCarReachedGoal()
    {
        OnCarReachedGoal();
    }

    public void ReportFirstSequenceCompleted()
    {
        OnFlatuencesStart();
    }

    public void OnGameGoalReached()
    {
        gameSceneManager.LoadScene(GameSceneManager.Scene.WinScene);
    }

    public void LoadStartScene()
    {
        gameSceneManager.LoadScene(GameSceneManager.Scene.StartScene);
    }
}
