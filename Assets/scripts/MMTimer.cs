//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMTimer : MonoBehaviour
{
    float timer = 1f;
    bool cont = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 1;   
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        if (timer < 0f) 
        { 
            if (cont)
            {
                SceneManager.LoadScene("StartMenu");
            }

            if (Input.anyKeyDown || Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            {
                cont = true;
            }
        }
        else
        {
            timer -= delta;
        }
    }
}
