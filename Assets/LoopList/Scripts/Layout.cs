using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ScrollR {

    public class Layout : UnityEngine.MonoBehaviour {

        [System.Serializable]
        public class Padding {
            public int top;
            public int left;
            public int right;
            public int bottom;
            public int spacing;
        }

        public enum LayoutType {
            FirstVertical,
            FirstHorizontal,
        }

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

        [Header("先择是行优先还是列优先 暂时无用 TODO")]
        public LayoutType type;

        /// <summary>
        /// Item的索引
        /// </summary>
        protected List<ItemMask_HG> items = new List<ItemMask_HG>();
    }
}