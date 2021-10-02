using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable] // показывает класс в юнити 
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size; 
    }

    public static ObjectPooler Instance;

    public void Awake()
    {
        Instance = this; // даем ссылку на этот объект 
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        //Класс Queue<T> представляет обычную очередь, работающую по алгоритму FIFO ("первый вошел - первый вышел").
        poolDictionary = new Dictionary<string, Queue<GameObject>>();  // Queue тип очереди 

        foreach (Pool pool in pools) // создаем все префабы что имеем
        {
            Queue<GameObject> objectPool = new Queue<GameObject>(); 

            for(int i=0; i < pool.size; i++) // создаем их количество что указано в инспекторе 
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj); 
            }

            poolDictionary.Add(pool.tag, objectPool);  // добавляем их (ключ шоб найти можно было, сам объект) 
        }
    }

//   Dequeue: извлекает и возвращает первый элемент очереди
//    Enqueue: добавляет элемент в конец очереди
//    Peek: просто возвращает первый элемент из начала очереди без его удаления

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag)) // если ключ не верный то выходим 
            return null;

        GameObject objectToSpawn = poolDictionary[tag].Dequeue(); // получаем объект 
        // вызываем его 
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn); // добавляем его в конец очереди . т.к использован уже 

        return objectToSpawn; 
    }
}
