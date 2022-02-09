using System.Collections.Generic;
using UnityEngine;
public class StoreItemSendCommand : MonoBehaviour, ICommand
{
    public List<StoreItem> StoreItems = new List<StoreItem>();
    [SerializeField]
    private TreeGrower _tree;
    private void Awake()
    {
        StoreItems.Add(_tree.Essence);
    }
    public void Add(StoreItem storeItem)
    {
        StoreItems.Add(storeItem);
    }
}