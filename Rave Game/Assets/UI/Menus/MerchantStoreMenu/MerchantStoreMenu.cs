using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MerchantStoreMenu : PaintMenu<MerchantPaintCellPanel>
{
    [SerializeField]
    private StoreItemPanel _storeItemBuyPanel;
    [SerializeField]
    private Button _buyCurrentItemButton;
    [SerializeField]
    private int _combinationPrice;
    [SerializeField]
    private float _priceIfAnimalIsNotAvailable;
    [SerializeField]
    private int _totalMoney;
    [SerializeField]
    private CanAffortImage _canAffortImage;
    private new void Awake()
    {
        base.Awake();
        _mediator.Subscribe<StoreItemSendCommand>(_storeItemBuyPanel.Initialize);
    }
    private new void Start()
    {
        base.Start();
        foreach (MerchantPaintCellPanel merchantPaintCellPanel in _paintPanels)
        {
            merchantPaintCellPanel.ChangePaintCount += UpdateTotalMoney;
            merchantPaintCellPanel.InitializePaint(_combinationPrice, _priceIfAnimalIsNotAvailable);
        }
    }
    private new void OnEnable()
    {
        base.OnEnable();
        _buyCurrentItemButton.onClick.AddListener(BuyCurrentItem);
        _storeItemBuyPanel.OnEnable();
        _storeItemBuyPanel.BuyItem += UpdateTotalMoney;
    }
    private new void OnDisable()
    {
        base.OnDisable();
        _buyCurrentItemButton.onClick.RemoveListener(BuyCurrentItem);
        _storeItemBuyPanel.OnDisable();
        foreach (MerchantPaintCellPanel merchantPaintCellPanel in _paintPanels)
            merchantPaintCellPanel.ChangePaintCount -= UpdateTotalMoney;
        _storeItemBuyPanel.BuyItem -= UpdateTotalMoney;
    }
    private void BuyCurrentItem()
    {
        _storeItemBuyPanel.BuyCurrentItem(_paintPanels, ref _totalMoney);
    }
    private void UpdateTotalMoney()
    {
        _totalMoney = 0;
        foreach (MerchantPaintCellPanel merchantPaintCellPanel in _paintPanels)
            _totalMoney += merchantPaintCellPanel.GetCount();
        _canAffortImage.ChangeAffortColor(_totalMoney, _storeItemBuyPanel.GetCurrentItemPrice());
    }
}