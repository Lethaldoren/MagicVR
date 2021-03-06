﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

[RequireComponent(typeof(Spell))]
public class Fireball : MonoBehaviour
{
    // public variables
    public bool charging;
    public float chargeTime;
    public GameObject projectilePrefab;
    // public VisualEffect chargeFX;
    public ParticleSystem chargeFX;
    public ParticleSystem readyFX;
    public Spell spell;

    // private variables
    float charge;
    GameObject projectile;

    public UnityEvent OnStartCharge;
    public UnityEvent OnCharging;
    public UnityEvent OnCompleteCharge;

    public void StartCharge()
    {
        charge = 0;
        charging = true;
        // projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        chargeFX.Play();
    }

    public void Charge()
    {
        if (charging)
        {
            //Debug.Log("I am charging");
            charge += Time.deltaTime;
            var prog = charge / chargeTime;
            // projectile.transform.localScale = new Vector3(prog, prog, prog);
        
            if (charge >= chargeTime)
            {
                //Debug.Log("Complete!");
                charge = chargeTime;
                charging = false;
                // projectile.transform.localScale = Vector3.one;

                readyFX.Emit(30);
                chargeFX.Stop();
            }

            
        }
        else
        {
            //Debug.Log("Done charging");
        }
    }

    public void StopCharge()
    {
        if (charging == true)
        {
            // Incomplete charge
            charging = false;

            chargeFX.Stop();
        }
        else
        {
            // Completed charge
            var proj = Instantiate(projectilePrefab, transform.position + (Vector3.forward * .25f), Quaternion.identity);
            Vector3 vel = transform.parent.GetComponent<Valve.VR.InteractionSystem.Wand>().velocity;
            vel = vel.normalized * Mathf.Clamp(Mathf.Pow(vel.magnitude, 2), 0, 10);
            proj.GetComponent<FireballProjectile>().Launch(vel);
            OnCompleteCharge.Invoke();
        }
        charge = 0;
    }


}
