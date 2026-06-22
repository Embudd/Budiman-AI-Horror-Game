using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioClipData", menuName = "Scriptable Objects/AudioClipData")]
public class AudioClipData : ScriptableObject
{
    [SerializeField] private AudioClip _clip;
    [SerializeField, Range(0f, 1f)] private float _volume = 1f;
    [SerializeField, Range(0f, 1f)] private float _spatialBlend = 0f;    

    public AudioClip Clip => _clip;
    public float Volume => _volume;
    public float SpatialBlend => _spatialBlend;        
}
