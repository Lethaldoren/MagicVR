﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaBallSpell : EquipableSpell
{
    public Transform wandTip;
    GameObject fireball;
    Rigidbody frb;

    float speed = 4000;


    public new void OnEquip()
    {
        Debug.Log("I am Magmaball");
        //create fireball particle effect and have it on hand
    }

    public new void OnTriggerDown()
    {
        //create fireball and change parent
        fireball = Instantiate(spellPrefab, wandTip);
        frb = fireball.GetComponent<Rigidbody>();
    }

    public new void OnTriggerHeld()
    {
        //keep fireball in hand until trigger released
        //give croshair for shot direction
    }


    public new void OnTriggerUp()
    {
        //records position at release and unparents from hand and moves to position at release
        Transform currentPosition = fireball.transform;
        fireball.transform.parent = null;
        fireball.transform.position = currentPosition.transform.position;

        //shoots fireball forward at speed
        if (frb != null)
        {
            frb.AddRelativeForce(Vector3.forward * speed);
        }
        //destroy prefab after 5 seconds or contact
    }

    public new void OnUnequip()
    {

    }

}
