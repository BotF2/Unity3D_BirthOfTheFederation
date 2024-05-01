using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Assets.Core
{
   
    public class E_Animator1 : MonoBehaviour
    {
        public Animator anim;
        public AudioSource warpAudioSource_0;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            if (GameManager.Instance._statePassedCombatInit)
            {
                anim.SetBool("EnemyWarp1", true);
                PlayWarp();
                //if (once == 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("E1_allGoodThings"))
                //{
                //    shipLayerSetup.OnStateEnter(anim, anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(anim.Name)), anim.GetLayerIndex(anim.Name));
                //    once = 1;
                //}
            }
        }

        public void PlayWarp() // called in animation - warp
        {
            if (GameManager.Instance._statePassedCombatInit)
            {
                warpAudioSource_0.volume = 1f;
                warpAudioSource_0.Play();
            }
        }
    }
}
