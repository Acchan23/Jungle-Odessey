using System;
using System.Collections;
using UnityEngine;


public class MapManager : MonoBehaviour
{
    //Variables de Arreglo Bidimensional de Tilemaps 
    private GameObject[,] piecesOfMap = new GameObject [2,2];
    [SerializeField] private Animator animationFade; 
    [SerializeField] private GameObject[] maps;
    //Variables de posición del jugador en el mapa
    public int posI, posJ;  
    //Variable para corregir la posición al atravesar una puerta
    public float correction = 0.5f; 
    // Start is called before the first frame update
    void Start()
    {
        ConstruirMapa(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Contacto con cada una de las entradas del mapa
    private void OnTriggerEnter2D(Collider2D other)
    {   
        CambioMapa(other);
    }

    /*private void OnTriggerExit2D(Collider2D other) 
    {   
        animationFade.StopPlayback(); 
        animationFade.Play("FadeOut");  
    }*/

/***********************************/

    //Método para armar el mapa
    private void ConstruirMapa()
    {
        //Arma el mapa en una matriz para poder navergarlo.
        for (int i = 0; i < piecesOfMap.GetLength(0); i++)
        {
            for (int j = 0; j < piecesOfMap.GetLength(1); j++)
            {
                piecesOfMap[i,j] = maps[i*2+j]; 
                //Debug.Log("Se ha agregado " + maps[i*2+j].name + " en la posición [" + i + "][" + j + "]");
                if(maps[i*2+j].activeSelf)
                {
                    posI = i;
                    posJ = j; 
                    Debug.Log("Nos encontramos en " + posI + "," + posJ);
                }
            }
        }

    }

    //Método para cambiar mapa según la dirección de la puerta por donde sale.
    private void CambioMapa(Collider2D other)
    {
        

        if(other.gameObject.CompareTag("Right"))
        {   
            StartCoroutine(TransicionFade());
            piecesOfMap[posI,posJ].SetActive(false);
            posJ ++;
            //Debug.Log("las posición en J es " + posJ);
            transform.position = new Vector2(-transform.position.x + correction,transform.position.y);
            piecesOfMap[posI,posJ].SetActive(true);
             
            
        }
        else if(other.gameObject.CompareTag("Left"))
        {
            piecesOfMap[posI,posJ].SetActive(false);
            posJ --;
            transform.position = new Vector2(-transform.position.x - correction,transform.position.y);
            //Debug.Log("las posición en J es " + posJ);
            piecesOfMap[posI,posJ].SetActive(true); 
        }
        else if(other.gameObject.CompareTag("Down"))
        {
            piecesOfMap[posI,posJ].SetActive(false);
            posI ++;
            //Debug.Log("las posición en J es " + posJ);
            transform.position = new Vector2(transform.position.x,-transform.position.y - correction);
            piecesOfMap[posI,posJ].SetActive(true); 
        }
        else if(other.gameObject.CompareTag("Up"))
        {
            piecesOfMap[posI,posJ].SetActive(false);
            posI --;
            //Debug.Log("las posición en J es " + posJ);
            transform.position = new Vector2(transform.position.x,-transform.position.y + correction);
            piecesOfMap[posI,posJ].SetActive(true); 
        }
    }

    public IEnumerator TransicionFade()
    {
            Time.timeScale = 0;
            animationFade.Play("FadeIn"); 
            animationFade.Play("FadeOut");
            Time.timeScale = 1; 
            yield return new WaitForSeconds(5.0f);
    }

}
