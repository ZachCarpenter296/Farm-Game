﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.ZachCarpenter.PhotonTutorial
{
    public class PlayerUI : MonoBehaviour
    {
        #region Private Fields

        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        private Text playerNameText;

        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;

        private PlayerManager target;

        float characterControllerHeight = 0f;
        Transform targetTransform;
        Renderer targetRenderer;
        CanvasGroup _canvasGroup;
        Vector3 targetPosition;

        #endregion

        #region Public Fields

        [Tooltip("Pixel offset from the player target")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 30f, 0f);

        #endregion

        #region MonoBehaviour Callbacks

        private void Update()
        {
            // Destroy itself if the target is null, It's a fail safe when Photon is destroying Instances of a Player over the network
            if (target == null)
            {
                Destroy(this.gameObject);
            }

            //Reflect the Player Health
            if (playerHealthSlider != null)
            {
                playerHealthSlider.value = target.Health;
            }

        }

        private void Awake()
        {
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);

            _canvasGroup = this.GetComponent<CanvasGroup>();
        }

        private void LateUpdate()
        {
            //Do not show the UI if we are not visible to the camera, thus avoid potential bugs with seeing the UI but no the player
            if (targetRenderer != null)
            {
                this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
            }

            // #Critical
            //Follow the target GameObject on screen
            if (targetTransform != null)
            {
                targetPosition = targetTransform.position;
                targetPosition.y += characterControllerHeight;
                this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
            }
        }

        #endregion

        #region Public Methods

        public void SetTarget(PlayerManager _target)
        {
            if (_target == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget", this);
                return;
            }

            //Cache references for efficiency
            target = _target;
            if (playerNameText != null)
            {
                playerNameText.text = target.photonView.Owner.NickName;
            }

            targetTransform = this.target.GetComponent<Transform>();
            targetRenderer = this.target.GetComponent<Renderer>();
            CharacterController characterController = _target.GetComponent<CharacterController>();
            //Get data from the player that won't change during the lifetim of this component
            if (characterController != null)
            {
                characterControllerHeight = characterController.height;
            }
        }

        #endregion
    }

}