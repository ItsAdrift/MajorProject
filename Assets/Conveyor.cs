using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static DirectionStorage;
using static Constants;

public class Conveyor : MonoBehaviour
{
    [SerializeField] public Direction direction;
    List<Rigidbody> objects = new List<Rigidbody>();

    Constants c;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            // new Vector3(0, 0.1f, 0) + (Get(direction) * Constants.GetInstance().conveyorSpeed)
            objects[i].GetComponent<Rigidbody>().velocity = (Get(direction) * Constants.Get().conveyorSpeed);
        }
    }

    private void Start()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            objects.Add(other.gameObject.GetComponent<Rigidbody>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            objects.Remove(other.gameObject.GetComponent<Rigidbody>());
            other.gameObject.GetComponent<Rigidbody>().AddForce(Get(direction) * Constants.Get().conveyorSpeed);
        }
    }
}
