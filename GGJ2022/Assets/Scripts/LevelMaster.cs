using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelManager
{
    public class LevelMaster : MonoBehaviour
    {
        [Header("Player Properties")]
        [SerializeField] private Vector3 playerSpawn;

        [Header("AI Properties")]
        [SerializeField] private Vector3[] AISpawnLocations;


        /// <summary>
        /// Returns player spawn location relative to this levels transform
        /// </summary>
        public Vector3 GetPlayerSpawn()
        {
            var local = transform.position;
            return local + playerSpawn;
        }


        /// <summary>
        /// Returns the AI spawn locations relative to this levels transform
        /// </summary>
        /// <returns></returns>
        public Vector3[] GetAISpawns()
        {
            var local = transform.position;
            Vector3[] locations = new Vector3[AISpawnLocations.Length];
            for (int i = 0; i < locations.Length; i++)
            {
                locations[i] = local + AISpawnLocations[i];
            }
            return locations;
        }







#if UNITY_EDITOR

        /*
         * Scuffed Unity editor to make setting spawn locations easier,
         * Could have just fumbled this in the GET method but thought this might be better ;) ♥ u Jon
         */


        private void OnDrawGizmos()
        {
            //get local space
            var local = transform.position;


            //green color for player spawn location
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(local + playerSpawn, settings.gizmoSize);  //draw wire sphere to denote position in unity editor   

            //white color for AI spawns
            Gizmos.color = Color.white;

            if (AISpawnLocations == null || AISpawnLocations.Length == 0)
            {
                return;
            }
            foreach (var location in AISpawnLocations)
            {
                Gizmos.DrawWireSphere(local + location, settings.gizmoSize);  //draw wire sphere to denote position in unity editor
            }
        }


        [Header("Editor Properties")]
        /// <summary>
        /// Made into a struct to force into dropdown in Unity Editor (personal preference, not required)
        /// </summary>
        [SerializeField] private EditorSettings settings;

        [System.Serializable]
        struct EditorSettings
        {
            public float gizmoSize;
        }
#endif

    }






    public class BulletPool : MonoBehaviour
    {
        public static BulletPool Instance;

        public int poolSize;

        private GameObject[] pool;
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
        }




        public GameObject GetBullet()
        {
            if (lastRetrieved == poolSize)
            {
                lastRetrieved = 0;
            }
            return pool[lastRetrieved++];
        }
    }
}