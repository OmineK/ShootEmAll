using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("PlayableLevel");
    }


    public void RulesButton()
    {
        SceneManager.LoadScene("Rules");
    }

    public void BackButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
