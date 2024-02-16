using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Vari�veis privadas
    bool isPaused = false;

    //Vari�veis p�blicas
    public static GameManager gm;
    public GameObject pausePanel;
    public GameObject NextlevelPanel;


    // Start is called before the first frame update
    void Start()
    {
        gm = this;
        pausePanel.SetActive(false);
        NextlevelPanel.SetActive(false);
        Time.timeScale = 1;
        
    }



    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausegame();
        }

        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            LoadnextLevel();
        }

        // Verificar se todos os inimigos foram derrotados
        if (AreAllEnemiesDead())
        {
            // Ativar o painel e pausar o tempo
            NextlevelPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void LoadnextLevel()
    {
        
        Debug.Log("A fase atual �" + SceneManager.GetActiveScene().buildIndex);
        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            Debug.Log("Bot�o clicado!");
            NextlevelPanel.SetActive(false);
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("Bot�o clicado!");
            NextlevelPanel.SetActive(false);
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        PlayerPrefs.SetInt("Level" + (SceneManager.GetActiveScene().buildIndex + 1).ToString() + "Unlocked", 1);

    }


    public void Pausegame()
    {
        if (isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }

        else
        {
            Debug.Log("Bot�o clicado!");
            isPaused = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            NextlevelPanel.SetActive(false);
        }
    }


    bool AreAllEnemiesDead()
    {
        // Verificar se todos os GameObjects com tag "Enemy" est�o mortos
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            // Verificar se o inimigo ainda est� ativo
            if (enemy.activeSelf)
            {
         
                return false; // Pelo menos um inimigo ainda est� vivo
                
            }
        }

        
        // Todos os inimigos est�o mortos
        return true;
    }



}
