using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    private List<string> _items = new List<string>();

    void Awake()
    {
        // Singleton guard — only one inventory exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // survives scene changes
    }

    public void AddItem(string itemName)
    {
        _items.Add(itemName);
        Debug.Log($"Picked up: {itemName}. Total items: {_items.Count}");
    }

    public bool HasItem(string itemName)
    {
        return _items.Contains(itemName);
    }

    public void RemoveItem(string itemName)
    {
        _items.Remove(itemName);
    }
}