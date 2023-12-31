using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("UI")]
    public AudioSource buttonClick;

    [Header("Universal Items")]
    public AudioClip equipItem;
    public AudioClip unequipItem;
    public AudioSource audioManagerSourceItem;

    [Header("Missy")]
    [SerializeField]
    AudioSource audioManagerSourcePlayerHurt;
    [SerializeField]
    AudioSource audioManagerSourcePlayerDead;
    public AudioSource playerAttackAudioSource;
    public AudioSource playerMovement;
    public AudioSource playerThud;
    public AudioClip playerDeathExplosionSound;
    public AudioClip playerSquishyStep;
    public AudioClip[] playerHurtSounds;
    public AudioClip[] playerDeathSounds;

    [Header("Items")]

    [Header("Sabertooth")]
    public AudioSource sabretoothCatch;
    public AudioSource sabretoothThrow;

    [Header("Slug Legs")]
    public AudioSource slugWalk;

    [Header("Nubs")]
    public AudioSource nubsAbility;
    public AudioSource nubsStep0;
    public AudioSource nubsStep1;

    [Header("Tripod")]
    public AudioSource tripodAbility;
    public AudioSource tripodWalk;
    public AudioSource tripodBounce;

    [Header("Pea Shooter")]
    public AudioSource peaShooterAttack;

    [Header("Squtio")]
    public AudioSource SquitoAttack;

    [Header("Shotgun")]
    public AudioSource shotgunAttack; //shoot
    public AudioSource shotgunAttackHit; //when projhectile hits enemy

    [Header("Baby Lob")]
    public AudioSource babyAttack;

    [Header("Eyeball")]
    public AudioSource eyeballAttack;

    [Header("Quill LMG")]
    public AudioSource urchinAttack;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void ItemPickUp()
    {
        audioManagerSourceItem.Play();
    }

    public void ButtonClick()
    {
        buttonClick.Play();
    }
    public void PlayerHurt()
    {
        var sound = playerHurtSounds[Random.Range(0, playerHurtSounds.Length)];

        audioManagerSourcePlayerHurt.clip = sound;
        audioManagerSourcePlayerHurt.Play();
    }
    
    public void PlayerDeathScream()
    {
        var sound = playerDeathSounds[Random.Range(0, playerDeathSounds.Length)];

        audioManagerSourcePlayerHurt.clip = sound;
        audioManagerSourcePlayerHurt.Play();
    }

    public void PlayerDeathExplosion()
    {
        audioManagerSourcePlayerDead.clip = playerDeathExplosionSound;
        audioManagerSourcePlayerDead.Play();
    }

 

    public void VaryPitch(AudioSource _audioSource)
    {
        _audioSource.pitch = Random.Range(128 - 30, 128 + 30);
    }

    public void SingleStep(AudioSource _audioSource)
    {
        //VaryPitch(_audioSource);
        _audioSource.Play();
    }
}
