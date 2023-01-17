using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerScript : CombatBody
{
    private PlayerInput inputComponent;
    private Vector3 inputVector = Vector3.zero;
    private InputAction movementInput;
    private Camera cam;

    new public void Release()
    {
        inputComponent.enabled = true;
        cam = transform.GetChild(1).GetComponent<Camera>();
    }

    new private void Start()
    {
        base.Start();
        team = "player";
        abilityList.Add(new CreateSphere(gameObject));
        abilityList.Add(new LockedAbility(gameObject));
    }

    new private void Update()
    {
        base.Update();

        inputVector = new Vector3(movementInput.ReadValue<Vector2>().x, 0, movementInput.ReadValue<Vector2>().y);
        rb.velocity = inputVector * moveSpeed;

        if (!inputComponent.enabled)
        {
            if (released)
            {
                inputComponent.enabled = true;
            }
        }
    }

    private void OnEnable()
    {
        inputComponent = GetComponent<PlayerInput>();
        movementInput = inputComponent.actions.FindAction("Move");
        movementInput.Enable();
    }

    private void OnDisable()
    {
        movementInput.Disable();
    }

    void OnFirstAbility()
    {
        abilityList[0].Use(Vector3.zero);
    }

    void OnSecondAbility()
    {
        abilityList[1].Use(transform.position);
    }
}
