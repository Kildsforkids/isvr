using System.Collections.Generic;
using UnityEngine;
using ISVR.Core.Bugs;
using ISVR.Core.Devices;

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
            if (spawner.TryGetComponent(out Wiretapper wiretapper)) return false;
            //if (spawner.TryGetComponent(out Electronic electronic)) {
            //    Destroy(electronic);
            //}
            wiretapper = spawner.gameObject.AddComponent<Wiretapper>();
            wiretapper.SetHarmonicsValue(30f, 5f);
            return true;
        }
    }
}