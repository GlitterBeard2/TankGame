using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public TankSpawner SpawnController;
    [Range(1, 10)]
    
    public HighScores m_HighScores;

    public TextMeshProUGUI m_MessageText;
    public TextMeshProUGUI m_TimerText;

    public GameObject[] m_Tanks;
    private float m_gameTime = 0;
    public float GameTime { get { return m_gameTime; } }

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    };

    private GameState m_GameState;
    public GameState State { get { return m_GameState; } }

    private void Awake()
    {
        //JUST ADDED
       // SpawnController = GameObject.FindObjectOfType<TankSpawner>();


        m_GameState = GameState.Start;

    }

    private void Start()
    {
        for (int i = 0; i< m_Tanks.Length; i++)
        {
            m_Tanks[i].SetActive(false);
        }

        m_TimerText.gameObject.SetActive(false);
        m_MessageText.text = "Get Ready";
    }

    private void Update()
    {
        switch (m_GameState)
        {
            case GameState.Start:
                if (Input.GetKeyUp(KeyCode.Return) == true) 
                {
                    m_TimerText.gameObject.SetActive(true);
                    m_MessageText.text = "";
                    m_GameState = GameState.Playing;
                    for (int i = 0; i < m_Tanks.Length; i++)
                    {
                        m_Tanks[i].SetActive(true);
                    }
                    //JUST ADDED
                    SpawnController.spawnTank();
                }
                break;
            case GameState.Playing:
                bool isGameOver = false;

                m_gameTime += Time.deltaTime;
                int seconds = Mathf.RoundToInt(m_gameTime);
                m_TimerText.text = string.Format("{0:D2}:{1:D2}",(seconds / 60),seconds % 60);

                if (OneTankLeft() == true)
                {
                        //disabled winning by killing all tanks
                    //isGameOver = true;

                }
                if (IsPlayerDead() == true)
                {
                    isGameOver = true;
                }
                if (isGameOver == true)
                {
                    m_GameState = GameState.GameOver;
                    m_TimerText.gameObject.SetActive(false);

                    if (IsPlayerDead() == true)
                    {
                        m_MessageText.text = "TRY AGAIN";
                        //save the score
                        m_HighScores.AddScore(Mathf.RoundToInt(m_gameTime));
                        m_HighScores.SaveScoresToFile();
                    }
                    else
                    {

                        m_MessageText.text = "WINNER";

                    }
                }
                break;
            case GameState.GameOver:
                if (Input.GetKeyUp(KeyCode.Return) == true)
                {
                    m_gameTime = 0;
                    m_GameState = GameState.Playing;
                    m_MessageText.text = "";
                    m_TimerText.gameObject.SetActive(true);

                    for (int i = 0; i < m_Tanks.Length; i++)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        m_Tanks[i].SetActive(true);
                    }
                }
                break;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private bool OneTankLeft()
    {
        int numTanksLeft = 0;
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].activeSelf == true)
            {
                numTanksLeft++;
            }
        }
        return numTanksLeft <= 1;
    }

    private bool IsPlayerDead()
    {
        for (int i = 0; i < m_Tanks.Length;i++)
        {
            if (m_Tanks[i].activeSelf == false)
            {
                if (m_Tanks[i].tag == "Player") 
                    return true;
            }
        }
        return false;
    }


}
