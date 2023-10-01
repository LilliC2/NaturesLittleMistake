using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    AudioSource audioManagerSourcePlayerHurt;
    [SerializeField]
    AudioSource audioManagerSourceItem;
    public AudioClip[] playerHurtSounds;
    public AudioClip[] playerDeathSounds;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ItemPickUp()
    {
        audioManagerSourceItem.Play();
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
}
