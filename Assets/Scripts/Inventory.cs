using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [System.Serializable] //отображение класса в инспекторе
    public class Item
    {
        public int Id;
        public string Name;
        public Sprite Image; 
    }

    public List<Item> Items; 
}
