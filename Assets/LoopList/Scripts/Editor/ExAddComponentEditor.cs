using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public static class ExAddComponentEditor 
{
    [System.Obsolete]
    [MenuItem("Component/Looplist/HGScrollView")]
    public static void Test(){
        Debug.Log("hello");
        var canvas= GameObject.FindObjectsOfType(typeof(Canvas));
        Debug.Log("画布的数量:"+canvas.Length);
        foreach (var item in canvas)
        {
            Debug.Log(item.name);
        }
        GameObject can;
        if(canvas.Length==0){
            can = new GameObject("Canvas");
            can.transform.position = Vector3.zero;
            can.AddComponent<Canvas>();
            can.AddComponent<CanvasScaler>();
            can.AddComponent<GraphicRaycaster>();
        }else
        {
            can = canvas[0] as GameObject;
        }
        Debug.Log(can);
        GameObject scroll = new GameObject("HGScrollView");
        scroll.transform.SetParent(can.transform);
        RectTransform ScrollRect = scroll.AddComponent<RectTransform>();
        ScrollRect.anchoredPosition3D = Vector3.zero;
        ScrollRect.sizeDelta = new Vector2(640,500);
        ScrollRect.rotation  = Quaternion.Euler(Vector3.zero);
        ScrollRect.localScale = Vector3.one;
        scroll.AddComponent<Image>();
        ScrollRect scroll_rect = scroll.AddComponent<ScrollRect>();
        scroll_rect.horizontal = true;
        scroll_rect.vertical = false;

        GameObject viewport = new GameObject("ViewPort");
        viewport.transform.SetParent(ScrollRect);
        RectTransform viewport_rect = viewport.AddComponent<RectTransform>();
        viewport_rect.rotation = Quaternion.Euler(Vector3.zero);
        viewport_rect.localScale = Vector3.zero;
        viewport_rect.anchorMin = Vector2.zero;
        viewport_rect.anchorMax = Vector2.one;
        viewport_rect.pivot = Vector2.up;
        viewport.AddComponent<Image>();

        GameObject content=new GameObject("Content");
        content.transform.SetParent(viewport_rect);
        RectTransform content_rect = content.AddComponent<RectTransform>();
        content_rect.rotation = Quaternion.Euler(Vector3.zero);
        content_rect.localScale = Vector3.one;
        content_rect.anchorMax = Vector2.up;
        content_rect.anchorMin = Vector2.zero;
        content_rect.pivot = Vector2.up*0.5f;
        content.AddComponent<ScrollR.HGLayout>();

    }
}
