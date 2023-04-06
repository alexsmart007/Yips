using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using System.Linq;
using System;

public class AudioControls : SerializedMonoBehaviour
{
    [SerializeField] Dictionary<int, AudioSource> audioFiles = new Dictionary<int, AudioSource>();
    [SerializeField] AudioSource[] audioProx;
    [SerializeField] float adjustmentSpeed;

    int desiredProxVolumes = 50;
    int[] desiredVolumes = { 50, 0, 0, 0, 50, 0};

    private void Start()
    {
        SceneTools.onSceneTransitionStart += OnSceneTransition;
        audioFiles.Values.ToList().ForEach(audio => audio.volume = 0);
        audioProx.ToList().ForEach(audio => audio.volume = 0);
    }

    private void OnSceneTransition()
    {
        SetAudio(new int[] { 0, 0, 0, 0, 0, 0 }, false);
        adjustmentSpeed = 50;
    }

    public void SetAudio(int[] audioVolumes, bool enableProx)
    {
        desiredVolumes = audioVolumes;
        desiredProxVolumes = enableProx ? 50 : 0;
    }

    private void Update()
    {
        foreach(var audioFile in audioFiles)
        {
            MoveTowards(audioFile.Value, desiredVolumes[audioFile.Key]);
        }

        audioProx.ToList().ForEach(audio => MoveTowards(audio, desiredProxVolumes));
    }

    private void MoveTowards(AudioSource audio, float desiredVolume)
    {
        if (audio.volume == desiredVolume) return;
            
        float directionToIncrement = Mathf.Sign(desiredVolume / 100f - audio.volume);
        float amountToIncrment = adjustmentSpeed * Time.deltaTime / 100f;
        audio.volume += directionToIncrement * amountToIncrment;
        audio.volume = directionToIncrement > 0
            ? Mathf.Min(audio.volume, desiredVolume / 100f)
            : Mathf.Max(audio.volume, desiredVolume / 100f);
    }

    private void OnDestroy()
    {
        SceneTools.onSceneTransitionStart -= OnSceneTransition;
    }
}
