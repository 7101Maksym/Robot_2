using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _background;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _selectedSprite;

    public UnityEvent<InventoryCell> OnDisplayCell;

    private InventoryItem MyItem;
    private int _count = 0;

    private void Awake()
    {
        _countText.text = _count.ToString();
    }

    public void SetItem(InventoryItem item)
    {
        MyItem = item;

        _image.sprite = item.Icon;
    }

    public int ChangeItemCount(int count = 0)
    {
        _count += count;
        _countText.text = _count.ToString();

        return _count;
    }

    public InventoryItem GetItem()
    {
        return MyItem;
    }

    public void DisplayCell()
    {
        OnDisplayCell?.Invoke(this);

        _background.sprite = _selectedSprite;
    }

    public void HideCell()
    {
        _background.sprite = _defaultSprite;
    }

    private void OnDestroy()
    {
        OnDisplayCell.RemoveAllListeners();
    }
}
