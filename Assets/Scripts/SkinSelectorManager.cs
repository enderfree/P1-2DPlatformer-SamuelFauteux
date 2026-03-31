using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkinSelectorManager : MonoBehaviour
{
    private InputSystem_Actions inputAction;

    private void Awake()
    {
        inputAction = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputAction.UI.Inventory.Enable();
        inputAction.UI.Inventory.performed += OnInventoryPerformed;
    }

    private void OnDisable()
    {
        inputAction.UI.Inventory.performed -= OnInventoryPerformed;
        inputAction.UI.Inventory.Disable();
    }

    private void OnInventoryPerformed(InputAction.CallbackContext context)
    {

    }
}
