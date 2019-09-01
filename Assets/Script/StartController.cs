using UnityEngine.SceneManagement;
using UnityEngine;

public class StartController : MonoBehaviour
{
    public void StartAGame()
    {
        SceneManager.LoadScene("Main");
    }
}
