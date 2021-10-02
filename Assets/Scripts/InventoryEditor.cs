using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    private Inventory _invetory; 

    public void OnEnable()
    {
        _invetory = (Inventory)target; 
    }

    public override void OnInspectorGUI()
    {
        if(_invetory.Items.Count > 0)
        {
            foreach(Inventory.Item item in _invetory.Items)
            {
                EditorGUILayout.BeginVertical("box");
                item.Id =  EditorGUILayout.IntField("�������������", item.Id);
                item.Name = EditorGUILayout.TextField("��� ��������", item.Name);
                item.Image = (Sprite)EditorGUILayout.ObjectField("������", item.Image, typeof(Sprite), false);
                EditorGUILayout.EndVertical();   
            }
        }
        else
        {
            EditorGUILayout.LabelField("��������� ������! ��������� ���");
        }
        //������ ����������� 
        if(GUILayout.Button("�������� �������", GUILayout.Width(300), GUILayout.Height(20))) 
            _invetory.Items.Add(new Inventory.Item());
        // ���� ��������� �������� ���������� � ����� * �����������
        if (GUI.changed) 
            SetObjectDirty(_invetory.gameObject); 
    }

    public static void SetObjectDirty(GameObject obj)
    {
        EditorUtility.SetDirty(obj);
        EditorSceneManager.MarkSceneDirty(obj.scene); 
    }
}
