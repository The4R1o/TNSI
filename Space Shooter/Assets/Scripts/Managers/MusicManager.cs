using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public List<AudioClip> songs = new List<AudioClip>();
    [SerializeField] AudioSource audioSource;
    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = PlayRandomSong();
        audioSource.Play();
    }
    AudioClip PlayRandomSong()
    {
        var getRadnomSong = Random.Range(0, songs.Count);
        return songs[getRadnomSong];
    }

    public void Mute(bool isMute)
    {
        if (isMute)
        {
            AudioListener.volume = 1f;
        }
        else
        {
            AudioListener.volume = 0f;
        }
    }
    private void Update()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = PlayRandomSong();
            audioSource.Play();
        }
    }
}
