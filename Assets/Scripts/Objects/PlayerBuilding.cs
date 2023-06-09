using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerBuilding : MonoBehaviour
{
    private Entity e;
    public GameObject copy;
    [ReadOnly] public PlaceableObject placeableObject;
    [ReadOnly] public MaterialSwapper copyMat;
    [ReadOnly] public GridAlignment gridAlignment;

    private PlayerObjectController objectController;

    // Precision Movement
    [Header("Precision Movement")]
    [SerializeField] private float precisionSpeed = 5f;
    private bool precision = false;
    private Vector2 stickInput;

    private void Start()
    {
        objectController = GetComponent<PlayerObjectController>();
    }

    public void Update()
    {
        // Align clone position to a grid position
        if (e == null)
            return;

        if (precision)
        {
            Vector3 movementDirection = new Vector3(stickInput.x, 0, stickInput.y);
            movementDirection.Normalize();

            objectController.precisionHold.transform.Translate(movementDirection * precisionSpeed * Time.deltaTime, Space.World);
        }

        copy.transform.localPosition = Vector3.zero;
        copy.transform.rotation = Quaternion.Euler(0, e.rotation, 0);

        copy.transform.position = BuildingSystem.instance.SnapEntityCoordinateToGrid(copy.transform.position, gridAlignment);

        // Check for collisions
        if (BuildingSystem.instance.CanBePlaced(copy.GetComponent<PlaceableObject>()))
        {
            copyMat.Set(1);
        } else
        {
            copyMat.Set(0);
        }
        

        //objectController.objectHold.transform.GetChild(0).transform.localPosition = Vector3.zero;
        //objectController.objectHold.transform.GetChild(0).position = BuildingSystem.instance.SnapCoordinateToGrid(objectController.objectHold.transform.GetChild(0).position);


    }

    public void Precision(InputAction.CallbackContext c)
    {
        if (c.action.name != "Precision Movement")
            return;

        if (copy == null)
            return;
        if (c.phase == InputActionPhase.Performed)
        {
            SetPrecision(true, true);
        } else if (c.phase == InputActionPhase.Canceled)
        {
            SetPrecision(false, true);
        }
    }

    private void SetPrecision(bool b, bool copyExists)
    {
        precision = b;
        if (precision)
            copy.transform.SetParent(objectController.precisionHold.transform);
        else
        {
            objectController.precisionHold.transform.position = objectController.objectHold.transform.position;
            if (copyExists)
                copy.transform.SetParent(objectController.objectHold.transform);
        }
    }

    public void OnMove(InputAction.CallbackContext c) => stickInput = c.ReadValue<Vector2>();

    public void Place()
    {
        // Allign again
        e.transform.position = BuildingSystem.instance.SnapEntityCoordinateToGrid(precision ? objectController.precisionHold.transform.position : e.transform.position, gridAlignment);

        // Check if position is available
        if (!BuildingSystem.instance.CanBePlaced(copy.GetComponent<PlaceableObject>()))
        {
            return;
        }
        // Place & Occupy grid spot
        e.gameObject.SetActive(true);
        e.transform.SetParent(null);
        e.transform.rotation = copy.transform.rotation;

        Destroy(copy);

        if (e.placementHandler != null)
            e.placementHandler.handle(e);

        e = null;
        copy = null;
        placeableObject = null;
        copyMat = null;
        gridAlignment = null;

        // Handle Precision
        objectController.heldEntity = null;
        SetPrecision(false, false);
    }

    public void HandleEntity(Entity e)
    {
        this.e = e;

        copy = Instantiate(e.gameObject, objectController.objectHold.transform);
        Destroy(copy.GetComponent<Entity>());

        placeableObject = copy.AddComponent<PlaceableObject>();
        copyMat = copy.AddComponent<MaterialSwapper>();
        gridAlignment = copy.GetComponent<GridAlignment>();

        copyMat.materials = new Material[] { Resources.Load("Material/Red", typeof(Material)) as Material, Resources.Load("Material/Green", typeof(Material)) as Material };

        e.gameObject.SetActive(false);
    }

    public GameObject CreateCopy(Entity e, Vector3 rotation)
    {
        GameObject copy = null;
        copy = Instantiate(e.gameObject);
        Destroy(copy.GetComponent<Entity>());

        copyMat = copy.AddComponent<MaterialSwapper>();
        copyMat.materials = new Material[] { Resources.Load("Material/Red", typeof(Material)) as Material, Resources.Load("Material/Green", typeof(Material)) as Material };

        copy.SetActive(false);

        copy.transform.rotation = Quaternion.Euler(rotation);

        return copy;
    }

    public void HandleRotate()
    {
        if (placeableObject != null)
        {
            placeableObject.Rotate();
        }
    }

}
