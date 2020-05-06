using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class waitS : MonoBehaviour {
    public GameObject _scroll;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(waits());
    }

    private IEnumerator waits()
    {
        yield return new WaitForSeconds(5);
        _scroll.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}