using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    static Constants instance;

    [SerializeField] public float conveyorSpeed = 0.25f;

    private void Awake()
    {
        instance = this;
    }

    public static Constants GetInstance()
    {
        return instance;
    }
}

