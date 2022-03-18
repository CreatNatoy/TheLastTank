using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class Item
    {
        public int Id;
        public string Name;
        public Sprite Image;
    }

    public List<Item> Items;
}
