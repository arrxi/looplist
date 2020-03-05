using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMark : MonoBehaviour
{
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
        title = transform.Find("Text").GetComponent<Text>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}

public static class Extension_List
{
    public static T Pop<T>(this List<T> list, int index = 0)
    {
        T t = list[0];
        list.RemoveAt(0);
        return t;
    }
}