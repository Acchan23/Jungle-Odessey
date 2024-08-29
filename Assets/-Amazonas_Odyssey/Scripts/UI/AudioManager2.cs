using System.Collections;
using System.Collections.Generic;
using System; 
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement; 

public class AudioManager2 : MonoBehaviour
{
    public static AudioManager2 Instance { get; private set; }
    [SerializeField] AudioMixer mixer; 
    public AudioSource musicSource, sfxSource; 

    //Lista de archivos para cada uno de los escenarios
    public AudioClip[] musicClips;
    public string[] sceneNames; 
    private Dictionary<string, AudioClip> musicDictionary;

    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    private void Awake() 
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this); 
        }

        musicSource.loop = true;     
    }

    private void Start() 
    {
        LoadVolume();
        //musicSource = GetComponent<AudioSource>();    
        //sfxSource = GetComponent<AudioSource>();

        musicDictionary = new Dictionary<string, AudioClip>();

        //Carga el diccionario con la lista de escenarios y su respectiva música
        for (int i = 0; i< sceneNames.Length; i++){
            musicDictionary.Add(sceneNames[i],musicClips[i]);
        }
        
        PlayMusicScene(); 
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    //Volume guardado en el script VolumeSettings
    private void LoadVolume()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 0.7f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 0.7f);

        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicScene();
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.clip = sfxClip;
        sfxSource.PlayOneShot(sfxSource.clip); 
    }

    public void PlaySFXOne(AudioClip sfxClip)
    {
        sfxSource.clip = sfxClip;
        sfxSource.Play(); 
    }
   public void PlayMusicScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        Debug.Log("Escena Actual: " + currentScene);

        if (musicDictionary.ContainsKey(currentScene))
        {
            musicSource.clip = musicDictionary[currentScene];
            musicSource.Play();
        }
    }

    public void StopMusic()
    {   
        musicSource.Pause();
    }
}
