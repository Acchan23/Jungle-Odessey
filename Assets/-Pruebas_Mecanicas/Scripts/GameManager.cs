using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private ObjectPooler objectPooler;
    [SerializeField] private GameObject victoryPanel;
    private readonly float victorytimer = 60f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        victoryPanel.SetActive(false);
        StartCoroutine(SpawnBonfire());
    }

    private IEnumerator SpawnBonfire()
    {
        yield return new WaitForSeconds(victorytimer);
        objectPooler.SpawnFromPool("Fire", transform.position, transform.rotation);
    }

    public void GameOver() => SceneManager.LoadScene(0);

    public void Victory()
    {
        Time.timeScale = 0;
        victoryPanel.SetActive(true);
    }

}
