using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI bulletText;
    [SerializeField] TextMeshProUGUI pointText;
    
    public TextMeshProUGUI shiledText;
    public GameObject gameOverCanvas;
    public GameObject pauseCanvas;

    int currentPoints;

    void Awake()
    {
        PointTextUpdate(currentPoints);
        gameOverCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
    }

    public void PointTextUpdate(int addAmount)
    {
        currentPoints += addAmount;

        pointText.text = "Points: " + currentPoints;
    }

    public void LifeTextUpdate(int currentLifes)
    {
        lifeText.text = "Lifes: " + currentLifes;
    }

    public void BulletTextUpdate(int maxBulletAmount)
    {
        bulletText.text = "Bullets: " + maxBulletAmount;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void GoBackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoBackToPlay()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseCanvas.SetActive(false);
    }
}
