using UnityEngine;

public class SightPerception : MonoBehaviour
{
    [SerializeField] private Transform _eyePosition;
    [SerializeField] private Transform _target;
    [SerializeField] private float _viewDistance = 10f;
    [SerializeField] private float _fieldOfView = 75f;
    [SerializeField] private LayerMask _targetLayer;   

    public bool CanSeeTarget { get; private set; }
    public Vector3 LastSeenPosition { get; private set; }

    private void Update()
    {
        CanSeeTarget = CheckSight();
    }    

    public bool CheckSight()
    {
        if (_target == null)
        {
            return false;
        }

        // Distance checking
        float distanceToTarget = Vector3.Distance(_eyePosition.position, _target.position);
        if (distanceToTarget > _viewDistance)
        {            
            return false;
        }

        // Field of view checking
        Vector3 dirToTarget = (_target.position - _eyePosition.position);
        float angleToTarget = Vector3.Angle(_eyePosition.forward, dirToTarget);

        // Divide by 2 because field of view is centered around the forward direction,
        // so we need to check if the angle is within half of the field of view on either side.
        if (angleToTarget > _fieldOfView / 2f)
        {            
            return false;
        }

        // Raycast checking
        if (Physics.Raycast(_eyePosition.position, dirToTarget.normalized, out RaycastHit hit, _viewDistance, _targetLayer))
        {
            if (hit.transform == _target)
            {                
                LastSeenPosition = _target.position;
                return true;
            }
        }

        return false;
    }    

    private void OnDrawGizmos()
    {
        if (_eyePosition == null) return;

        Gizmos.color = Color.red;

        bool isSeeTarget = CheckSight();
        if (isSeeTarget)
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireSphere(_eyePosition.position, _viewDistance);

        Vector3 leftSigth = Quaternion.Euler(0, -_fieldOfView / 2f, 0) * _eyePosition.forward;
        Vector3 rightSigth = Quaternion.Euler(0, _fieldOfView / 2f, 0) * _eyePosition.forward;

        Gizmos.DrawRay(_eyePosition.position, leftSigth * _viewDistance);
        Gizmos.DrawRay(_eyePosition.position, rightSigth * _viewDistance);
    }
}
