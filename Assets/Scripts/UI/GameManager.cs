using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState //Define the different Game States
    {
        GamePlay,
        Paused
    }

    public GameState currentState; //store the current state of the game 
    public GameState previousState; //store the previous state of the game

    [Header("UI")]
    public GameObject pauseScreen;

    void Awake()
    {
        DisableScreens();
    }

    void Update()
    {
        switch (currentState) //Behaviour for each state
        {
            case GameState.GamePlay:
                PauseCheck();
                break;

            case GameState.Paused:
                PauseCheck();
                break;

            default:
                Debug.LogWarning("Invalid State Does Not Exist");
                break;
        }
    }

    public void ChangeState(GameState newState) //Streamline State Changing
    {
        currentState = newState;
    }

    public void PauseCheck() //Key Inputs to Pause and Resume Game
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame() 
    {
        if(currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f; //Stop the game
            pauseScreen.SetActive(true);
            Debug.Log("Game is Paused");
        }
    }

    public void ResumeGame()
    {
        if(currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f; //Resume the Game
            pauseScreen.SetActive(false);
            Debug.Log("Game is Resumed");
        }
    }

    void DisableScreens()
    {
        pauseScreen.SetActive(false);
    }
}
