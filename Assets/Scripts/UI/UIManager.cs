using System.Collections;
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
    private Text _playerGemCountText = null;
    [SerializeField]
    private Image _selectImage = null;

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount)
    {
        if (_playerGemCountText != null)
        {
            _playerGemCountText.text = gemCount + "G"; 
        }

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
}
