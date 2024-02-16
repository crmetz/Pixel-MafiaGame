using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Variáveis privadas
    bool isPaused = false;

    //Variáveis públicas
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
        
        Debug.Log("A fase atual é" + SceneManager.GetActiveScene().buildIndex);
        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            Debug.Log("Botão clicado!");
            NextlevelPanel.SetActive(false);
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("Botão clicado!");
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
            Debug.Log("Botão clicado!");
            isPaused = false;
            Time.timeScale = 1;
            pausePanel.SetActive(false);
            NextlevelPanel.SetActive(false);
        }
    }


    bool AreAllEnemiesDead()
    {
        // Verificar se todos os GameObjects com tag "Enemy" estão mortos
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            // Verificar se o inimigo ainda está ativo
            if (enemy.activeSelf)
            {
         
                return false; // Pelo menos um inimigo ainda está vivo
                
            }
        }

        
        // Todos os inimigos estão mortos
        return true;
    }



}
