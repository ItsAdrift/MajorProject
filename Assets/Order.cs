using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;

    [Header("Values")]
    [SerializeField] public ItemType type;

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = type.render;
        text.text = type.name;
    }
}
