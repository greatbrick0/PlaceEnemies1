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
    [SerializeField]
    private Camera cam;
    private RaycastHit hitData;
    private Ray mouseRay;

    public LayerMask layerMask;

    new public void Release()
    {
        base.Release();

        inputComponent.enabled = true;
        cam = transform.GetChild(1).GetComponent<Camera>();
    }

    new private void Start()
    {
        base.Start();

        team = "player";
        abilityList[0] = new MagicArrowAbility(gameObject);
        abilityList.Add(new CreateSphere(gameObject));
        abilityList.Add(new LockedAbility(gameObject));
    }

    new private void Update()
    {
        base.Update();

        inputVector = new Vector3(movementInput.ReadValue<Vector2>().x, 0, movementInput.ReadValue<Vector2>().y);
        rb.velocity = inputVector * moveSpeed;

        mouseRay = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(Physics.Raycast(cam.transform.position, mouseRay.direction, out hitData, 80.0f, 1 << 9))
        {
            transform.LookAt(hitData.point);
            //Debug.DrawRay(hitData.point, Vector3.down * 10, Color.white);
        }
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }

    private void OnEnable()
    {
        cam = Camera.main;
        inputComponent = GetComponent<PlayerInput>();
        movementInput = inputComponent.actions.FindAction("Move");
        movementInput.Enable();
    }

    private void OnDisable()
    {
        movementInput.Disable();
    }

    public void PackPlayer()
    {
        released = false;
        //inputComponent.enabled = false;
    }

    void OnFirstAbility()
    {
        abilityList[0].Use(hitData.point);
    }

    void OnSecondAbility()
    {
        abilityList[1].Use(hitData.point);
    }
}
