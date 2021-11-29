using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject HowToPlay;
    // Start is called before the first frame update
    void Start()
    {
        MainMenu.SetActive(true);
        HowToPlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainMenuButton()
    {
        MainMenu.SetActive(true);
        HowToPlay.SetActive(false);
    }
    public void HowToButton()
    {
        MainMenu.SetActive(false);
        HowToPlay.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Game()
    {
        SceneManager.LoadScene("Game");
    }

}
