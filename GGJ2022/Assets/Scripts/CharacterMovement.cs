using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [System.Serializable]
    private struct MovementValues
    {
        public float movementSpeed;
        public float rotationSpeed;
    }

    [SerializeField] private MovementValues movementValues;
    // [SerializeField] public Animator playeranim;
    CharacterController ccharacter;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform player;
    float characterAngle;
    

    private float gravity =  -9.8f;
    Vector3 movementInput;

    private void Start()
    {
        ccharacter = GetComponent<CharacterController>();
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
            ccharacter.Move(move * movementValues.movementSpeed * Time.deltaTime);
            // Rptate towards specified movement direction
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(movementInput.x, 0, movementInput.z), Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, movementValues.rotationSpeed * Time.deltaTime);
        }
        /*playeranim.SetFloat("MovementX", movementInput.x);
        playeranim.SetFloat("MovementY", movementInput.y);*/
    }

    void LookDirection()
    {
        // To Be Tested:

        characterAngle += Input.GetAxis("Mouse X") * movementValues.rotationSpeed * Time.deltaTime;
        characterAngle = Mathf.Clamp(characterAngle, 0, 360);
        player.localRotation = Quaternion.AngleAxis(characterAngle, Vector3.up);

    }
}
