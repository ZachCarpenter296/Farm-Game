using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCastShoot : MonoBehaviour
{
    /// <summary>
    /// This code was taken from a unity tutorial and implements raycasting for the Farmer's weapons
    /// https://learn.unity.com/tutorial/let-s-try-shooting-with-raycasts#5c7f8528edbc2a002053b468
    /// </summary>
    /// 
    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    [SerializeField] private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    private LineRenderer laserLine;

    //this holds the time for when the player will be allowed to fire again
    private float nextFire;


    void Start()
    {
        laserLine = GetComponent<LineRenderer>();

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);

                if(hit.collider.tag == "Player")
                {
                    hit.collider.SendMessage("TakeDamage", 0.4f);
                }
               
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;

        //this causes our coroutine to wait 0.07 secodns
        yield return shotDuration;
        laserLine.enabled = false;
    }
}

