using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    PhotonView PV;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        //if (!PV.IsMine)
        //{
        //    Destroy(rigidbody);
        //}
        normalMovementSpeed = movementSpeed;
        normalLevitationSpeed = levitationSpeed;
    }

    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //transform.Translate(Vector3.up * levitationSpeed * Time.deltaTime);
            rigidbody.velocity = Vector3.up * levitationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            //transform.Translate(Vector3.down * levitationSpeed * Time.deltaTime);
            rigidbody.velocity = Vector3.down * levitationSpeed * Time.deltaTime;
        }
        else
        {
            rigidbody.velocity = new Vector3(1, 0, 1);
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
        Vector3 newMovement = new Vector3(movement.x, rigidbody.velocity.y, movement.z);

        rigidbody.velocity = newMovement;
    }
}
