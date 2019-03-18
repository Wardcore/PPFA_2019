using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Slider m_lifebar;
    public float life = 100;

    void Update()
    {
        m_lifebar.value = life;
        if(life <= 0){
            life = 100;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Damage"){
            if(other.gameObject.layer == 10){
                life -= other.gameObject.GetComponent<Bullet>().m_damage;
            }
            else{
                life -= other.attachedRigidbody.GetComponent<WeaponController>().m_CurrentStats.damages;
            }
        }
    }
}
