using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
   [SerializeField] AudioMixer mixer;
   [SerializeField] Slider musicSlider;
   [SerializeField] Slider sfxSlider; 

   public const string MIXER_MUSIC = "MusicVolume";
   public const string MIXER_SFX = "SFXVolume";

   private void Awake() 
   {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
   }

   private void Start() 
   {
        //Valores predeterminados del audio
        musicSlider.value = PlayerPrefs.GetFloat(AudioManager2.MUSIC_KEY, 0.7F); 
        sfxSlider.value = PlayerPrefs.GetFloat(AudioManager2.SFX_KEY, 0.7F); 
   }

   private void OnDisable() 
   {      
        //Agrega el valor guardado por preferencia del jugador al volumen que se va a manejar  
        PlayerPrefs.SetFloat(AudioManager2.MUSIC_KEY, musicSlider.value); 
        PlayerPrefs.SetFloat(AudioManager2.SFX_KEY, sfxSlider.value);
   }

   private void SetMusicVolume(float value)
   {
        //Setea el canal de Musica del AudioMixer de musica al valor que tiene el slider
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
   }

   private void SetSFXVolume(float value)
   { 
        //Setea el canal de SoundsEffects del AudioMixer al valor que se encuentra en el slider
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
   }
}
