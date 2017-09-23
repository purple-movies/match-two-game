using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileReference : MonoBehaviour
{
    public TileNode node;
    private Text text;

    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    public void showValue()
    {
        // TODO: We can change this to show images or something rather than the int value. 

        text.text = node.value.ToString();
    }

    internal void hideValue()
    {
        text.text = "?";
    }

    //void Start()
    //{
    //    GetComponent<Button>().onClick.AddListener(onClick);
    //}

    //private void onClick()
    //{
    //    GetComponentInChildren<Text>().text = node.value.ToString();
    //}
}
