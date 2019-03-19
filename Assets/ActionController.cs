using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [Header("Monitoring")]
    public bool canInteract = true;
    public bool m_isInteracting;

    
    [HideInInspector] public TopDownController m_PlayerTopDown; 
    [HideInInspector] public WeaponController m_PlayerWeapons; 

    void Start()
    {
        m_PlayerTopDown = GetComponent<TopDownController>();
        m_PlayerWeapons = GetComponent<WeaponController>();
    }

    void Update()
    {
		InputActions();
    }

	private void InputActions(){
        
        //INTERACT
        if(Input.GetButtonDown("Interact") && canInteract){
            m_isInteracting = true;
        }
        if(Input.GetButtonUp("Interact") && canInteract){
            m_isInteracting = false;
        }

        //INTERACT
        if(Input.GetButtonDown("OtherInteraction") && m_PlayerWeapons.m_weaponEquiped){
            foreach (Transform weapon in m_PlayerWeapons.m_WeaponPoint)
            {
                weapon.GetComponent<Weapons>().ThrowWeapon(m_PlayerWeapons);
            }
            m_PlayerWeapons.m_CurrentStats.ResetStats();
            m_PlayerWeapons.m_BaseWeaponStats.ResetStats();
        }

        //SHOOT
        if(Input.GetButton("Fire1") && m_PlayerWeapons.m_weaponEquiped && m_PlayerWeapons.canShoot && m_PlayerWeapons.m_CurrentWeapon != Weapon.none){
            m_PlayerWeapons.Attack();
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9){
            if(other.tag == "Mech"){
                UIManager.WriteActionMessage("Press E to enter");
            }

            if(other.tag == "Weapon"){
                UIManager.WriteActionMessage("Press E to pick up");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 9){
            if(other.tag == "Mech"){
                UIManager.WriteActionMessage("");
            }

            if(other.tag == "Weapon"){
                UIManager.WriteActionMessage("");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 9){
            if(other.tag == "Mech"){
                if(m_isInteracting){
                    m_isInteracting = false;
                    //print("hasInteract with : " + other.transform.parent.name);
                    canInteract = m_PlayerWeapons.canShoot = false;
                    UIManager.WriteActionMessage("");
                    other.transform.parent.parent.GetComponent<MechController>().ActiveMech();
                }
            }

            if(other.tag == "Weapon"){
                if(m_isInteracting){
                    m_isInteracting = false;

                    //Ref of the weapon selected
                    Weapons weapon = other.transform.parent.GetComponent<Weapons>();
                    UIManager.WriteActionMessage("");

                    // //Throw the old weapon
                    // if(m_PlayerWeapons.m_weaponEquiped)
                    //     m_PlayerWeapons.m_CurrentWeaponRef.ThrowWeapon(m_PlayerWeapons);

                    if(m_PlayerWeapons.m_CurrentWeapon == Weapon.none){
                        //starting weapon
                        m_PlayerWeapons.m_CurrentWeaponType = weapon.m_WeaponType;
                        m_PlayerWeapons.m_CurrentWeapon = weapon.m_Weapon;

                        m_PlayerWeapons.m_BaseWeaponStats.AddStats(weapon.m_WeaponStats, true);
                        m_PlayerWeapons.m_CurrentStats.AddStats(weapon.m_WeaponStats, true);
                        
                        //Grab the new weapon
                        weapon.GrabWeapon(m_PlayerWeapons);
                    }
                    else{
                        m_PlayerWeapons.m_CurrentStats.AddStats(weapon.m_WeaponStats, false);
                        
                        weapon.GrabWeapon(m_PlayerWeapons, false);
                    }
                }
            }
        }
    }
}
