using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public GameObject mainMenu, levelSelect;
    public Button[] lvlButtons;

    public GameManager gm;


    void Start()
    {
        MainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            CheckLevels();
        }
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void LevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        CheckLevels();

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        //Application.Quit();
        //Debug.Log("Quit!");
        SceneManager.LoadScene("Main Menu");
    }

    public void ReloadScene()
    {
        // Obtém o nome da cena atual
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Recarrega a cena atual pelo nome
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

   
    void CheckLevels()
    {
        for (int i=1; i<lvlButtons.Length; i++)
        {
            if(PlayerPrefs.HasKey("Level"+ (i + 1).ToString() + "Unlocked"))
            {
                lvlButtons[i].interactable = true;
            }
            else
            {
                lvlButtons[i].interactable = false;

            }

        }
    }
}
