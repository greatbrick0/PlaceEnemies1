using System;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerScript : CombatBody
{
    [SerializeField]
    private CombatUIBrain combatUIC;
    private PlayerInput inputComponent;
    private Vector3 inputVector = Vector3.zero;
    private InputAction movementInput;
    [SerializeField]
    private Camera cam;
    private RaycastHit hitData;
    private Ray mouseRay;

    public override void Release()
    {
        base.Release();
       
        combatUIC = GameObject.Find("CombatUI").GetComponent<CombatUIBrain>();
        if (combatUIC != null)
        {
            combatUIC.ConnectSpellList(abilityList);
            combatUIC.HealthUpdate(health);
        }
        inputComponent.enabled = true;
    }

    protected override void Start()
    {
        base.Start();

        team = "player";
    }

    protected override void Update()
    {
        if (released)
        {
            inputVector = new Vector3(movementInput.ReadValue<Vector2>().x, 0, movementInput.ReadValue<Vector2>().y);
            controlledVelocity = inputVector * moveSpeed;

            mouseRay = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(cam.transform.position, mouseRay.direction, out hitData, 100.0f, 1 << 9))
            {
                transform.LookAt(hitData.point);
            }
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        }

        base.Update();
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
        GetComponent<Collider>().enabled = false;
        inputComponent.enabled = false;
        SessionDataManager.savedPlayerHealth = health;
    }

    void OnFirstAbility()
    {
        UseAbility(0, hitData.point);
    }

    void OnSecondAbility()
    {
        UseAbility(1, hitData.point);
    }

    void OnThirdAbility()
    {
        UseAbility(2, hitData.point);
    }

    void OnFourthAbility()
    {
        UseAbility(3, hitData.point);
    }

    protected override bool UseAbility(int abilityIndex, Vector3 pos)
    {
        bool success = base.UseAbility(abilityIndex, pos);
        if (success && combatUIC != null) combatUIC.TriggerAbility(abilityIndex);
        return success;
    }

    public void SetCameraRef(Camera newCam)
    {
        cam = newCam;
    }

    public void SetAbilities(List<Ability> newAbilities) //black magic, NO touchy
    {
        ConstructorInfo constructor;
        for (int ii = 0; ii < newAbilities.Count; ii++)
        {
            constructor = newAbilities[ii].GetType().GetConstructor(new Type[] { typeof(GameObject) });
            abilityList.Add((Ability)constructor.Invoke(new object[] { gameObject }));
        }

        FillRemainingAbilities();
    }

    public void SetDefaultAbilities()
    {
        abilityList.Add(new GravityAbility(gameObject));
        abilityList.Add(new HomingMissileAbility(gameObject));
        abilityList.Add(new BoulderAbility(gameObject));

        FillRemainingAbilities();
    }

    private void FillRemainingAbilities()
    {
        for(int ii = abilityList.Count; ii < 4; ii++)
        {
            abilityList.Add(new LockedAbility(gameObject));
        }
    }

    public override int Hurt(int damageAmount = 1) //Is this fine? i dont see a problem. slap me ig -Ethan.
    {
        int hurtStore;
        hurtStore = base.Hurt(damageAmount);
        if (hurtStore > 0)
        { 
            if (combatUIC != null) combatUIC.HealthUpdate(health - damageAmount);
            CameraShakeOnHitScript.instance.Shake();
        }
        return base.Hurt(damageAmount);
    }

    protected override void Die()
    {
        GameObject managerRef = GameObject.Find("/ArenaSceneManager"); //i dont like this line
        if (managerRef != null) managerRef.GetComponent<PlacingManager>().PlayerHasDied();

        base.Die();
    }
}
