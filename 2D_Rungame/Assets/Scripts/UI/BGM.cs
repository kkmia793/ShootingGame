using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip[] bgmClips; // MP3ファイルの配列
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (bgmClips.Length > 0)
        {
            PlayRandomBGM();
        }
        else
        {
            Debug.LogError("BGM clips array is empty. Add MP3 files to the array.");
        }
    }

    void PlayRandomBGM()
    {
        int randomIndex = Random.Range(0, bgmClips.Length);
        AudioClip selectedBGM = bgmClips[randomIndex];

        audioSource.clip = selectedBGM;
        audioSource.Play();
    }
}
