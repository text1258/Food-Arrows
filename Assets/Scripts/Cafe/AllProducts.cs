using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllProducts", menuName = "ScriptableObjects/AllItems/AllProducts")]
public class AllProducts : AllItems
{
    [SerializeField] private  List<Product> products;
    public List<Product> Products => products;
}