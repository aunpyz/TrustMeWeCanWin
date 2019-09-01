using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private PlayerController thePlayer1;
    [SerializeField] private PlayerController thePlayer2;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject RestartButton;
    private bool isWaitForRespawn;
    public bool isVictory;
    [SerializeField] private BossController theBoss;

    void Start()
    {

    }

    void Update()
    {
        if (!isVictory)
            if (!thePlayer1.isStart && !thePlayer2.isStart)
            {
                if (Input.anyKey)
                    if (Input.GetButtonDown("Start"))
                    {
                        StartGame();
                    }
            }

        if (!isVictory)
            if (thePlayer1.isDeath && thePlayer2.isDeath && !isWaitForRespawn)
            {
                isWaitForRespawn = true;
                RestartButton.SetActive(true);
            }
    }

    public void StartGame()
    {
        theBoss.BossDeath("Player1");
        thePlayer1.isStart = true;
        thePlayer2.isStart = true;
        StartButton.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
