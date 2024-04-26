using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject GameWin;
    bool gameIsOver;
    void Start()
    {
        Guard.OnGuardSpot += showGameOverUI;
    }

    
    void Update()
    {
        if (gameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void showGameOverUI()
    {
        OnGameOver(GameOver);
    }

    void showGameWinUI()
    {
        OnGameOver(GameWin);
    }

    void OnGameOver(GameObject gameLoseUI)
    {
        gameLoseUI.SetActive(true);
        gameIsOver = true;
        Guard.OnGuardSpot -= showGameOverUI;
    }
}
