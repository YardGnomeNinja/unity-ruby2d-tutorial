﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public ParticleSystem collectibleEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            if(controller.health < controller.maxHealth)
            {
                ParticleSystem collectibleEffectInstance = Instantiate(collectibleEffect, transform.position, Quaternion.identity);
                collectibleEffectInstance.Play();

                controller.ChangeHealth(1);
                Destroy(gameObject);
            }
        }
    }
}
