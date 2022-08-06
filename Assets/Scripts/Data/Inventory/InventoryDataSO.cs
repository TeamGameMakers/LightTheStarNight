using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "Data/Inventory Data")]
    [Serializable]
    public class InventoryDataSO : ScriptableObject
    {
        // public List<ItemDataSO> items = new List<ItemDataSO>(6);
        // public List<ItemDataSO> pickedItems = new List<ItemDataSO>();

        public int batteryNum;
        public float powerRemaining;
        
        /// <summary>
        /// 储藏室的钥匙
        /// </summary>
        public bool hasKey1;
        
        /// <summary>
        /// 办公室钥匙
        /// </summary>
        public bool hasKey2;
    }
}
