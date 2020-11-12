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
        if (other == farmerController.gameObject || gooseController.gameObject || turkeyController.gameObject || chickenController.gameObject)
            return;

        farmerController.SetGroundedState(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == farmerController.gameObject || gooseController.gameObject || turkeyController.gameObject || chickenController.gameObject)
            return;

        farmerController.SetGroundedState(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == farmerController.gameObject || gooseController.gameObject || turkeyController.gameObject || chickenController.gameObject)
            return;

        farmerController.SetGroundedState(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == farmerController.gameObject || gooseController.gameObject || turkeyController.gameObject || chickenController.gameObject)
            return;

        farmerController.SetGroundedState(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == farmerController.gameObject || gooseController.gameObject || turkeyController.gameObject || chickenController.gameObject)
            return;

        farmerController.SetGroundedState(false);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == farmerController.gameObject || gooseController.gameObject || turkeyController.gameObject || chickenController.gameObject)
            return;

        farmerController.SetGroundedState(true);
    }
}
