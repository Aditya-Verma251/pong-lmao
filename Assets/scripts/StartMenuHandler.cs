using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    public void PlayButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitButton()
    {
        if (Application.isPlaying)
        {
            Application.Quit();
        }
    }
}
