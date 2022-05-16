using System.Collections.Generic;
using UnityEngine;

namespace ISVR {

    public class RandomSpawner : MonoBehaviour {

        [SerializeField] private int entitiesToSpawn;
        [SerializeField] private List<Transform> spawners;

        private void Start() {
            SpawnRandomInList();
        }

        private void SpawnRandomInList() {
            for (int i = 0; i < entitiesToSpawn; i++) {
                Spawn();
            }
        }

        private void Spawn() {
            int randomIndex = Random.Range(0, spawners.Count);
            bool wasSpawned = TrySpawn(spawners[randomIndex]);
            while (!wasSpawned) {
                randomIndex = Random.Range(0, spawners.Count);
                wasSpawned = TrySpawn(spawners[randomIndex]);
            }
        }

        private bool TrySpawn(Transform spawner) {
            if (spawner.TryGetComponent(out Bug bug)) return false;
            if (spawner.TryGetComponent(out Electrical electrical)) {
                Destroy(electrical);
            }
            var bugComponent = spawner.gameObject.AddComponent<Bug>();
            bugComponent.Value = 10f;
            return true;
        }
    }
}