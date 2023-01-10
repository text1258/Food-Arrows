using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField] private uint iD;
    [SerializeField] private Sprite picture;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private uint price;
    
    public uint Price => price;
    public Sprite Picture => picture;
    public GameObject ItemPrefab => itemPrefab;
    public string ID
    {
        get
        {
            return iD.ToString();
        }
    }

    private void OnValidate()
    {
        if (price < 0)
        {
            price = 0;
        }
    }

    private void Awake()
    {
        SetNonRepeatingID();
    }

    private void Reset()
    {
        SetNonRepeatingID();
    }
    
    private void SetNonRepeatingID() => iD = FindMissingNumber(Resources.FindObjectsOfTypeAll<Item>().ToList().Select(i => i.iD).ToList());

    private uint FindMissingNumber(List<uint> list)
    {
        for (uint i = 1; i < list.Count; i++)
        {
            if (!list.Contains(i))
            {
                return i;
            }
        }
        return (uint)list.Count;
    }
}