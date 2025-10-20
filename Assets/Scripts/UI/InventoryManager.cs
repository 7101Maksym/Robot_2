using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
	[SerializeField] private GameObject _inventoryCellTemplate;

	private List<CellData> _items = new List<CellData>();

	[Header("Display Item Data")]
    [SerializeField] private TextMeshProUGUI _itemName;
	[SerializeField] private TextMeshProUGUI _itemDescription;
	[SerializeField] private Image _itemIcon;
	[SerializeField] private Image _itemBackground;
    [Header("Inventory canvas")]
    [SerializeField] private Canvas _myCanvas;

    [SerializeField] private InventoryItem[] _testItem;

	private void Awake()
	{
		_myCanvas.enabled = false;

		HideCells();
        UpdateInventory();
	}

	public void AddItem(InventoryItem item, int count = 1)
	{
		for (int i = 0; i < _items.Count; i++)
		{
			CellData cell = _items[i];

			if (cell.Item.Name == item.Name)
			{
				CellData newCell = new CellData();
				newCell.Item = cell.Item;
				newCell.Count = cell.Count + count;

				_items[i] = newCell;

				UpdateInventory();

				return;
			}
		}

		CreateCell(item, count);

		UpdateInventory();
	}

	public void RemoveItem(InventoryItem item, int count = 1)
	{
		for (int i = 0; i < _items.Count; i++)
		{
			CellData cell = _items[i];

			if (cell.Item.Name == item.Name)
			{
				int newCount = cell.Count -= count;

				if (newCount <= 0)
				{
					_items.RemoveAt(i);
                }
				else
				{
                    CellData newCell = new CellData();
                    newCell.Item = cell.Item;
                    newCell.Count = newCount;

                    _items[i] = newCell;
                }

				UpdateInventory();
				return;
			}
		}

		UpdateInventory();
	}

	private void CreateCell(InventoryItem item, int count = 1)
	{
		CellData cell = new CellData();

		cell.Item = item;

		cell.Count = count;

		_items.Add(cell);
	}

	private void UpdateInventory()
	{
		foreach (var item in transform.GetComponentsInChildren<InventoryCell>())
		{
			Destroy(item.gameObject);
		}

		foreach (var cell in _items)
		{
			GameObject newCell = Instantiate(_inventoryCellTemplate, transform);

			newCell.GetComponent<InventoryCell>().SetItem(cell.Item);
			newCell.GetComponent<InventoryCell>().ChangeItemCount(cell.Count);

			newCell.GetComponent<InventoryCell>().OnDisplayCell.AddListener(DisplayData);
		}
	}

	private void HideCells()
	{
        InventoryCell[] cells = transform.GetComponentsInChildren<InventoryCell>();

        foreach (var c in cells)
        {
            c.HideCell();
        }
    }

	private void DisplayData(InventoryCell cell)
	{
		HideCells();

        _itemName.enabled = true;
		_itemDescription.enabled = true;
		_itemIcon.enabled = true;
        _itemBackground.enabled = true;

        _itemName.text = cell.GetItem().Name;
		_itemDescription.text = cell.GetItem().Description;
		_itemIcon.sprite = cell.GetItem().Icon;
	}

	public void OpenOrClaseInventory(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			if (!_myCanvas.enabled)
			{
				_itemName.enabled = false;
				_itemDescription.enabled = false;
				_itemIcon.enabled = false;
				_itemBackground.enabled = false;

                HideCells();

                _myCanvas.enabled = true;
			}
			else
			{
                _myCanvas.enabled = false;
            }
		}
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem(_testItem[0], 1);
        }

		if (Input.GetKeyDown(KeyCode.O))
        {
            AddItem(_testItem[1], 1);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AddItem(_testItem[2], 1);
        }
    }
}
