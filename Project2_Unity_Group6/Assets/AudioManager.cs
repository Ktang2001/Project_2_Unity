using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip gameOver;
    public AudioClip laserSound;
    public static AudioManager instance;

    private void Start()
    {
        musicSource.clip= background;
        musicSource.Play();
    }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundEffects(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    
}
