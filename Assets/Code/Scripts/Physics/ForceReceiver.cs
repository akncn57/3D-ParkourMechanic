using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float drag;
    private Vector3 _impact;
    private Vector3 _dampingVelocity;
    private float _verticalVelocity;
    public Vector3 Movement => _impact + Vector3.up * _verticalVelocity;

    private void Update()
    {
        // If player stopped and player is grounded.
        if (_verticalVelocity < 0 && controller.isGrounded)
        {
            _verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        
        _impact = Vector3.SmoothDamp(_impact, Vector3.zero, ref _dampingVelocity, drag);
    }
}
