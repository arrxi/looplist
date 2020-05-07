using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace ScrollR {

    public abstract class HGLayout : Layout {


        /// <summary>
        /// 行数   列数
        /// </summary>
        protected int rows, cols;

        public override void Awake()
        {
            base.Awake();
            rows = Mathf.CeilToInt(_scrollRect.rect.width / prefabWidth);
            cols = Mathf.FloorToInt(_scrollRect.rect.height / prefabHeight);
            Debug.LogWarning($"行数(rows)：{rows}    列数(cols)：{cols}");
        }

        public override void Start()
        {
            int num = rows * cols > _itemDataList.Count ? _itemDataList.Count : rows * cols;
            for (int i = 0; i < num; i++)
            {
                CreateItem(_itemDataList[i], i);
            }
            _content.sizeDelta = Vector2.right * (padding.left + padding.right + prefabWidth * rows + (rows - 1) * padding.spacing);
            _startIndex = 0;
            _endIndex = num - 1;
            Debug.LogWarning($"_startIndex:{_startIndex}\t_endIndex:{_endIndex}");
            _scroll.onValueChanged.AddListener(ValueChangedCall);
        }

        public override void ValueChangedCall(Vector2 arg0)
        {
            if (_scroll.horizontalScrollbar.value > 1)
            {
                for (int i = 0; i < cols; i++)
                {
                    Start2End();
                }
            }
            else
            {
                float lastItemPos = _content.anchoredPosition3D.x + items[0]._rect.anchoredPosition3D.x;
                if (lastItemPos >= 0)
                {
                    for (int i = 0; i < cols; i++)
                    {
                        End2Start();
                    }
                }
            }
        }

        /// <summary>
        /// 把前端的Item移动到末端
        /// </summary>
        public override void Start2End()
        {
            if (_endIndex >= _itemDataList.Count - 1)
            {
                return;
            }
            _startIndex++;
            _endIndex++;
            ItemMark_HG mark = items.Pop();
            float x = 0;
            float y = 0;
            int c = _endIndex%cols;
            if (c ==0)//刚好是整数倍
            {
                x = items.lastData()._rect.anchoredPosition.x + prefabWidth + padding.spacing;
                y = -padding.top;
                _content.sizeDelta += Vector2.right * (prefabWidth + padding.spacing);
            }
            else
            {

                x = items.lastData()._rect.anchoredPosition.x;
                y = -(padding.top + padding.spacing*c + prefabHeight*c);
            }
            mark._rect.anchoredPosition3D = new Vector3(x, y, 0);
            mark.Data = _itemDataList[_endIndex];
            items.Add(mark);
   
        }

        /// <summary>
        /// 把末端的Item移动到前面
        /// </summary>
        public override void End2Start()
        {
            if (_startIndex <= 0)
            {
                return;
            }

            ItemMark_HG mask = items.PopLast();
            float x = 0, y = 0;
            int c = _startIndex%cols;
            if (c !=0)
            {
                x = items[0]._rect.anchoredPosition.x;
                y = -(padding.top+(padding.spacing+prefabHeight)*(c-1));
            }
            else//如果需要换列
            {
                _content.sizeDelta -= Vector2.right * (prefabWidth + padding.spacing);
                x = items[0]._rect.anchoredPosition.x - prefabWidth - padding.spacing;
                y = -(padding.top + (padding.spacing+prefabHeight)*(cols-1)  );
            }
            mask._rect.anchoredPosition3D = new Vector3(x, y, 0);
            mask.Data = _itemDataList[_startIndex - 1];
            items.Insert(0, mask);
            _startIndex--;
            _endIndex--;
        }

        /// <summary>
        /// clone一个Item
        /// </summary>
        /// <param name="data">传入的数据</param>
        /// <param name="index">传入数据的索引</param>
        public override void CreateItem(ScrollItemData data, int index)
        {
            int x = index / cols;
            int y = index % cols;
            var item = Instantiate(ItemPrefab);

            var mask = item.GetComponent<ItemMark_HG>();
            if (mask == null)
            {
                mask = item.AddComponent<ItemMark_HG>();
            }
            mask.initItemCall = inititemCall;//.AddSingleListener(inititemCall);
            mask.Data = data;
            item.transform.SetParent(_content);
            (item.transform as RectTransform).anchoredPosition3D = new Vector3(padding.left + prefabWidth * x + padding.spacing * x,
                                                                             -(padding.top + prefabHeight * y + padding.spacing * y),
                                                                             0);
            item.transform.localScale = Vector3.one;
            items.Add(mask);
        }


    }
}