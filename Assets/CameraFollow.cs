
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform m_target;
    public Transform m_lookTarget;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private void Start()
    {
        //Cursor.visible = false;
    }

    void Update()
    {
        // if (Input.GetKeyDown("escape"))
        // {
        //     Cursor.visible = true;
        // }

        Vector3 desiredPosition = m_target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        
        transform.LookAt(m_lookTarget);


    }

}
