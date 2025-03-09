using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    [SerializeField] private AudioSource soundObj;
    [SerializeField] AudioClip onboarding;


    private int voiceSequence = 0;
    void Start()
    {
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        PlaySound(onboarding, new GameObject("AudioSource").transform, 1, 3f);
        voiceSequence++;
        //show choices after the onboarding audio clip ends
        Invoke("ShowChoices", onboarding.length + 3.2f);
    }
    //play the onboarding audio clip when it starts after a 3 second delay
    public void PlaySound(AudioClip clip, Transform spawn, float volume, float delay)
    {
        AudioSource audioSource = Instantiate(soundObj, spawn.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.PlayDelayed(delay);
        float clipLength = clip.length + delay;
        Destroy(audioSource.gameObject, clipLength);
    }
}
