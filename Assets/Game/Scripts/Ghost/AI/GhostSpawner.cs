using UnityEngine;
using System.Collections;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GhostAIController _aiController;
    [SerializeField] private float _minSpawnDelay = 3f;
    [SerializeField] private float _maxSpawnDelay = 8f;
    [SerializeField] private float _minSpawnDistance = 3f;
    [SerializeField] private float _maxSpawnDistance = 5f;

    private Coroutine _SpawnCoroutine;

    private void Start()
    {
        _aiController.OnGhostDespawn += RestartSpawn;
    }                

    private void OnDestroy()
    {
        _aiController.OnGhostDespawn -= RestartSpawn;
    }             

    public void RestartSpawn()
    {
        if (_SpawnCoroutine != null)       
        {
            StopCoroutine(_SpawnCoroutine);
        }
        _SpawnCoroutine = StartCoroutine(SpawnGhostRoutine());
    }

    public IEnumerator SpawnGhostRoutine()
    {
        float spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
        yield return new WaitForSeconds(spawnDelay);

        if (_aiController.Target == null || _aiController.Target.IsHiding)
        {
            RestartSpawn();
            yield break;
        }

        SpawnGhost();
    }

    public void SpawnGhost()
    {
        float spawnDistance = Random.Range(_minSpawnDistance, _maxSpawnDistance);
        Vector3 spawnPos = _aiController.Target.transform.position - _aiController.Target.transform.forward * spawnDistance;
        spawnPos.y = _aiController.Target.transform.position.y;

        _aiController.NavMeshAgent.enabled = true;
        _aiController.NavMeshAgent.Warp(spawnPos);
        _aiController.transform.LookAt(_aiController.Target.transform);

        _aiController.gameObject.SetActive(true);

        _aiController.BehaviorGraphAgent.SetVariableValue("LastSeenPosition", _aiController.Target.transform.position);
        _aiController.BehaviorGraphAgent.enabled = true;
    }
}
