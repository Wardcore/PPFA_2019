using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRaycast : MonoBehaviour
{
	[SerializeField] private Transform m_MouseRaycastIndicator;
	[SerializeField] private LayerMask m_LayerGround;

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    private void Look(){
        Ray mouseOnWorld = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //Debug.DrawRay(mouseOnWorld, Color.red);
        if(Physics.Raycast(mouseOnWorld, out hit, 100, m_LayerGround)){
            m_MouseRaycastIndicator.position = new Vector3(hit.point.x, 0, hit.point.z);
        }
    }
}
