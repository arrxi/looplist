using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace ScrollR{

    [Serializable]public class InitItemCall:UnityEvent<RectTransform,ScrollItemData>{}
    public class ItemMark_HG : MonoBehaviour {
        private ScrollItemData data;
        private int _index;
        public int Index { get => _index; }
        public ScrollItemData Data { get => data; set { data = value; Init(); } }
        public RectTransform _rect;
        public InitItemCall initItemCall=new InitItemCall();
        public void AddSingleListener(UnityAction<RectTransform,ScrollItemData> callback){
            if(callback==null)return;
            initItemCall.RemoveAllListeners();
            initItemCall.AddListener(callback);
        }

        public virtual void Init()
        {
            if(_rect==null)_rect = GetComponent<RectTransform>();
            _index = data.id;
            initItemCall.Invoke(_rect,data);
        }
    }
}
