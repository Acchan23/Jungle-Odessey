using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoIntro;
    // Start is called before the first frame update
    private void Awake() 
    {
        AudioManager2.Instance.StopMusic(); 
        videoIntro = GetComponent<VideoPlayer>();
        videoIntro.Play();
        videoIntro.loopPointReached += finishVideo;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            finishVideo(videoIntro);
        }
    }

    void finishVideo(VideoPlayer player)
    {
        player.Stop();
        SceneManager.LoadScene("Main");

    }
}
