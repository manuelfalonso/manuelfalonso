﻿#if CINEMACHINE
using Cinemachine.Utility;
using UnityEngine;

namespace SombraStudios.Shared.Gameplay.PlayerMovement3D
{
    /// <summary>
    /// Move an object with Input around a SphereCollider.
    /// </summary>
    public class PlayerMoveOnSphere : MonoBehaviour
    {
        public SphereCollider Sphere;

        public float speed = 5;
        public bool rotatePlayer = true;
        public float rotationDamping = 0.5f;

        // Update is called once per frame
        void Update()
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (input.magnitude > 0)
            {
                input = Camera.main.transform.rotation * input;
                if (input.magnitude > 0.001f)
                {
                    transform.position += input * (speed * Time.deltaTime);
                    if (rotatePlayer)
                    {
                        float t = Cinemachine.Utility.Damper.Damp(1, rotationDamping, Time.deltaTime);
                        Quaternion newRotation = Quaternion.LookRotation(input.normalized, transform.up);
                        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, t);
                    }
                }
            }

            // Stick to sphere surface
            if (Sphere != null)
            {
                var up = transform.position - Sphere.transform.position;
                up = up.normalized;
                var fwd = transform.forward.ProjectOntoPlane(up);
                transform.position = Sphere.transform.position + up * (Sphere.radius + transform.localScale.y / 2);
                transform.rotation = Quaternion.LookRotation(fwd, up);
            }
        }
    }
}
#endif