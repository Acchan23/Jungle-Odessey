using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip putOnMouseSound;
    private AudioSource fuenteAudio;

    void Start()
    {
        fuenteAudio = GetComponent<AudioSource>(); //Toma los sonidos agsinados al boton
    }
    public void OnButtonClick()
    {
        AudioManager2.Instance.PlaySFX(clickSound); //Reproduce el sonido al hacer click
    }

    public void PutOnButton()
    {
        AudioManager2.Instance.PlaySFX(putOnMouseSound); //Reproduce el sonido al poner sobre el boton el mouse
    }
    
}
