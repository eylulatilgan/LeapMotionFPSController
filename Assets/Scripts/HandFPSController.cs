using UnityEngine;
using System.Collections;
using Leap;

public class HandFPSController : MonoBehaviour
{
    public float grabDistance = 10.0f;
    public Transform holdPosition;
    public float throwForce = 100.0f;
    public ForceMode throwForceMode;
    public LayerMask layerMask = -1;

    private GameObject heldObject = null;
    private Controller ctrl;
    private Frame frame;
    private Hand currentHand;
    void Start()
    {
        ctrl = new Controller();

    }

    void rotate(Hand hand)
    {
        float centerOfXAxis = 0.0f;
        float handX = hand.PalmPosition.ToUnityScaled().x;

        if (handX > centerOfXAxis + 0.1)
        {
            transform.Rotate(Vector3.up, 100 * Time.deltaTime);
        }
        else if (handX < centerOfXAxis)
        {
            transform.Rotate(Vector3.down, 100 * Time.deltaTime);
        }
    }

    void movement(Hand hand)
    {
        if (hand.PalmPosition.ToUnityScaled().z > 0)
        {
            transform.position += transform.forward * 0.2f;
        }
        else if (hand.PalmPosition.ToUnityScaled().z < -0.1f)
        {
            transform.position -= transform.forward * 0.1f;
        }


    }


    // Update is called once per frame
    void Update()
    {

        frame = ctrl.Frame();
        currentHand = frame.Hands[0];

        movement(currentHand);
        rotate(currentHand);
        grabbing(currentHand);
    }

    private void grabbing(Hand hand)
    {
      //  foundHit = Physics.SphereCast(transform.parent.position, 1, transform.parent.forward, out hit);

        if (heldObject == null)
        {
            if (hand.GrabStrength == 1) {
                RaycastHit hit;
                if (Physics.SphereCast(transform.position, 1, transform.forward, out hit, layerMask)) {
                    Debug.Log("You cast!");
                    heldObject = hit.collider.gameObject;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Collider>().enabled = false;
                }
            }
        }
        else
        {
            heldObject.transform.position = holdPosition.position;
            //heldObject.transform.rotation = holdPosition.rotation;

            if (hand.GrabStrength == 0)
            {
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject.GetComponent<Collider>().enabled = true;
                heldObject.GetComponent<Rigidbody>().AddForce(throwForce * transform.forward, throwForceMode);
                heldObject = null;
            }
        }
    }
}
