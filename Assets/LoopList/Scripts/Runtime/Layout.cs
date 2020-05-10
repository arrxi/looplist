using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ScrollR
{
    public abstract class Layout : MonoBehaviour
    {
        // public UnityAction<RectTransform,ScrollItemData> inititemCall;
        [Header("用于初始化Item组件的事件：")]
        public InitItemCall inititemCall = new InitItemCall();

        [System.Serializable]
        public class Padding
        {
            public int top;
            public int left;
            public int right;
            public int bottom;
            public int spacing;
        }

        protected int _startIndex, _endIndex;

        /// <summary>
        /// 是否添加模拟数据
        /// </summary>
        [Header("是否添加模拟数据！！")]
        public bool virtualModel;

        #region Item预制体相关

        /// <summary>
        /// Item预制体
        /// </summary>
        [Header("Item预制体")]
        public GameObject ItemPrefab;

        /// <summary>
        /// 预制体的长 宽
        /// </summary>
        protected float prefabWidth, prefabHeight;

        #endregion Item预制体相关

        /// <summary>
        /// content组件
        /// </summary>
        protected RectTransform _content;

        /// <summary>
        /// ScrollRect组件
        /// </summary>
        public ScrollRect _scroll;

        /// <summary>
        /// ScrollRect组件的transform
        /// </summary>
        protected RectTransform _scrollRect;

        /// <summary>
        /// 数据
        /// </summary>
        [HideInInspector]
        public List<ScrollItemData> _itemDataList = new List<ScrollItemData>();

        /// <summary>
        /// 布局
        /// </summary>
        [Header("----------布局----------")]
        public Padding padding;

        /// <summary>
        /// Item的索引
        /// </summary>
        protected List<ItemMark_HG> items = new List<ItemMark_HG>();

        protected bool isInit;

        public virtual void Awake()
        {
            isInit = false;
            RectTransform prefabRect = ItemPrefab.GetComponent<RectTransform>();
            if (prefabRect == null)
                Debug.LogError("ItemPrefab不包含RectTransform组件，请确保ItemPrefab是一个UI的预制体");

            prefabWidth = prefabRect.rect.width;
            prefabHeight = prefabRect.rect.height;
            _content = transform as RectTransform;
            _scrollRect = _scroll.transform as RectTransform;
            //添加模拟数据
            if (virtualModel) GetModel();
        }

        public virtual void Start()
        {
        }

        public virtual void ValueChangedCall(Vector2 arg0)
        {
        }

        public virtual void Start2End()
        {
        }

        public virtual void End2Start()
        {
        }

        public virtual void CreateItem(ScrollItemData data, int index)
        {
        }

        /// <summary>
        /// 添加模拟数据
        /// </summary>
        public virtual void GetModel()
        {
            for (int i = 0; i < 100; i++)
            {
                _itemDataList.Add(new ScrollItemData(i));
            }
        }

        /// <summary>
        /// 添加元素到末尾
        /// </summary>
        /// <param name="data"></param>
        public virtual void AddData(ScrollItemData data)
        {
            _itemDataList.Add(data);
        }

        /// <summary>
        /// 插入一个数据 如果索引为负，则添加到列表末尾
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        public virtual void AddDataAt(ScrollItemData data, int index = -1)
        {
            if (index >= _itemDataList.Count)
            {
                Debug.LogError("索引超出范围");
                return;
            }
            _itemDataList.Insert(index < 0 ? _itemDataList.Count - 1 : index, data);
        }
    }
}