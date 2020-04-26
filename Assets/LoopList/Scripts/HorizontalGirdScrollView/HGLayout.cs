using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
namespace LoopList.Scripts.HorizontalGirdScrollView
{
    [System.Serializable]
    public class Padding
    {
        public int top;
        public int left;
        public int right;
        public int bottom;
        public int spacing;
    }
    public class HGLayout:MonoBehaviour
    {
        public enum LayoutType
        {
            FirstVertical,
            FirstHorizontal
        }
        [Header("是否添加模拟数据！！")]
        public bool virtualModel;

        #region Item预制体相关

        [Header("Item预制体")]
        public GameObject ItemPrefab;

        private float prefabWidth, prefabHeight;

        #endregion
            

        public ScrollRect _scroll;
        private RectTransform _scrollRect;
        [HideInInspector]
        public List<ScrollItemData> _itemDataList = new List<ScrollItemData>();
        [Header("----------布局----------")]
        public Padding padding;
        [Header("先择是行优先还是列优先 暂时无用 TODO")]
        public LayoutType type;

        
        private int _startIndex , _endIndex ;
        /// <summary>
        /// 行数   列数
        /// </summary>
        private int rows,cols;
        private RectTransform _content;
        private void Awake()
        {
            RectTransform prefabRect = ItemPrefab.GetComponent<RectTransform>();
            if (prefabRect==null)
                Debug.LogError("ItemPrefab不包含RectTransform组件，请确保ItemPrefab是一个UI的预制体");
            prefabWidth = prefabRect.rect.width;
            prefabHeight = prefabRect.rect.height;
            _content = transform as RectTransform;
            _scrollRect = _scroll.transform as RectTransform;
            rows = Mathf.CeilToInt(_scrollRect.rect.width / prefabWidth) ;
            cols = Mathf.FloorToInt(_scrollRect.rect.height / prefabHeight);
            Debug.Log($"Rows:{rows}  Cols:{cols}");
            //添加模拟数据
            if (virtualModel) GetModel();
            Debug.Log($"DataItem:{_itemDataList.Count}");

        }

        private void Start()
        {
            int num = rows * cols > _itemDataList.Count ? _itemDataList.Count : rows * cols;
            Debug.Log($"数量num:{num}");
            for (int i = 0; i < num; i++)
            {
                CreateItem(_itemDataList[i],i);
            }
            _content.sizeDelta = Vector2.right*(padding.left+padding.right+prefabWidth*rows+(rows-1)*padding.spacing);
            _startIndex = 0;
            _endIndex = num;
            _scroll.onValueChanged.AddListener(ValueChangedCall);
        }

        private void ValueChangedCall(Vector2 arg0)
        {
            if (arg0.x>1)
            {
                Debug.Log(arg0);
                _content.sizeDelta+=Vector2.right*(prefabWidth+padding.spacing);
//                Debug.Log($"XXXXXXXXXXXXXXXXXXXX:{arg0}");
                Debug.Log(_scroll.horizontalScrollbar.value);
            }
        }

        void CreateItem(ScrollItemData data,int i)
        {
            int x = i / cols;
            int y = i % cols;
            Debug.Log($"创建第{i}个对象坐标是:({x},{y})");
            var item = Instantiate(ItemPrefab);
            item.transform.SetParent(_content);
            (item.transform as RectTransform).anchoredPosition3D=new Vector3(padding.left+prefabWidth*x+padding.spacing*x,
                                                                             -(padding.top+prefabHeight*y+padding.spacing*y),
                                                                             0);
            item.transform.localScale = Vector3.one;
            item.GetComponentInChildren<Text>().text = i.ToString();
        }
        /// <summary>
        /// 添加模拟数据
        /// </summary>
        void GetModel()
        {
            for (int i = 0; i < 100; i++)
            {
                _itemDataList.Add(new ScrollItemData(i));
            }
        }
    }
}