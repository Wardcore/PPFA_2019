using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType{
    Distance = 0,
    melee,
    Tool,
    other,
    none
}

public enum Weapon{
    Automatic = 0,
    Shotgun,
    GrenadeLauncher,
    Fist,
    Blade,
    Spear,
    none
}

public class WeaponController : MonoBehaviour
{
    [SerializeField] public Weapon m_StartingWeapon = Weapon.none;

    [Header("Shooting refs")]
    //[SerializeField] private LayerMask m_shootLayer;
    [SerializeField] public Transform m_shootPoint;
    [SerializeField] public Transform m_WeaponPoint;
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] public bool m_weaponEquiped = false;

    [Header("Weapon main Stats")]
    [SerializeField] public Stats m_BaseWeaponStats;
    [SerializeField] public Stats m_CurrentStats;

    // [Header("OLD")]
    // [Header("Weapon main Stat")]
    // [SerializeField] public float m_mainDamage = 1;
    // [SerializeField] private float m_mainAttackRate = 5;
    // [SerializeField] private float m_mainRange = 2;

    // [Header("Weapon other Stats")]
    // [SerializeField] private float m_bulletSpeed = 16;
    // [SerializeField] private float m_bulletSize = 2;
    // [SerializeField] private int m_bulletPerShot = 1;
    // [SerializeField] private float m_bulletSpread = 1.5f;

    [Header("Monitoring")]
    [SerializeField] public WeaponType m_CurrentWeaponType;
    [SerializeField] public Weapon m_CurrentWeapon = Weapon.none;
    [SerializeField] public Weapons m_CurrentWeaponRef;

    public bool canShoot = true;

    void Start()
    {
        // ResetBaseStat(m_CurrentStats);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(m_WeaponPoint.childCount == 1){
            m_weaponEquiped = true;
            m_CurrentWeaponRef = m_WeaponPoint.GetChild(0).GetComponent<Weapons>();
        }
        else if(m_WeaponPoint.childCount == 0)
        {
            m_weaponEquiped = false;
            m_CurrentWeaponRef = null;
        }
    }

    public void Attack(){
        switch (m_CurrentWeaponType)
        {
            case WeaponType.Distance:
                StartCoroutine("Shoot");
            break;
            case WeaponType.melee:
                m_CurrentWeaponRef.PlayAnim(m_CurrentStats.attackRate, m_CurrentStats.attackSpeed);
            break;
            default:
            break;
        }
    }

    public void StopAttack(){
        StopCoroutine("Shoot");
        canShoot = true;
    }

    private IEnumerator Shoot(){
        canShoot = false;

        for (int i = 0; i < m_CurrentStats.bulletPerShot; i++)
        {

            GameObject bullet = Instantiate(m_Bullet, m_shootPoint.position, m_shootPoint.rotation);

            bullet.name = "Bullet";

            bullet.GetComponent<Bullet>().m_livingTime = m_CurrentStats.range;
            bullet.GetComponent<Bullet>().m_damage = m_CurrentStats.damages/m_CurrentStats.bulletPerShot;
            bullet.transform.localScale = bullet.transform.localScale*m_CurrentStats.bulletSize;

            Vector3 spread = bullet.transform.right* Random.Range(-m_CurrentStats.bulletSpread, m_CurrentStats.bulletSpread);
            float speed = (m_CurrentStats.bulletPerShot > 1)? m_CurrentStats.bulletSpeed + Random.Range(-m_CurrentStats.bulletSpread, m_CurrentStats.bulletSpread): m_CurrentStats.bulletSpeed;
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * speed + spread, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(1/m_CurrentStats.attackRate);

        canShoot = true;
    }
}
