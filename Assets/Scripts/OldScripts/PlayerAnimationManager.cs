﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Com.ZachCarpenter.PhotonTutorial
{
    public class PlayerAnimationManager : MonoBehaviourPun
    {

        #region Private Fields

        [SerializeField]
        private float directionDampTime = 0.25f;
        private Animator animator;

        #endregion

        #region MonoBehaviour Callbacks

        void Start()
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayerAnimationManager is Missing Animator Component", this);
            }
        }

        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }

            if (!animator)
            {
                return;
            }

            //deal with jumping
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //only allow jumping if we are running
            if (stateInfo.IsName("Base Layer.Run"))
            {
                //when using trigger parameter
                if (Input.GetButtonDown("Fire2"))
                {
                    animator.SetTrigger("Jump");
                }
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (v < 0)
            {
                v = 0;
            }
            animator.SetFloat("Speed", h * h + v * v);
            animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
        }

        #endregion
    }

}