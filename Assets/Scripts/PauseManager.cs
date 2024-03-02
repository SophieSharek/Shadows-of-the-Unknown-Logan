﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
    private static string currentActionMap;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (InputManager.Instance.MenuOpenInput || InputManager.Instance.MenuCloseInput)
        {
            PauseCheck();
        }
    }

    public void PauseCheck()
    {
        if (IsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        // InputManager.PlayerInput.currentActionMap = InputManager.PlayerInput.actions.FindActionMap(currentActionMap);
        InputManager.PlayerInput.actions.FindActionMap("UI").Disable();
        InputManager.PlayerInput.actions.FindActionMap(currentActionMap).Enable();
        InputManager.PlayerInput.currentActionMap = InputManager.PlayerInput.actions.FindActionMap(currentActionMap);
        // InputManager.PlayerInput.SwitchCurrentActionMap(currentActionMap);
        IsPaused = false;
    }

    public void Pause()
    {
        currentActionMap = InputManager.PlayerInput.currentActionMap?.name;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        InputManager.PlayerInput.actions.FindActionMap(currentActionMap).Disable();
        InputManager.PlayerInput.actions.FindActionMap("UI").Enable();
        InputManager.PlayerInput.currentActionMap = InputManager.PlayerInput.actions.FindActionMap("UI");
        InputManager.PlayerInput.SwitchCurrentActionMap("UI");
        IsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        IsPaused = false;

        LevelManager.Instance.LoadScene("MainMenu", "CrossFade");
    }
    public void Quid()
    {
        Application.Quit();
    }
}