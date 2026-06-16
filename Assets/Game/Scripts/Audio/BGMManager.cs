using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private TriggerBox _triggerBox;
    [SerializeField] private AudioSource _ambienceHorror;
    [SerializeField] private AudioSource _tenseLoop;

    void Start()
    {
        _triggerBox.OnShowGhostEventTriggered += TriggerBGM;
    }

    void OnDestroy()
    {
        _triggerBox.OnShowGhostEventTriggered -= TriggerBGM;
    }

    private void TriggerBGM()
    {
        _ambienceHorror.Stop();
        _tenseLoop.Play();
    }
}
