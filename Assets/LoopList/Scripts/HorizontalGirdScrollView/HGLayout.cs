using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace ScrollR {

    public class HGLayout : Layout {
        protected int _startIndex, _endIndex;

        /// <summary>
        /// 行数   列数
        /// </summary>
        protected int rows, cols;

        private void Awake()
        {
            RectTransform prefabRect = ItemPrefab.GetComponent<RectTransform>();
            if (prefabRect == null)
                Debug.LogError("ItemPrefab不包含RectTransform组件，请确保ItemPrefab是一个UI的预制体");

            prefabWidth = prefabRect.rect.width;
            prefabHeight = prefabRect.rect.height;
            _content = transform as RectTransform;
            _scrollRect = _scroll.transform as RectTransform;
            rows = Mathf.CeilToInt(_scrollRect.rect.width / prefabWidth);
            cols = Mathf.FloorToInt(_scrollRect.rect.height / prefabHeight);
            //添加模拟数据
            if (virtualModel) GetModel();
        }

        private void Start()
        {
            int num = rows * cols > _itemDataList.Count ? _itemDataList.Count : rows * cols;
            Debug.Log($"数量num:{num}");
            for (int i = 0; i < num; i++)
            {
                CreateItem(_itemDataList[i], i);
            }
            _content.sizeDelta = Vector2.right * (padding.left + padding.right + prefabWidth * rows + (rows - 1) * padding.spacing);
            _startIndex = 0;
            _endIndex = num - 1;
            _scroll.onValueChanged.AddListener(ValueChangedCall);
        }

        private void ValueChangedCall(Vector2 arg0)
        {
            if (_scroll.horizontalScrollbar.value > 1)
            {
                for (int i = 0; i < cols; i++)
                {
                    Start2End();
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
        public void Start2End()
        {
            if (_endIndex >= _itemDataList.Count - 1)
            {
                return;
            }
            ItemMask_HG mark = items.Pop();
            float x = 0;
            float y = 0;
            if (_endIndex % 2 != 0)
            {
                x = items.lastData()._rect.anchoredPosition.x + prefabWidth + padding.spacing;
                y = -padding.top;
                _content.sizeDelta += Vector2.right * (prefabWidth + padding.spacing);
            }
            else
            {
                x = items.lastData()._rect.anchoredPosition.x;
                y = -(padding.top + padding.spacing + prefabHeight);
            }
            mark._rect.anchoredPosition3D = new Vector3(x, y, 0);
            mark.Data = _itemDataList[_endIndex + 1];
            items.Add(mark);
            _startIndex++;
            _endIndex++;
        }

        /// <summary>
        /// 把末端的Item移动到前面
        /// </summary>
        public void End2Start()
        {
            if (_startIndex <= 0)
            {
                return;
            }
            ItemMask_HG mask = items.PopLast();
            float x = 0, y = 0;
            if (_startIndex % 2 != 0)
            {
                x = items[0]._rect.anchoredPosition.x;
                y = -padding.top;
            }
            else
            {
                _content.sizeDelta -= Vector2.right * (prefabWidth + padding.spacing);
                x = items[0]._rect.anchoredPosition.x - prefabWidth - padding.spacing;
                y = -(padding.top + padding.spacing + prefabHeight);
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
        /// <param name="data"></param>
        /// <param name="i"></param>
        private void CreateItem(ScrollItemData data, int i)
        {
            int x = i / cols;
            int y = i % cols;
            Debug.Log($"创建第{i}个对象坐标是:({x},{y})");
            var item = Instantiate(ItemPrefab);

            var mask = item.GetComponent<ItemMask_HG>();
            if (mask == null)
            {
                mask = item.AddComponent<ItemMask_HG>();
            }
            mask.Data = data;
            item.transform.SetParent(_content);
            (item.transform as RectTransform).anchoredPosition3D = new Vector3(padding.left + prefabWidth * x + padding.spacing * x,
                                                                             -(padding.top + prefabHeight * y + padding.spacing * y),
                                                                             0);
            item.transform.localScale = Vector3.one;
            items.Add(mask);
        }

        /// <summary>
        /// 添加模拟数据
        /// </summary>
        private void GetModel()
        {
            for (int i = 0; i < 100; i++)
            {
                _itemDataList.Add(new ScrollItemData(i));
            }
        }
    }
}