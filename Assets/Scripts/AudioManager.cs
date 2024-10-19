using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip backgroundInBattle;
    public AudioClip attacking;
    public AudioClip walking;
    public AudioClip takingDamage;
    public AudioClip trueBuy;
    public AudioClip falseBuy;
    public AudioClip winInBattle;
    public AudioClip loseInBattle;
    public AudioClip dodgeSound;
    public AudioClip attackSound;

    private float walkingPitch = 0.75f;

    private void Awake()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        musicSource.volume = 0.9f;
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlayBackgroundMusicInBattle()
    {
        musicSource.volume = 0.7f;
        musicSource.clip = backgroundInBattle;
        musicSource.Play();
    }

    public void PlayAttackingSound()
    {
        float randomPitch = Random.Range(0.9f, 1.1f);
        SFXSource.pitch = randomPitch;
        SFXSource.PlayOneShot(attacking);
    }

    public void PlayWalkingSound()
    {
        SFXSource.volume = 0.8f;
        SFXSource.pitch = walkingPitch;
        SFXSource.clip = walking;
        SFXSource.loop = true;
        SFXSource.Play(); 
    }

    public void SetWalkingPitch(float pitch)
    {
        walkingPitch = pitch;
        SFXSource.pitch = walkingPitch;
    }

    public void PlayTakingDamageSound()
    {
        float randomPitch = Random.Range(0.8f, 1.2f);
        SFXSource.pitch = randomPitch;
        SFXSource.PlayOneShot(takingDamage);
    }

    public void PlayTrueBuy()
    {
        SFXSource.volume = 0.7f;
        SFXSource.PlayOneShot(trueBuy);
    }

    public void PlayFalseBuy()
    {
        SFXSource.volume = 0.7f;
        SFXSource.PlayOneShot(falseBuy);
    }

    public void PlayWinBattle()
    {
        SFXSource.volume = 0.7f;
        SFXSource.PlayOneShot(winInBattle);
    }

    public void PlayLoseBattle()
    {
        SFXSource.volume = 0.7f;
        SFXSource.PlayOneShot(loseInBattle);
    }
    public void PlayDodgeSound()
    {
        float randomPitch = Random.Range(0.8f, 1.2f);
        SFXSource.pitch = randomPitch;
        SFXSource.volume = 0.7f;
        SFXSource.PlayOneShot(dodgeSound);
    }
    public void PlayAttackSound()
    {
        float randomPitch = Random.Range(0.8f, 1.2f);
        SFXSource.pitch = randomPitch;
        SFXSource.volume = 0.7f;
        SFXSource.PlayOneShot(attackSound);
    }


    public void StopSFX()
    {
        SFXSource.Stop();
    }
}