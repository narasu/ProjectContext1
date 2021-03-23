using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    public bool pauseActive;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseActive)
            {
                pauseActive = false;
                pauseMenu.SetActive(false);
                return;
            }
            Cursor.lockState = CursorLockMode.None;
            pauseActive = true;
            pauseMenu.SetActive(true);
        }
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
