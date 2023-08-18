using System;
using UnityEngine;

public class InputManager
{
    public event Action<GameObject> OnClick;

    private readonly LayerMask _layer;

    public InputManager()
    {
        _layer = LayerMask.GetMask("cell");
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnClick?.Invoke(GetPickedObject());
    }

    private GameObject GetPickedObject()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    
        GameObject clickedObject = null;

        var hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, _layer);
        
        if(hit.collider)
            clickedObject = hit.collider.gameObject;
    
        return clickedObject;
    }
}