using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechController : MonoBehaviour
{
    [SerializeField] private bool m_StartActive = false;
    [SerializeField] private Transform m_playerLockPos;
    [SerializeField] private Transform m_playerOutPos;
    [SerializeField] private GameObject m_DetectionSphere;

    [Header("Shooting")]
    [SerializeField] private LayerMask m_shootLayer;
    [SerializeField] private Transform m_shootPoint;
    [SerializeField] private float m_bulletSpeed;
    [SerializeField] private float m_ShootDelay;
    [SerializeField] private GameObject m_Bullet;
    private TopDownController m_mechTopDown;
    
    [Header("Monitoring")]
    public bool canShoot = false;
    public bool isActive;

    private ActionController m_player;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<ActionController>();
        m_mechTopDown = GetComponent<TopDownController>();
        m_mechTopDown.canMove = m_StartActive;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(isActive)
            InputActions();
    }

    private void InputActions()
    {        
        if(Input.GetButtonDown("Interact")){
            QuitMech();
        }

        //SHOOT
        if(Input.GetButton("Fire1") && canShoot){
            StartCoroutine("Shoot");
        }
	}

    private IEnumerator Shoot(){
        canShoot = false;
        //print("shoot");
        GameObject bullet = Instantiate(m_Bullet, m_shootPoint.position, m_shootPoint.rotation);
        bullet.name = "Bullet";
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * m_bulletSpeed, ForceMode.Impulse);

        yield return new WaitForSeconds(m_ShootDelay);

        canShoot = true;
    }

    public void ActiveMech(){
        m_player.transform.parent = transform;
        m_player.transform.position = m_playerLockPos.position;
        m_player.gameObject.SetActive(false);
        m_mechTopDown.canMove = true;
        m_DetectionSphere.SetActive(false);
        isActive = true;
    }

    public void QuitMech(){
        m_player.transform.parent = null;
        m_player.transform.position = m_playerOutPos.position;
        m_player.canInteract = m_player.m_PlayerWeapons.canShoot = m_player.m_PlayerTopDown.canMove = true;
        m_player.gameObject.SetActive(true);

        m_mechTopDown.canMove = false;
        m_DetectionSphere.SetActive(true);
        isActive = false;
    }
}
