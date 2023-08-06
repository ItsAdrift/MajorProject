using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    private Vector2 movementInput;

    [SerializeField] private bool snapRotation = false;

    CharacterController controller;

    private bool precision = false;

    void Awake()
    {
        Debug.Log("Awaken");

        controller = GetComponent<CharacterController>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 1)
            return;

        CameraControl.Instance.m_Targets.Add(transform);
    }

        void Update()
    {
        if (precision)
        {
            return;
        }

        Vector3 movementDirection = new Vector3(movementInput.x, 0, movementInput.y);
        movementDirection.Normalize();

        //transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

        controller.Move(movementDirection * speed * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            if (snapRotation)
            {
                transform.rotation = toRotation;
                return;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void OnMove(InputAction.CallbackContext c) => movementInput = c.ReadValue<Vector2>();

    public void OnPrecision(InputAction.CallbackContext c)
    {
        if (c.action.name != "Precision Movement")
            return;

        if (c.phase == InputActionPhase.Performed)
        {
            precision = true;
        } else if (c.phase == InputActionPhase.Canceled)
        {
            precision = false;
        }
    }

    public void DebugMSG(string msg)
    {
        Debug.Log(msg);
    }
}
