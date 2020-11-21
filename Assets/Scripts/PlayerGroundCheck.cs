using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    /// <summary>
    /// This script checks to see if the farmer is touching the ground. If he is, then he can jump, otherwise he cannot jump
    /// </summary>
    FarmerController farmerController;
    GooseController gooseController;
    TurkeyController turkeyController;
    ChickenController chickenController;

    private void Awake()
    {
        farmerController = GetComponentInParent<FarmerController>();
        gooseController = GetComponentInParent<GooseController>();
        turkeyController = GetComponentInParent<TurkeyController>();
        chickenController = GetComponentInParent<ChickenController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            return;

        SetTrue(this.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            return;

        SetFalse(this.gameObject.name);
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other == farmerController.gameObject)
        if (other.tag == "Player")
            return;

        SetTrue(this.gameObject.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            return;

        SetTrue(this.gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            return;

        SetFalse(this.gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            return;

        SetTrue(this.gameObject.name);
    }

    private void SetTrue(string name)
    {
        if (name == "GroundCheckFarmer")
        {
            farmerController.SetGroundedState(true);
        }
        if (name == "GroundCheckGoose")
        {
            gooseController.SetGroundedState(true);
        }
        if(name == "GroundCheckTurkey")
        {
            turkeyController.SetGroundedState(true);
        }
        if(name == "GroundCheckChicken")
        {
            chickenController.SetGroundedState(true);
        }
    }

    private void SetFalse(string name)
    {
        if (name == "GroundCheckFarmer")
        {
            farmerController.SetGroundedState(false);
        }
        if (name == "GroundCheckGoose")
        {
            gooseController.SetGroundedState(false);
        }
        if (name == "GroundCheckTurkey")
        {
            turkeyController.SetGroundedState(false);
        }
        if (name == "GroundCheckChicken")
        {
            chickenController.SetGroundedState(false);
        }
    }
}
