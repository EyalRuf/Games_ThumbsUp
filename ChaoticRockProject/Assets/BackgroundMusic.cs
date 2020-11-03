using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> audioClips;
    private AudioSource audioSource;
    private int currentClipIndex;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        int rnd = Random.Range(0, audioClips.Count);
        currentClipIndex = rnd;
        audioSource.clip = audioClips[currentClipIndex];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            currentClipIndex = (currentClipIndex + 1) % audioClips.Count;
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
        }
    }
}
