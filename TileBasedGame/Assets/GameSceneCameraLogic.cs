using UnityEngine;
using System.Collections;

public class GameSceneCameraLogic : MonoBehaviour
{

    public Transform focused = null;
    public float camSpeed = 4;

    public float camTorque = 180;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            Vector3 forward = transform.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = Vector3.Cross(Vector3.up,forward);

            focused = null;
            transform.position += right * h * Time.fixedDeltaTime * camSpeed;
            transform.position += forward * v * Time.fixedDeltaTime * camSpeed;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 pos = transform.position + transform.forward * 10;
            //transform.Rotate(Vector3.up * Time.fixedDeltaTime * camTorque);
            transform.Rotate(Vector3.up * Time.fixedDeltaTime * camTorque,Space.World);
            transform.position = pos - transform.forward * 10;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Vector3 pos = transform.position + transform.forward * 10;
            transform.Rotate(Vector3.up * Time.fixedDeltaTime * (-camTorque), Space.World);
            transform.position = pos - transform.forward * 10;
        }

        if (focused != null)
        {
            transform.position = Vector3.Lerp(transform.position, focused.position - transform.forward * 10, Time.fixedDeltaTime * camSpeed);
        }
    }
}
