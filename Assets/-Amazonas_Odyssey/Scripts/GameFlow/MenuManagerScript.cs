using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{   
    //Carga las escenas del jeugo
    public void EscenaJuego(){
        SceneManager.LoadScene("Intro");
    }

    public void StartScene()
    {
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
