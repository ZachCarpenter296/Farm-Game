using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;

    [SerializeField] float mouseSensitivity, walkSpeed, sprintSpeed, jumpForce, smoothTime;

    [SerializeField] Image powerfill;

    [SerializeField] GameObject playerUI;

    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    Rigidbody rb;
    PhotonView PV;

    Animator myAnim;
    float timer;

    EnergyHealth myEnergyHealth;
    Image healthBar;
    Image energyBar;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        myAnim = GetComponent<Animator>();
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

            myEnergyHealth = this.GetComponentInChildren<EnergyHealth>();
            healthBar = myEnergyHealth.health;
            energyBar = myEnergyHealth.energy;

            Debug.Log(healthBar.rectTransform.transform.localScale.x);

            //reset power bar and reset health 
            Vector2 resetPower = new Vector2(0f, energyBar.rectTransform.transform.localScale.y);

            energyBar.rectTransform.transform.localScale = resetPower;
            //Debug.LogWarning(powerfill.rectTransform.transform.localScale.x + " x value");
        }
    }

    private void Update()
    {
        if (!PV.IsMine)
            return;
        Look();
        Jump();
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PV.IsMine)
            return;
        if (other.tag == "Food")
        {
            //reset timer
            timer = 0.0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!PV.IsMine)
            return;
        if (other.tag == "Food")
        {
            //Debug.Log("About to eat food");

            Eat(other);
        }
    }

    void Eat(Collider food)
    {
        if (!PV.IsMine)
            return;
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
            else if(timer >= 3.99f && timer <= 4.01f)
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
                //Debug.Log("ATE CABBAGE 3");
                //Update the power bar
                Vector2 currentSize = powerfill.rectTransform.transform.localScale;
                Vector2 power = new Vector2(0.1f, 0);
                powerfill.rectTransform.transform.localScale = currentSize + power;

                GameObject lettuce = food.gameObject.transform.Find("Lettuce6").gameObject;

                Destroy(lettuce);
                Debug.Log(currentSize);
            }
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            myAnim.SetBool("isEating", false);
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Taking hits");

        Vector2 currentHealth = healthBar.rectTransform.transform.localScale;
        Vector2 newHealth = new Vector2(damage, 0f);

        healthBar.rectTransform.transform.localScale = currentHealth - newHealth;

        Debug.Log(healthBar.rectTransform.transform.localScale.x);
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