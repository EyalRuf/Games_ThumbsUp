using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private AudioClip blockSound;
    AudioSource pAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        pAudioSource = GetComponent<AudioSource>();
    }

    public void PlayHitSound()
    {
        pAudioSource.PlayOneShot(hitSound);
    }

    public void PlayJumpSound()
    {
        pAudioSource.PlayOneShot(jumpSound);
    }

    public void PlayDashSound()
    {
        pAudioSource.PlayOneShot(dashSound, 0.65f);
    }

    public void PlayThrowSound()
    {
        pAudioSource.PlayOneShot(throwSound);
    }

    public void PlayBlockSound()
    {
        pAudioSource.PlayOneShot(blockSound, 0.9f);
    }
}
