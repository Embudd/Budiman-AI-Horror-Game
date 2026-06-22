using UnityEngine;
using System;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "DoorEvent", menuName = "Scriptable Objects/DoorEvent")]
public class DoorAudioEvent : ScriptableObject
{
    [SerializeField] private AudioClipData _openClipData;
    [SerializeField] private AudioClipData _lockedClipData;
    [SerializeField] private AudioClipData _closeClipData;

    public void DoorOpened(AudioSource audioSource)
    {

        PlayDoorSFX(audioSource, _openClipData.Clip, _openClipData.SpatialBlend, _openClipData.Volume);
    }

    public void DoorClosed(AudioSource audioSource)
    {
        PlayDoorSFX(audioSource, _closeClipData.Clip, _closeClipData.SpatialBlend, _closeClipData.Volume);
    }

    public void DoorLocked(AudioSource audioSource)
    {
        PlayDoorSFX(audioSource, _lockedClipData.Clip, _lockedClipData.SpatialBlend, _lockedClipData.Volume);
    }

    private void PlayDoorSFX(AudioSource audioSource ,AudioClip clip, float spatialBlend, float volume)
    {
        audioSource.clip = clip;
        audioSource.spatialBlend = spatialBlend;
        audioSource.volume = volume;        

        audioSource.Play();
    }
}
