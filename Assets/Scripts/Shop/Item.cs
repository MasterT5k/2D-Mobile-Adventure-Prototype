using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string _itemName;
    [SerializeField]
    private int _itemID = 0;
    [SerializeField]
    private int _cost = 100;
    private Text _costText = null;

    private void Start()
    {
        _costText = transform.Find("Cost Text").GetComponent<Text>();
        if (_costText != null)
        {
            _costText.text = _cost + "G";
        }
    }

    public int GetCost()
    {
        return _cost;
    }

    public string GetItemName()
    {
        return _itemName;
    }

    public int GetItemID()
    {
        return _itemID;
    }
}
