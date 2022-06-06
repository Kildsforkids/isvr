using System.Collections.Generic;
using UnityEngine;

namespace ISVR {

    public class ObjectPooler : MonoBehaviour {

        [SerializeField] private GameObject poolObject;

        private Queue<GameObject> pool = new Queue<GameObject>();

        public void Init(int count) {
            if (count <= 0) return;
            ClearPool();
            for (int i = 0; i < count; i++) {
                var go = Instantiate(poolObject, transform);
                //var component = go.GetComponent<T>();
                go.SetActive(false);
                pool.Enqueue(go);
            }
        }

        public T GetFromPool<T>() {
            if (pool.Count <= 0) return default(T);
            var go = pool.Dequeue();
            go.SetActive(true);
            return go.GetComponent<T>();
        }

        public void AddToPool(GameObject go) {
            go.SetActive(false);
            //var component = go.GetComponent<T>();
            pool.Enqueue(go);
        }

        private void ClearPool() {
            pool.Clear();
        }
    }
}
