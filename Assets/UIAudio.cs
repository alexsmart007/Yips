using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] AudioClip click;
    [SerializeField] AudioClip swap;
    [SerializeField] AudioSource source;

    public void PlayClick()
    {
        source.PlayOneShot(click);
    }

    public void PlaySwap()
    {
        source.PlayOneShot(swap);
    }
}