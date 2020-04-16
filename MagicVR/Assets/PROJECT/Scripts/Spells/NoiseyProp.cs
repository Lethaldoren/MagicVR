﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourMachine;

public class NoiseyProp : MonoBehaviour
{
    // Start is called before the first frame update
    public float ForceToAlert;
    public float NoiseRadius;
    public LayerMask enemyLayerMask;
    Rigidbody rb;

    GameObjectVar BBProp;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
       if (collision.impulse.magnitude > ForceToAlert)
        {
            Collider[] colliders = Physics.OverlapSphere(rb.transform.position, NoiseRadius, enemyLayerMask);

            foreach (Collider collider in colliders)
            {
                Blackboard blackboard = collider.gameObject.GetComponent<Blackboard>();

                BBProp = blackboard.GetGameObjectVar("NoisyProp");

                BBProp.Value = gameObject;

                blackboard.SendEvent("HeardSound");
            }
        }
    }
}
