using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AttackController), typeof(Rigidbody), typeof(HealthSystem))]
public class CharacterMovement : MonoBehaviour, IAtributeIncrease
{
    [System.Serializable]
    private struct MovementValues
    {
        public float movementSpeed;
        public float rotationSpeed;
    }

    private float initialMovementSpeed = 0f;
    [SerializeField] private MovementValues movementValues;
    // [SerializeField] public Animator playeranim;
    CharacterController ccharacter;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform player;
    float characterAngle;

    private HealthSystem playerHealth = null;
    private AttackController _atkController;
    private Rigidbody _rb;
    

    private float gravity =  -9.8f;
    Vector3 movementInput;

    private void Start()
    {
        ccharacter = GetComponent<CharacterController>();
        playerHealth = GetComponent<HealthSystem>();
        _atkController = GetComponent<AttackController>();
        _rb = GetComponent<Rigidbody>();
        initialMovementSpeed = movementValues.movementSpeed;
        // playeranim = GetComponent<Animator>();
    }

    private void Update()
    {
        GetInputs();
        UpdateMovement();
        LookDirection();
    }

    private void GetInputs()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.z = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            _atkController.UseAttack();
        }
    }

    private void UpdateMovement()
    {
        // Defines the move vector
        Vector3 move =  Vector3.right * movementInput.x + Vector3.forward * movementInput.z;
        // Clamp move vector so speed doesn't increment diagonally
        move = Vector3.ClampMagnitude(move, 1f);
        if (movementInput != Vector3.zero)
        {
            // Moves the character
            //ccharacter.Move(move * movementValues.movementSpeed * Time.deltaTime);
            _rb.MovePosition(transform.position + move * Time.deltaTime * movementValues.movementSpeed);
            // Rptate towards specified movement direction
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(movementInput.x, 0, movementInput.z), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, movementValues.rotationSpeed * Time.deltaTime);
        }
    }

    void LookDirection()
    {
        // To Be Tested:

        Vector3 lookatPoint = Vector3.zero;
        var worldRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        var didHit = Physics.Raycast(worldRay, out RaycastHit hit);
        lookatPoint = hit.point;
        lookatPoint.y = player.position.y;
        if (!didHit)
        {
            return;
        }
        player.localRotation = Quaternion.LookRotation((lookatPoint - player.position), Vector3.up);

    }

    public void IncreaseSpeed(float increasePercentage)
    {
        movementValues.movementSpeed += initialMovementSpeed * increasePercentage;
    }

    public void IncreaseHealth(float increasePercentage)
    {
        playerHealth.HealthUpgrade(increasePercentage);
    }

    public void IncreaseDamage(float increasePercentage)
    {
        //Empty
    }

    public void IncreaseRateOfFire(float increasePercentage)
    {
        //Empty
    }

    public void IncreaseProjectileSpeed(float increasePercentage)
    {
        //Empty
    }
}
