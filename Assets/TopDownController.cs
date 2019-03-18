using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType{
    Player = 0,
    Mecha,
    other
}

public class TopDownController : MonoBehaviour {

	[SerializeField] private EntityType m_Entity;

    [Header("Pivots")]
    [SerializeField] private Transform m_LookPivot;
    [SerializeField] private Transform m_MovePivot;

    [Header("Moving")]
    [SerializeField] private float m_moveSpeed = 2;
	[SerializeField] private Transform m_MouseRaycastIndicator;

    [Header("Monitoring")]
    public bool canMove = true;
	public Vector3 m_Move;
    public bool isMoving;

	[HideInInspector] public Rigidbody m_rbEntity;

	private float m_inputH;
	private float m_inputV;

	void Start () 
    {
		m_rbEntity = GetComponent<Rigidbody>();
	}
	
	void Update () 
	{
        InputMvt();
	}

	private void FixedUpdate()
    {
        //MVT
        if(canMove){        
            //Calcutate MVT
            m_Move = m_inputV * Vector3.forward * Time.deltaTime + m_inputH * Vector3.right * Time.deltaTime;
            Debug.DrawRay(transform.transform.position, m_Move*10, Color.blue);

            
            isMoving = (m_Move.magnitude > 0);

            m_rbEntity.MovePosition(m_rbEntity.position + m_Move.normalized*(m_moveSpeed/10));
            
            //ROTATION
            m_LookPivot.LookAt(m_MouseRaycastIndicator, Vector3.up);
            m_MovePivot.forward = (m_Move != Vector3.zero)? m_Move: m_MovePivot.forward;
        }

        if(!isMoving){
            m_rbEntity.velocity = Vector3.zero;
            m_rbEntity.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            m_rbEntity.isKinematic = true;
        }else{
            m_rbEntity.isKinematic = false;
            m_rbEntity.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
	}	
    
    private void InputMvt(){
        //MVT
        m_inputH = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(m_inputH) > 1)
            m_inputH = 1;
        //
        m_inputV = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(m_inputV) > 1)
            m_inputV = 1;
	}

}
