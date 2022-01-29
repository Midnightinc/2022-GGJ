using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelManager
{
    public class RoomManager : MonoBehaviour
    {
        /// <summary>
        /// Stores all parent objects of rooms, serialized to editor
        /// </summary>
        [SerializeField] private LevelMaster[] roomParents;


        /// <summary>
        /// ID for currently loaded level
        /// </summary>
        private int currentRoomID = -1;


        /// <summary>
        /// Unloads the current room and loads in the next
        /// </summary>
        /// <param name="index"></param>
        public void LoadRoom(int index)
        {
            roomParents[currentRoomID].Load();

            roomParents[index].Unload();
            currentRoomID = index;
        }

    }
}