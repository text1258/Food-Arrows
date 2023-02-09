using UnityEditor;
using UnityEngine;

public class AllScriptableObjects 
{
    public static T[] GetAllScriptableObjects<T>() where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
        T[] scriptableObjects = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            scriptableObjects[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }
        return scriptableObjects;
    }
}