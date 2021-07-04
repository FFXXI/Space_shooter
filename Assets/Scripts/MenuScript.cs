using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject startButton;
    public GameObject setingButton;


    public void StartGame()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }
}
