using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class ShopKeeper : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopPanel = null;
    private Item _currentItemSelection = null;
    private int _currentItemCost;
    private Player _player;

    private void Start()
    {
        if (_shopPanel != null)
        {
            _shopPanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_player == null)
            {
                _player = other.GetComponent<Player>(); 
            }

            if (_shopPanel != null)
            {
                _shopPanel.SetActive(true);
            }

            ClearItemSelection();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_shopPanel != null)
            {
                _shopPanel.SetActive(false);
            }
        }
    }

    public void SelectItem(GameObject row)
    {
        if (row != null)
        {
            RectTransform rowRect = row.GetComponent<RectTransform>();
            if (rowRect != null)
            {
                UIManager.Instance.UpdateSelectedItem(rowRect); 
            }

            _currentItemSelection = row.GetComponent<Item>();
            if (_currentItemSelection != null)
            {
                _currentItemCost = _currentItemSelection.GetCost();
            }
        }
    }

    public void BuyItem()
    {
        if (_currentItemSelection != null)
        {
            int gems = _player.GetCemCount();
            if (gems >= _currentItemCost)
            {
                _player.UpdateGems(-_currentItemCost);
                Debug.Log("Purchased: " + _currentItemSelection.GetItemName());

                Button button = _currentItemSelection.transform.GetComponentInChildren<Button>();
                if (button != null)
                {
                    button.interactable = false;
                }

                if (_currentItemSelection.GetItemID() == 2)
                {
                    GameManager.Instance._hasKeyToCastle = true;
                }

                ClearItemSelection();
            }
            else
            {
                if (_shopPanel != null)
                {
                    _shopPanel.SetActive(false);
                }

                ClearItemSelection();
                Debug.Log("Come back when you get more Gems.");
            }
        }
        else
        {
            Debug.Log("Select an Item to buy.");
        }
    }

    private void ClearItemSelection()
    {
        if (_currentItemSelection != null)
        {
            _currentItemSelection = null;
            _currentItemCost = 0;
        }

        UIManager.Instance.ClearShopSelection();
    }
}
