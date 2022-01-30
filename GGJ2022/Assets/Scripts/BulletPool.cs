using System;
using UnityEngine;

namespace BulletHandlers
{
    public class BulletPool : MonoBehaviour
    {
        public static BulletPool Instance;

        [SerializeField] private int poolSize;
        [SerializeField] private GameObject prefab;

        private BulletClass[] pool;
        private int lastRetrieved = 0;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
            }

            pool = new BulletClass[poolSize];
            for (int i = 0; i < poolSize; i++)
            {
                pool[i] = Instantiate(prefab).GetComponent<BulletClass>();
                pool[i].gameObject.SetActive(false);
            }
        }




        public BulletClass GetBullet()
        {
            if (lastRetrieved == poolSize)
            {
                lastRetrieved = 0;
            }

            return pool[lastRetrieved++];
        }

        public void ReturnToPool(BulletClass bullet)
        {
            bullet.gameObject.SetActive(false);
        }
    }
}