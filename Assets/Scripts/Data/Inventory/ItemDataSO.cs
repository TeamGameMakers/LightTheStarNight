using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Data/Item")]
    public class ItemDataSO : ScriptableObject
    {
        public new string name;
        public string info;
        public Sprite icon;
        public bool canPick = false;
    }
}
