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
    [SerializeField] private GameObject InstructionObject;
    [SerializeField] private AudioSource CombatSound;
    [SerializeField] private GameObject BeforeCombatSound;
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
        if (GameObject.FindObjectOfType<BGMSound>())
            Destroy(GameObject.FindObjectOfType<BGMSound>().gameObject);
        CombatSound.Play();
        theBoss.BossDeath("Player1");
        thePlayer1.isStart = true;
        thePlayer2.isStart = true;
        StartButton.SetActive(false);
    }

    public void RestartGame()
    {
        if (GameObject.FindObjectOfType<BGMSound>())
            Destroy(GameObject.FindObjectOfType<BGMSound>().gameObject);
        Instantiate(BeforeCombatSound, transform.position, Quaternion.identity);
        SceneManager.LoadScene("Main");
    }

    public void CloseInstruction()
    {
        InstructionObject.SetActive(false);
    }

    public void OpenInstruction()
    {
        InstructionObject.SetActive(true);
    }

    public void Exite()
    {
        Application.Quit();
    }
}
