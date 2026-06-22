using UnityEngine;

[CreateAssetMenu(fileName = "PlayerEventAudio", menuName = "Scriptable Objects/PlayerEventAudio")]
public class PlayerAudioEvent : ScriptableObject
{
    [SerializeField] private AudioClipsData _walkClips;
    [SerializeField] private AudioClipsData _runClips;

    private int _footstepIndex = 0;

    public void PlayFootStepSFX(PlayerState state)
    {
        if (state == PlayerState.Walk)
        {
            AudioManager.Instance.PlaySequentialSFX(_walkClips, ref _footstepIndex);
        }            
        else if (state == PlayerState.Run)
        {
            AudioManager.Instance.PlaySequentialSFX(_runClips, ref _footstepIndex);
        }
    }
}
