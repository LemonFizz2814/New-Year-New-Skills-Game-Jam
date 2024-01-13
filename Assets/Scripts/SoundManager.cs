using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<Audio> soundList = new List<Audio>();

    [Serializable]
    public struct Audio
    {
        public SoundType soundType;
        public AudioClip clip;
    }

    public enum SoundType
    {
        DrawCards, // done
        CardHover, // done
        ButtonHover,
        PlayCard, // done
        ScorePoint, // done
        LosePoint, // done
        NextTurn, // done
        Gameover, // done
        GameWon, // done
        ButtonPressed, //done
    }

    public void PlaySound(SoundType _soundType)
    {
        audioSource.PlayOneShot(GetAudio(_soundType).clip);
    }

    public Audio GetAudio(SoundType _soundType)
    {
        foreach (Audio audio in soundList)
        {
            if(audio.soundType == _soundType)
            {
                return audio;
            }
        }

        Debug.LogError("INCORRECT SOUND FOUND");
        return soundList[0];
    }

    public void ButtonHoverSound()
    {
        PlaySound(SoundType.ButtonHover);
    }
}
