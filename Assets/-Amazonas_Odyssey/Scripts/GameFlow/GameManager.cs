using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private ObjectPooler objectPooler;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject fire;
    [SerializeField] private Transform rescueSite;
    //private readonly float victorytimer = 60f;

    private void Awake()
    {
        inventory.SetActive(true);
    }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        victoryPanel.SetActive(false);

    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public void Reset()
    {
        SceneManager.LoadScene(0);
    }

    [ContextMenu("Victory")]
    public void Victory()
    {
        Vector3 position = new(-59, -50, 0);
        Instantiate(fire, position, quaternion.identity);
        StartCoroutine(EndGame());
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSecondsRealtime(4f);
        Time.timeScale = 0;
        victoryPanel.SetActive(true);
    }
}
