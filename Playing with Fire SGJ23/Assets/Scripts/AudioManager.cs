using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public enum Sfx {
        knife,
        knife_miss,
        step,
        encounter
    }

    public int huntAmount = 0;

    public AudioClip[] sfxClips;

    AudioSource musicSource;
    public AudioClip patrolSong, huntIntro, huntBody;
    private Coroutine huntIntroCoroutine = null;
    // Start is called before the first frame update
    private void Awake() {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        musicSource = GetComponent<AudioSource>();
        

    }

    void Start()
    {
        PlaySongPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySongHunt() {
        huntIntroCoroutine = StartCoroutine(HuntSong());
    }

    private void PlaySong(AudioClip song, bool loop) {
        musicSource.Stop();
        musicSource.clip = song;
        musicSource.Play();
        musicSource.loop = loop;
    }

    public void PlaySongPatrol() {
        if (huntIntroCoroutine != null) {
            StopCoroutine(huntIntroCoroutine);
        }

        PlaySong(patrolSong, true);
    }

    private IEnumerator HuntSong() {
        PlaySong(huntIntro, false);

        yield return new WaitWhile(() => musicSource.isPlaying);

        PlaySong(huntBody, true);

    }


    public void PlaySoundEffect(Sfx sfx, float vol, Vector2 pitch) {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = sfxClips[(int)sfx];
        source.volume = vol;
        source.pitch = Random.Range(pitch.x, pitch.y);
        source.Play();
        StartCoroutine(DestroySource(source));

    }

    private IEnumerator DestroySource(AudioSource source) {
        yield return new WaitWhile(() => source.isPlaying);
        Destroy(source);
    }

    public void HuntUpdate(int i) {
        huntAmount += i;
        Debug.Log(huntAmount);
        if (huntAmount == 1) {
            PlaySongHunt();
        }
        if (huntAmount == 0) {
            PlaySongPatrol();
        }
    }


}
