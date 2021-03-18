using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*    Character Movement    */
    private Rigidbody rigidbody;
    private float normalMovementSpeed;
    private float normalLevitationSpeed;
    [SerializeField] private float movementSpeed = 8.0f;
    [SerializeField] private float slowMovementSpeed = 4.0f;
    [SerializeField]private float levitationSpeed;
    [SerializeField]private float slowLevitationSpeed;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        normalMovementSpeed = movementSpeed;
        normalLevitationSpeed = levitationSpeed;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * levitationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.down * levitationSpeed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = slowMovementSpeed;
            levitationSpeed = slowLevitationSpeed;
        }
        else
        {
            movementSpeed = normalMovementSpeed;
            levitationSpeed = normalLevitationSpeed;
        }

        //get input vector
        float vertInput = Input.GetAxisRaw("Vertical");
        float horizInput = Input.GetAxisRaw("Horizontal");
        Vector2 walkVector = new Vector2(horizInput, vertInput);

        //update movement vector
        Vector3 forwardMovement = transform.forward * walkVector.y;
        Vector3 rightMovement = transform.right * walkVector.x;
        Vector3 movement = Vector3.Normalize(forwardMovement + rightMovement) * movementSpeed;

        rigidbody.velocity = movement;
    }
}
