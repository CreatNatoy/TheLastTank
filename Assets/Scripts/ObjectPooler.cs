using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable] // ���������� ����� � ����� 
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size; 
    }

    public static ObjectPooler Instance;

    public void Awake()
    {
        Instance = this; // ���� ������ �� ���� ������ 
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        //����� Queue<T> ������������ ������� �������, ���������� �� ��������� FIFO ("������ ����� - ������ �����").
        poolDictionary = new Dictionary<string, Queue<GameObject>>();  // Queue ��� ������� 

        foreach (Pool pool in pools) // ������� ��� ������� ��� �����
        {
            Queue<GameObject> objectPool = new Queue<GameObject>(); 

            for(int i=0; i < pool.size; i++) // ������� �� ���������� ��� ������� � ���������� 
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj); 
            }

            poolDictionary.Add(pool.tag, objectPool);  // ��������� �� (���� ��� ����� ����� ����, ��� ������) 
        }
    }

//   Dequeue: ��������� � ���������� ������ ������� �������
//    Enqueue: ��������� ������� � ����� �������
//    Peek: ������ ���������� ������ ������� �� ������ ������� ��� ��� ��������

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag)) // ���� ���� �� ������ �� ������� 
            return null;

        GameObject objectToSpawn = poolDictionary[tag].Dequeue(); // �������� ������ 
        // �������� ��� 
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn); // ��������� ��� � ����� ������� . �.� ����������� ��� 

        return objectToSpawn; 
    }
}
