using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
namespace ScrollR{
    [System.Serializable]
    public class myEvent :UnityEvent<RectTransform,ScrollItemData>{}
    public class HGridLayout:HGLayout{

       public override void Awake()
        {
            base.Awake();
            // inititemCall.AddListener(InitItem);
        }
        public void OnDestroy() {
            inititemCall.RemoveAllListeners();
        }
        public void InitItem(RectTransform rect,ScrollItemData data){
            Debug.Log("rect:"+rect);
            Debug.Log("Data:"+data.id);
            rect.GetComponentInChildren<Text>().text = data.id.ToString();
        }
    }
}
