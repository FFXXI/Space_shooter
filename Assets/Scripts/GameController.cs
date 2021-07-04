using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    private int score;

    //public Text restartText;
    public Text gameOverText;

    private bool gameover;
    private bool restart;

    public GameObject menuButton;
    public GameObject restartButton;

    
    //public void Update()
    //{
    //  if (restart)
    //{
    // if (Input.GetKeyDown(KeyCode.R))
    // {
    //    SceneManager.LoadScene("main", LoadSceneMode.Single);
    // }
    // }
    // }


    private void Start()
    {
        gameover = false;
        restart = false;

        restartButton.SetActive(false);
        menuButton.SetActive(false);
        //restartText.text = "";
        gameOverText.text = "";


        score = 0;
        UpdateScore();

        StartCoroutine(SpawnWawes());

    }

    IEnumerator SpawnWawes()
    {

        yield return new WaitForSeconds(startWait);

        while (true)
        {

            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;

                GameObject hazard = hazards[Random.Range(0, hazards.Length)];


                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(Random.Range(0.5f, spawnWait));

            }
            yield return new WaitForSeconds(waveWait);

            if (gameover)
            {
                restartButton.SetActive(true);
                menuButton.SetActive(true);
                //restartText.text = "Press 'R' for restart";
                restart = true;
                break;
            }
        }

    }
    void UpdateScore()
    {
        scoreText.text = "Счет: " + score;

    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();

    }

    public void GameOver()
    {
        gameOverText.text = "GameOver";
        gameover = true;


        restartButton.SetActive(true);
        menuButton.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }

    public void MenuGO()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }
}
