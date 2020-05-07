using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ScrollR{

    public class VGLayout:Layout{
        /// <summary>
        ///          行数   列数
        /// </summary>
        protected int rows,cols;
        public override void Awake() {
            base.Awake();
            rows = Mathf.FloorToInt(_scrollRect.rect.width / prefabWidth);
            cols = Mathf.CeilToInt(_scrollRect.rect.height / prefabHeight);
            Debug.LogWarning($"行数(rows)：{rows}    列数(cols)：{cols}");
        }
        public override void Start(){
            int num = rows * cols > _itemDataList.Count ? _itemDataList.Count : rows * cols;
            for (int i = 0; i < num; i++)
            {
                CreateItem(_itemDataList[i], i);
            }
        }
        public override void CreateItem(ScrollItemData data, int i){
            int x=0;
            int y=0;
            var item=Instantiate(ItemPrefab);
            var mark = item.GetComponent<ItemMark_HG>();
            if(mark==null)mark = item.AddComponent<ItemMark_HG>();
            
        }
    }
}