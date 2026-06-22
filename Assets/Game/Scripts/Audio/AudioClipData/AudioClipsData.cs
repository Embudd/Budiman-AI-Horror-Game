using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipsData", menuName = "Scriptable Objects/AudioClipsData")]
public class AudioClipsData : ScriptableObject
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField, Range(0f, 1f)] private float _volume = 1f;
    [SerializeField, Range(0f, 1f)] private float _spatialBlend = 0f;    

    public AudioClip[] Clips => _clips;
    public float Volume => _volume;
    public float SpatialBlend => _spatialBlend;        
}
