using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMark : MonoBehaviour {
    public int index = -1;
    private ScrollItemData data;
    public RectTransform _rect;
    private Text title;

    public ScrollItemData Data { get => data; set { data = value; _init(); } }

    private void _init()
    {
        title.text = data.id.ToString();
        index = data.id;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        title = transform.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}