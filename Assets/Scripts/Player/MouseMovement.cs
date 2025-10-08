using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseMovement : MonoBehaviour
{
    public Tilemap map;
    MouseInput mouseInput;
    private Vector3 destination;
    [SerializeField] private float movementSpeed = 5f;

    void Awake()
    {
        mouseInput = new MouseInput();
    }

    void OnEnable()
    {
        mouseInput.Enable();
    }

    void OnDisable()
    {
        mouseInput.Disable();
    }


    void Start()
    {
        destination = transform.position;
        mouseInput.Mouse.MouseClic.performed += _ => MouseClic();
    }

    void MouseClic()
    {
        Vector2 mousePosition = mouseInput.Mouse.MousePosition.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);
        if (map.HasTile(gridPosition))
        {
            destination = mousePosition;
        }
        //Debug.Log("Clic");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, destination) > 0.1f)
            transform.position = Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime);
    }
}
