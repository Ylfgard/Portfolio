using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class StoreItemSwitcher
{
    [SerializeField]
    private Button _nextStoreItemButton;
    [SerializeField]
    private Button _previousItemButton;
    [SerializeField]
    private SpriteView _storeItemSpriteView;
    private List<StoreItem> _storeItems;
    protected StoreItem _currentItem;
    private int _storeItemIndex;
    public void OnEnable()
    {

        _nextStoreItemButton.onClick.AddListener(MoveToNextItem);
        _previousItemButton.onClick.AddListener(MoveToPreviousItem);
    }
    public void OnDisable()
    {
        _nextStoreItemButton.onClick.RemoveListener(MoveToNextItem);
        _previousItemButton.onClick.RemoveListener(MoveToPreviousItem);
    }
    public void Initialize(StoreItemSendCommand callBack)
    {
        _storeItems = callBack.StoreItems;
        SetDefaultStoreItem();
    }
    private void MoveToNextItem()
    {
        if (_storeItemIndex + 1 >= _storeItems.Count)
        {
            _storeItemIndex = 0;
            SetCurrentStoreItem(_storeItemIndex);
        }
        else
        {
            _storeItemIndex++;
            SetCurrentStoreItem(_storeItemIndex);
        }
    }
    private void MoveToPreviousItem()
    {
        if (_storeItemIndex - 1 < 0)
        {
            _storeItemIndex = _storeItems.Count - 1;
            SetCurrentStoreItem(_storeItemIndex);
        }
        else
        {
            _storeItemIndex--;
            SetCurrentStoreItem(_storeItemIndex);
        }
    }
    private void SetDefaultStoreItem()
    {
        _currentItem = _storeItems[0];
        _storeItemSpriteView.SetSprite(_currentItem.ItemSprite);
    }
    private void SetCurrentStoreItem(int index)
    {
        _currentItem = _storeItems[index];
        _storeItemSpriteView.SetSprite(_currentItem.ItemSprite);
    }
}