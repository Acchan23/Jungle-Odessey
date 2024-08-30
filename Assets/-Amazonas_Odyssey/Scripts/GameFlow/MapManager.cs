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

    // Start is called before the first frame update

    private void Awake()
    {
        characterCollider = GetComponent<Collider2D>();
        //Crea el arreglo bidimensional donde se guardaran cada uno de los fragmentos del mapa
        piecesOfMap = new GameObject[dimensionArray, dimensionArray];
    }

    private void Start()
    {
        ConstruirMapa();
        animationFade.Play("FadeOut");
    }

    //Contacto con cada una de las entradas del mapa
    private void OnTriggerEnter2D(Collider2D other)
    {   
        //Activa el método solo cuando entra en contacto con alguna de las salidas
        if (other.gameObject.CompareTag("Right") || other.gameObject.CompareTag("Left") || other.gameObject.CompareTag("Down") || other.gameObject.CompareTag("Up"))
        {   
            //Desactiva el collider del personaje para que no lo reconozca más de una vez
            characterCollider.enabled = false;
            //Inicia la corutina para hacer el cambio del mapa con fade a negro
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
                //Inicializa las variables en la posición del primer mapa activado
                if (maps[i * dimensionArray + j].activeSelf)
                {
                    posI = i;
                    posJ = j;
  
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

        if (other.gameObject.CompareTag("Right")) // Sale por una puerta marcada cómo "Right"
        {
            piecesOfMap[posI, posJ].SetActive(false); //Desactiva el mapa
            posJ++; //Mueve el apuntador para el arreglo bidimensional
            
            transform.position = new Vector2(transform.position.x + correction, transform.position.y); //Reubica al personaje en la siguiente sala
            edge.transform.position = new Vector2(edge.transform.position.x + 30, edge.transform.position.y); //Reubica el borde máximo al que se puede mover la cámara del Cinemachine
            piecesOfMap[posI, posJ].SetActive(true); //Activa la nueva porción del mapa
        }
        else if (other.gameObject.CompareTag("Left")) // Sale por una puerta marcada cómo "Left"
        {
            piecesOfMap[posI, posJ].SetActive(false);//Desactiva el mapa
            posJ--;//Mueve el apuntador para el arreglo bidimensional
            transform.position = new Vector2(transform.position.x - correction, transform.position.y); //Reubica al personaje en la siguiente sala
            edge.transform.position = new Vector2(edge.transform.position.x - 30, edge.transform.position.y); //Reubica el borde máximo al que se puede mover la cámara del Cinemachine
            
            piecesOfMap[posI, posJ].SetActive(true); //Activa la nueva porción del mapa
        }
        else if (other.gameObject.CompareTag("Down")) // Sale por una puerta marcada cómo "Down"
        {
            piecesOfMap[posI, posJ].SetActive(false);//Desactiva el mapa
            posI++;//Mueve el apuntador para el arreglo bidimensional
            
            transform.position = new Vector2(transform.position.x, transform.position.y - correction);//Reubica al personaje en la siguiente sala
            edge.transform.position = new Vector2(edge.transform.position.x, edge.transform.position.y - 23); //Reubica el borde máximo al que se puede mover la cámara del Cinemachine
            piecesOfMap[posI, posJ].SetActive(true); //Activa la nueva porción del mapa
        }
        else if (other.gameObject.CompareTag("Up")) // Sale por una puerta marcada cómo "Up"
        {
            piecesOfMap[posI, posJ].SetActive(false);//Desactiva el mapa
            posI--;//Mueve el apuntador para el arreglo bidimensional
            
            transform.position = new Vector2(transform.position.x, transform.position.y + correction); //Reubica al personaje en la siguiente sala
            edge.transform.position = new Vector2(edge.transform.position.x, edge.transform.position.y + 23); //Reubica el borde máximo al que se puede mover la cámara del Cinemachine
            piecesOfMap[posI, posJ].SetActive(true); //Activa la nueva porción del mapa
        }
    }

    /// <summary>
    /// Corutina configurada para cambiar el mapa
    /// </summary>
    public IEnumerator TransicionFade(Collider2D other)
    {
        animationFade.PlayInFixedTime("FadeIn"); //Activa la transición a negro
        Time.timeScale = 0; //Para el juego mientras cambia el mapa
        yield return new WaitForSecondsRealtime(0.3f); // Espera el tiempo de transición
        animationFade.Play("FadeOut"); //Activa la transición que sal de negro
        Time.timeScale = 1; // Reanuda el juego
        CambioMapa(other); // Activa el cambio de mapa
        characterCollider.enabled = true; // Reactiva el collider del personaje

    }

}
