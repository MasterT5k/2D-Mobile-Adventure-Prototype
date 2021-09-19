using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance = null;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is NULL");
            }
            return _instance;
        }
    }

    [SerializeField]
    private Text _gemCountText = null;
    [SerializeField]
    private Text _shopGemCountText = null;
    [SerializeField]
    private Image _selectImage = null;
    [SerializeField]
    private Transform _healthBar = null;
    private List<Image> _healthUnits = new List<Image>();

    private void Awake()
    {
        _instance = this;

        if (_healthBar != null)
        {
            for (int i = 0; i < _healthBar.childCount; i++)
            {
                Image image = _healthBar.GetChild(i).GetComponent<Image>();
                if (image != null)
                {
                    _healthUnits.Add(image);
                }
            }
        }
    }

    public void ClearShopSelection()
    {
        if (_selectImage != null)
        {
            _selectImage.gameObject.SetActive(false);
        }
    }

    public void UpdateSelectedItem(RectTransform rowRect)
    {
        if (_selectImage != null)
        {
            _selectImage.gameObject.SetActive(true);
            _selectImage.rectTransform.sizeDelta = rowRect.sizeDelta;
            _selectImage.rectTransform.anchoredPosition = rowRect.anchoredPosition; 
        }
    }

    public void UpdateGemCount(int gems)
    {
        if (_shopGemCountText != null)
        {
            _shopGemCountText.text = gems + "G";
        }

        if (_gemCountText != null)
        {
            _gemCountText.text = gems.ToString();
        }
    }

    public void UpdateHealthBar(int health)
    {
        if (health < 0)
        {
            health = 0;
        }

        if (_healthUnits.Count > 0)
        {
            for (int i = 0; i < _healthUnits.Count; i++)
            {
                if (i >= health)
                {
                    _healthUnits[i].color = Color.clear;
                }
                else
                {
                    _healthUnits[i].color = Color.white;
                }
            }
        }
    }
}
