//Fade In and Out are from this source: https://forum.unity.com/threads/logarithmic-fade-out-and-in.438953/

using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    private static AudioManager INSTANCE = null;

    public Sound[] sounds;

    private bool isMute = false;

    private void Awake()
    {

        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(gameObject);
            return;
        }

        INSTANCE = this;

        DontDestroyOnLoad(gameObject);

        Debug.Log("Audio Manager created.");
    }

    //Initialize the AudioSource and its components at Start.
    private void Start()
    {

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.isLoop;

            s.source.playOnAwake = false;

        }

        //Play the "BGM Menu" sound.
        PlaySound(15);
    
    }

    #region Sound Logics

    //Plays the particular sound.
    public static void PlaySound(int id)
    {

        if (INSTANCE == null)
            return;

        if (id == 0)
            return;

        Sound s = INSTANCE.FindSound(id);

        if (s == null)
            return;

        s.source.Play();

    }

    //Mutes or Unmutes all sounds.
    public static void MuteSound()
    {

        INSTANCE.isMute = !INSTANCE.isMute;

        foreach (Sound s in INSTANCE.sounds)
            s.source.mute = INSTANCE.isMute;
    }

    //Responsible for fading out the background musics.
    public static void FadeOut(float fadeSpeed = 0.5f, int id = 15)
    {
        INSTANCE.StartCoroutine(INSTANCE.EnumFadeOut(fadeSpeed, id));
    }

    //Logarithmically fades out the sound.
    private IEnumerator EnumFadeOut(float fadeSpeed, int id)
    {

        Sound s = FindSound(id);

        while (s.source.volume > 0.1f)
        {

            //Waits for one frame.
            yield return null;

            s.source.volume *= fadeSpeed;
            Debug.Log("Fades Out: " + s.source.volume + " volume.");
        }

        s.source.volume = 0.1f;

        Debug.Log("Fades Out: " + s.source.volume + " volume.");

        s.source.Stop();

    }

    //Responsible for fading in the background musics.
    public static void FadeIn(float fadeSpeed = 0.5f, int id = 15)
    {
        INSTANCE.StartCoroutine(INSTANCE.EnumFadeIn(fadeSpeed, id));
    }

    //Logarithmically fades in the sound.
    private IEnumerator EnumFadeIn(float fadeSpeed, int id)
    {

        Sound s = FindSound(id);

        s.source.volume = 0.1f;

        s.source.Play();

        while(s.source.volume < s.volume)
        {

            yield return null;

            s.source.volume /= fadeSpeed;
            Debug.Log("Fades In: " + s.source.volume + " volume.");

        }

        s.source.volume = s.volume;

        Debug.Log("Fades In: " + s.source.volume + " volume.");

    }

    //Finds a particular sound using its id using binary search optimized for AudioManager.
    private Sound FindSound(int id)
    {

        int low = 0, high = sounds.Length - 1, mid = (low + high) / 2;

        while (low <= high)
        {
            if (sounds[mid].id < id)
                low = mid + 1;

            else if (sounds[mid].id > id)
                high = mid - 1;

            else
                return sounds[mid];

            mid = (low + high) / 2;
        }

        return null;

    }

    #endregion

    #region Getter(s)
    
    //Returns true if the sound is playing, otherwise false.
    public static bool IsPlaying(int id)
    {

        if (INSTANCE == null)
            return false;

        return INSTANCE.FindSound(id).source.isPlaying;

    }

    //Returns if the sound is muted/unmuted, otherwise false.
    public static bool IsMute()
    {
        if (INSTANCE == null)
            return false;

        return INSTANCE.isMute;
    }
    
    #endregion

    #region Sound Class

    [System.Serializable]
    public class Sound
    {

        public string name = "";
        public int id = 0;

        [Space]
        [Header("Components:")]

        public AudioClip clip = null;

        [Space]

        public float volume = 0.1f;
        public float pitch = 0.1f;

        [Space]
        public bool isLoop = false;

        [HideInInspector]
        public AudioSource source = null;

    }

    #endregion
}
