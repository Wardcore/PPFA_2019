using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] public WeaponType m_WeaponType;
    [SerializeField] public Weapon m_Weapon;
    [SerializeField] private GameObject m_detectionSphere;
    
    [SerializeField] public Transform m_shootPoint;

    [Header("Weapon main Stats")]
    [SerializeField] public Stats m_WeaponStats;

    // [Header("Weapon main Stat")]
    // [SerializeField] public float m_mainDamage = 1;
    // [SerializeField] public float m_mainAttackRate = 5;
    // [SerializeField] public float m_mainRange = 1;

    // [Header("Distance Stats")]
    // [SerializeField] public float m_bulletSpeed = 20;
    // [SerializeField] public float m_bulletSize = 1;
    // [SerializeField] public int m_bulletPerShot = 1;
    // [SerializeField] public float m_bulletSpread = 0;
    
    [Header("Melee Stats")]
    [SerializeField] public Animator m_meleeAnimator;
    [SerializeField] public AnimationClip m_attackAnim;

    private bool isAttacking;

    public void GrabWeapon(WeaponController graber, bool isPrimary = true){
        transform.parent = graber.m_WeaponPoint;
        transform.position = graber.m_WeaponPoint.position;
        transform.rotation = graber.m_WeaponPoint.rotation;
        m_detectionSphere.SetActive(false);
        if(isPrimary)
            graber.m_shootPoint = m_shootPoint;
        gameObject.SetActive(isPrimary);
    }

    public void ThrowWeapon(WeaponController graber){
        transform.parent = null;
        graber.StopAttack();
        graber.m_CurrentWeapon = Weapon.none;
        transform.position = new Vector3(transform.position.x + Random.Range(-1.0f, 1.0f), 0.2f, transform.position.z + Random.Range(-1.0f, 1.0f));
        m_detectionSphere.SetActive(true);
        gameObject.SetActive(true);

    }

    //MELEE
    public void PlayAnim(float rate, float speed){
        if(m_meleeAnimator.GetBool("Cooldown") && !isAttacking){
            //StartCoroutine("CooldownMelee", m_attackAnim.length/speed + 1/rate);
            StartCoroutine(CooldownMelee(speed, rate));
        }
    }

    // public void StartCooldown(){
    //     StartCoroutine("CooldownMelee");
    // }
    // public void SetParameter(string s){
    //     m_meleeAnimator.SetBool(s, true);
    // }

    // public void ResetParameter(string s){
    //     m_meleeAnimator.SetBool(s, false);
    // }

    private IEnumerator CooldownMelee(float speed, float rate){
        isAttacking = true;
        print(speed + "  " + rate + " " + m_attackAnim.length);

        m_meleeAnimator.SetBool("Attack", true);
        m_meleeAnimator.SetFloat("Speed", speed);
        yield return new WaitForSeconds((m_attackAnim.length/speed)/2); 

        m_meleeAnimator.SetBool("Cooldown", false);
        yield return new WaitForSeconds((m_attackAnim.length/speed)/2); 

        yield return new WaitForSeconds(1/rate);  
        m_meleeAnimator.SetBool("Attack", false);
        m_meleeAnimator.SetBool("Cooldown", true);

        print("cd OK");
        isAttacking = false;
    }
}
