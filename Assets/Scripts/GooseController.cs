﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GooseController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;

    [SerializeField] float mouseSensitivity, walkSpeed, sprintSpeed, jumpForce, smoothTime;

    [SerializeField] Image powerfill, healthfill;

    [SerializeField] GameObject playerUI;

    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    Rigidbody rb;
    PhotonView PV;

    Animator myAnim;
    float timer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        myAnim = GetComponentInParent<Animator>();
    }

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
        else
        {

            GameObject myUI = Instantiate(playerUI);
            myUI.transform.SetParent(this.transform, true);

            //reset power bar and reset health 
            Vector2 resetPower = new Vector2(0f, powerfill.rectTransform.transform.localScale.y);

        powerfill.rectTransform.transform.localScale = resetPower;
        }
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;
        Look();
        Move();
        Jump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            //reset timer
            timer = 0.0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Food")
        {
            //Debug.Log("About to eat food");

            Eat(other);
        }
    }


    void Eat(Collider food)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            myAnim.SetBool("isEating", true);
        }

        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("EATING");
            //Debug.Log(timer);

            timer += Time.deltaTime;

            if (timer >= 1.99f && timer <= 2.01f)
            {
                //Debug.Log("ATE CABBAGE 1");
                //Update the power bar
                Vector2 currentSize = powerfill.rectTransform.transform.localScale;
                Vector2 power = new Vector2(0.1f, 0);
                powerfill.rectTransform.transform.localScale = currentSize + power;

                GameObject lettuce = food.gameObject.transform.Find("Lettuce2").gameObject;

                Destroy(lettuce);
            }
            else if (timer >= 3.99f && timer <= 4.01f)
            {
                //Debug.Log("ATE CABBAGE 2");
                //Update the power bar
                Vector2 currentSize = powerfill.rectTransform.transform.localScale;
                Vector2 power = new Vector2(0.1f, 0);
                powerfill.rectTransform.transform.localScale = currentSize + power;

                GameObject lettuce = food.gameObject.transform.Find("Lettuce4").gameObject;

                Destroy(lettuce);
            }
            else if (timer >= 5.99f && timer <= 6.01f)
            {
               // Debug.Log("ATE CABBAGE 3");
                //Update the power bar
                Vector2 currentSize = powerfill.rectTransform.transform.localScale;
                Vector2 power = new Vector2(0.1f, 0);
                powerfill.rectTransform.transform.localScale = currentSize + power;

                GameObject lettuce = food.gameObject.transform.Find("Lettuce6").gameObject;

                Destroy(lettuce);
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            myAnim.SetBool("isEating", false);
        }
    }

    //Controls for the farmers camera
    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -75f, 75f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;

    }

    //Controls for the farmers movement
    void Move()
    {
        //check to see if there's input from the player to move
       // if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Veritcal") != 0)


            Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

            moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);

    }

    //Controls for the farmers jumping
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }

    //Bool to see if the farmer is grounded, if yes, then can jump
    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);

        //check to see if there is movement input from user, if so
        //set animator boolean isRunning to true
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            myAnim.SetBool("isRunning", true);
        }
        else if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            myAnim.SetBool("isRunning", false);
        }
    }
}
