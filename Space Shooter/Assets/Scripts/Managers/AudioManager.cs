using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Serializable]
    public class AudioClass
    {
        public string soundName;
        public AudioClip audioClip;
        [Range(0, 1)] 
        public float audioVolume;
    }

    [SerializeField] private AudioClass[] sounds;

    // Reference //
    private Camera camera;

    private void Awake()
    {
        #region SINGLETON
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
        #endregion

        camera = Camera.main;
    }
    public void PlaySound(string name)
    {
        foreach (AudioClass sound in sounds)
        {
            if (sound.soundName == name)
            {
               AudioSource.PlayClipAtPoint(sound.audioClip, camera.transform.position, sound.audioVolume);
                return;
            }
        }
        Debug.LogWarning("Sound " + name + "doesn't exist!");
    }
}

