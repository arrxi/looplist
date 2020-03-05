using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollItemData
{
    ///
    public int id;

    public ScrollItemData(int id)
    {
        this.id = id;
    }
}

public class MEVerticalLayout : MonoBehaviour
{
    public ScrollRect _scroll;
    public GameObject ItemPrefab;
    public bool isRealWidth = false;
    public List<ScrollItemData> _data = new List<ScrollItemData>();
    private int _startIndex = 0, _endIndex = 0;
    private float _itemHeight;
    private int _itemNum;
    private RectTransform _content;
    private RectTransform viewPort;
    private float viewPortHeight;
    private ItemMark _startItem;
    private ItemMark _endItem;
    private List<ItemMark> items = new List<ItemMark>();

    #region Padding

    public int spacing = 0;
    public int top = 0;
    public int bottom = 0;
    public int left = 0;
    public int right = 0;

    #endregion Padding

    // Start is called before the first frame update
    private void Start()
    {
        GetModel();
        _scroll.onValueChanged.AddListener(this.OnValueChange);
        _itemHeight = ItemPrefab.GetComponent<RectTransform>().rect.height;
        _content = transform as RectTransform;
        viewPort = _content.parent as RectTransform;
        viewPortHeight = viewPort.rect.height;
        GetItemNum();
        Debug.Log(string.Format("item的个数：{0}", _itemNum));
        InitContentSize();
        Debug.Log(string.Format("content的高度：{0}", _content.rect.height));
        SpwanItem();
        Debug.Log(string.Format("content的宽度：{0}", _content.rect.width));
    }

    /// <summary>
    /// 创建虚拟的数据模型
    /// </summary>
    private void GetModel()
    {
        for (int i = 0; i < 50; i++)
        {
            _data.Add(new ScrollItemData(i));
        }
    }

    /// <summary>
    /// 计算用于显示的item的数量
    /// </summary>
    private void GetItemNum()
    {
        _itemNum = Mathf.CeilToInt((_content.rect.height + spacing) / (_itemHeight + spacing)) + 1;
    }

    private void InitContentSize()
    {
        float height = 0;
        float realLength = _itemNum > _data.Count - 1 ? _data.Count - 1 : _itemNum;
        height = _itemHeight * realLength + spacing * (_itemNum - 1);
        _content.sizeDelta = new Vector2(0, height);
    }

    private void SpwanItem()
    {
        int dataLength = _data.Count;
        if (dataLength <= 0)
        {
            Debug.LogWarning(string.Format("data数据块儿为空！！！！！！！！！"));
            return;
        }

        if (dataLength >= _itemNum)
        {
            for (int i = 0; i < _itemNum; i++)
            {
                ItemMark tmp = CreateItem(i);
                if (tmp != null)
                {
                    items.Add(tmp);
                }
            }
        }
        else
        {
            for (int i = 0; i < dataLength; i++)
            {
                ItemMark tmp = CreateItem(i);
                if (tmp != null)
                {
                    items.Add(tmp);
                }
            }
        }
        _startItem = items[0];
        _endItem = items[items.Count - 1];
    }

    private ItemMark CreateItem(int i)
    {
        _endIndex++;
        if (_endIndex >= _data.Count)
        {
            Debug.LogWarning("createItemError");
            return null;
        }
        //Item _aitem = new Item(_content);

        #region 生成物体并设置位置

        GameObject item = Instantiate(ItemPrefab, _content);
        item.name = i.ToString();
        RectTransform itemRect = item.GetComponent<RectTransform>();
        itemRect.localRotation = Quaternion.Euler(Vector3.zero);
        itemRect.localScale = Vector3.one;
        if (isRealWidth)
            itemRect.sizeDelta = new Vector2(_content.rect.width, itemRect.sizeDelta.y);
        itemRect.anchorMax = new Vector2(0.5f, 1);
        itemRect.anchorMin = new Vector2(0.5f, 1);
        itemRect.pivot = new Vector2(0.5f, 1);
        float y = i * (_itemHeight + spacing);
        itemRect.anchoredPosition = Vector3.up * -y;

        ItemMark mark = item.AddComponent<ItemMark>();
        mark.Data = _data[i];

        #endregion 生成物体并设置位置

        return mark;
    }

    private void OnValueChange(Vector2 data)
    {
        //上滑到顶了
        float _contentY = _content.anchoredPosition.y;
        float _startItemY = _startItem._rect.anchoredPosition.y;
        float _endItemY = _endItem._rect.anchoredPosition.y;
        Debug.LogWarning(_contentY + _startItemY);
        if ((_contentY + _startItemY) < 0)//划到顶了
        {
            int tmp_index = _startItem.index - 1;
            endToStart(tmp_index);
        }
        bool isbottom = Mathf.Abs(_endItemY + _contentY) + _itemHeight < viewPortHeight;
        if (isbottom)//滑到低了
        {
            int tmp_index = _endItem.index + 1;
            startToEnd(tmp_index);
        }
        /////////////////////////
    }

    private void startToEnd(int index)
    {
        if (index >= _data.Count)
        {
            return;
        }
        Vector2 pos = _endItem._rect.anchoredPosition - Vector2.up * (spacing + _itemHeight);
        _startItem._rect.anchoredPosition = pos;
        _content.sizeDelta += Vector2.up * (spacing + _itemHeight);
        _startItem.Data = _data[index];

        _endItem = _startItem;

        bool rmOK = items.Remove(_startItem);
        items.Add(_endItem);

        _startItem = items[0];
    }

    private void endToStart(int index)
    {
        if (index < 0)
            return;

        _content.sizeDelta -= Vector2.up * (spacing + _itemHeight);
        Vector2 pos = _startItem._rect.anchoredPosition + Vector2.up * (spacing + _itemHeight);

        _endItem._rect.anchoredPosition = pos;
        _endItem.Data = _data[index];
        _startItem = _endItem;
        bool rmOK = items.Remove(_endItem);
        items.Insert(0, _startItem);
        _endItem = items[items.Count - 1];
    }

    // Update is called once per frame
    private void Update()
    {
    }
}