using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
    {

    public float playerSpeed, turnSmoothTime;
    public Transform Camera;
    public Joystick playerJoystick;


    private Vector3 PlayerVelocity;
    private float turnSmoothVelocity = 1;
    private CharacterController controller;

    public PhotonView view;


    // Start is called before the first frame update
    void Start()
        {
        controller = gameObject.GetComponent<CharacterController>();
        }

    // Update is called once per frame
    void Update()
        {

        if (view.IsMine)
            {

            Vector3 move = new Vector3(playerJoystick.Horizontal, 0, playerJoystick.Vertical).normalized;

            if (move.magnitude >= 0.1f)
                {

                float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0, targetAngle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                PlayerVelocity = moveDir.normalized * Time.deltaTime * playerSpeed;

                controller.Move(PlayerVelocity);
                Camera.Translate(PlayerVelocity);
                }

            }

        }
    }
