using UnityEngine;
using Photon.Pun;

public class RigidbodySync : MonoBehaviourPun, IPunObservable
{

    Rigidbody r;

    Vector3 latestPos;
    Quaternion latestRot;
    Vector3 latestScale;
    Vector3 velocity;
    Vector3 angularVelocity;
    Collider collider;

    bool valuesReceived = false;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        
        collider = GetComponent<Collider>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.localScale);
            stream.SendNext(r.constraints);
            stream.SendNext(r.velocity);
            stream.SendNext(r.angularVelocity);
            stream.SendNext(collider.isTrigger);
        }
        else
        {
            //Network player, receive data
            latestPos = (Vector3)stream.ReceiveNext();
            latestRot = (Quaternion)stream.ReceiveNext();
            latestScale = (Vector3)stream.ReceiveNext();
            r.constraints = (RigidbodyConstraints)stream.ReceiveNext();
            velocity = (Vector3)stream.ReceiveNext();
            angularVelocity = (Vector3)stream.ReceiveNext();
            collider.isTrigger = (bool)stream.ReceiveNext();

            valuesReceived = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && valuesReceived)
        {
            //Update Object position and Rigidbody parameters
            transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, latestRot, Time.deltaTime * 5);
            transform.localScale = Vector3.Lerp(transform.localScale, latestScale, Time.deltaTime * 5);
            r.velocity = velocity;
            r.angularVelocity = angularVelocity;
        }
    }

    void OnCollisionEnter(Collision contact)
    {
        if (!photonView.IsMine)
        {
            Transform collisionObjectRoot = contact.transform.root;
            if (collisionObjectRoot.CompareTag("Player"))
            {
                //Transfer PhotonView of Rigidbody to our local player
                photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
            }
        }
    }
}