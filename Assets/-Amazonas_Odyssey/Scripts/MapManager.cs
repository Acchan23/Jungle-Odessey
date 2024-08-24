using System;
using System.Collections;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;


public class MapManager : MonoBehaviour
{
    //Variables de Arreglo Bidimensional de Tilemaps 
    [SerializeField] private int dimensionArray;
    private GameObject[,] piecesOfMap;
    private int dimension;
    [SerializeField] private Animator animationFade;
    [SerializeField] private GameObject[] maps;
    //Variables de posición del jugador en el mapa
    public int posI, posJ;
    //Variable para corregir la posición al atravesar una puerta
    public float correction = 10.0f;
    [SerializeField] private Collider2D characterCollider;

    public GameObject edge;

    // Configuración del Cinemachine
    // Start is called before the first frame update

    private void Awake()
    {
        characterCollider = GetComponent<Collider2D>();
        piecesOfMap = new GameObject[dimensionArray, dimensionArray];
        ConstruirMapa();
    }
    void Start()
    {

    }

    //Contacto con cada una de las entradas del mapa
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Right") || other.gameObject.CompareTag("Left") || other.gameObject.CompareTag("Down") || other.gameObject.CompareTag("Up"))
        {
            characterCollider.enabled = false;
            StartCoroutine(TransicionFade(other));
        }
    }


    /***********************************/

    /// <summary>
    /// Método para armar el mapa
    /// </summary>
    private void ConstruirMapa()
    {
        //Arma el mapa en una matriz para poder navergarlo.
        for (int i = 0; i < piecesOfMap.GetLength(0); i++)
        {
            for (int j = 0; j < piecesOfMap.GetLength(1); j++)
            {
                piecesOfMap[i, j] = maps[i * dimensionArray + j];
                Debug.Log("Se ha agregado " + maps[i * dimensionArray + j].name + " en la posición [" + i + "][" + j + "]");
                if (maps[i * dimensionArray + j].activeSelf)
                {
                    posI = i;
                    posJ = j;
                    Debug.Log("Nos encontramos en " + posI + "," + posJ);
                }
            }
        }

    }

    /// <summary>
    ///  Método para cambiar mapa según la dirección de la puerta por donde sale.
    /// </summary>
    /// <param name="other"></param>
    private void CambioMapa(Collider2D other)
    {


        if (other.gameObject.CompareTag("Right"))
        {
            piecesOfMap[posI, posJ].SetActive(false);
            posJ++;
            //Debug.Log("las posición en J es " + posJ);
            transform.position = new Vector2(transform.position.x + correction, transform.position.y);
            edge.transform.position = new Vector2(edge.transform.position.x + 30, edge.transform.position.y);
            piecesOfMap[posI, posJ].SetActive(true);
        }
        else if (other.gameObject.CompareTag("Left"))
        {
            piecesOfMap[posI, posJ].SetActive(false);
            posJ--;
            transform.position = new Vector2(transform.position.x - correction, transform.position.y);
            edge.transform.position = new Vector2(edge.transform.position.x - 30, edge.transform.position.y);
            //Debug.Log("las posición en J es " + posJ);
            piecesOfMap[posI, posJ].SetActive(true);
        }
        else if (other.gameObject.CompareTag("Down"))
        {
            piecesOfMap[posI, posJ].SetActive(false);
            posI++;
            Debug.Log("las posición en I es " + posI);
            transform.position = new Vector2(transform.position.x, transform.position.y - correction);
            edge.transform.position = new Vector2(edge.transform.position.x, edge.transform.position.y - 23);
            piecesOfMap[posI, posJ].SetActive(true);
        }
        else if (other.gameObject.CompareTag("Up"))
        {
            piecesOfMap[posI, posJ].SetActive(false);
            posI--;
            //Debug.Log("las posición en J es " + posJ);
            transform.position = new Vector2(transform.position.x, transform.position.y + correction);
            edge.transform.position = new Vector2(edge.transform.position.x, edge.transform.position.y + 23);
            piecesOfMap[posI, posJ].SetActive(true);
        }
    }

    public IEnumerator TransicionFade(Collider2D other)
    {
        animationFade.PlayInFixedTime("FadeIn");
        Time.timeScale = 0;
        //animationFade.Play("FadeIn"); 
        yield return new WaitForSecondsRealtime(0.3f);
        animationFade.Play("FadeOut");
        Time.timeScale = 1;
        CambioMapa(other);
        characterCollider.enabled = true;

    }

}
