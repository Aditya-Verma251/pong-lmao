using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public bool isPaused = false;
    public playercontrol player;
    public ballscript ball;
    public enemycontrol enemy;
    public GameObject canvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            { 
                isPaused = true;
                canvas.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                isPaused = false; 
                Time.timeScale = 1f;
                canvas.SetActive(false);
            }
        }
    }

    public void Resume()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
            canvas.SetActive(false);
        }
    }

    public void Reset() 
    {
        player.Reset();
        ball.Reset();
        enemy.Reset();
        Resume();
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
}
