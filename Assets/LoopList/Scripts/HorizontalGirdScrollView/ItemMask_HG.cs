using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ItemMask_HG : MonoBehaviour {
    private ScrollItemData data;
    private int _index;
    public int Index { get => _index; }
    public ScrollItemData Data { get => data; set { data = value; Init(); } }
    public RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    public virtual void Init()
    {
        _index = data.id;
        transform.GetComponentInChildren<Text>().text = _index.ToString();
    }
}