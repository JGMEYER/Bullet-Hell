using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private EnemyCharacter _enemyPrefab;

    public float minSpawnDelay = 3f;
    public float maxSpawnDelay = 6f;
    private float _timeSinceLastSpawn;
    private float _nextSpawn;

	void Start () {
        ResetTimer();
	}
	
	void Update () {
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn > _nextSpawn) {
            EnemyCharacter enemy = Instantiate(_enemyPrefab) as EnemyCharacter;
            enemy.transform.position = transform.position;

            AITracker tracker = enemy.GetComponent<AITracker>();
            if (tracker != null) {
                GameObject player = GameObject.Find("Player");
                tracker.SetTarget(player);
            }

            ResetTimer();
        }
	}

    private void ResetTimer() {
        _timeSinceLastSpawn = 0;
        _nextSpawn = Random.Range(minSpawnDelay, maxSpawnDelay);
    }
}
