using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerHabilities
{
    //-- VARIABLES -----------------------------------------------------------------------------------------------------------------

    [Header("Jump Variables")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpVelocity;

    public float normalSmooth;
    public float smoothJump;
    public float smoothFall;

    private float speedY;
    private bool jumpRequest;

    public PlayerDash dashController;
    [Header("Sonido Jump")]
    public AudioSource audioSource;
    public AudioClip clipJump;
    [Header("Camera")]
    public MainCamera mainCamera;

    private bool isJumping;
    public float jumpTime;
    public float jumpTimeCounter;
    public float jumpForceToHold;
    private Vector3 jumpForce;

    //-- START & UPDATE ------------------------------------------------------------------------------------------------------------

    protected override void Start()
    {
        dashController = GetComponent<PlayerDash>();
        mainCamera = FindObjectOfType<MainCamera>();
        base.Start();
    }

    private void Update()
    {
        /*if (Input.GetButtonDown("Jump") && _player.isGrounded && _player.canJump)
        {
            mainCamera.ChangeSmoothTimeY(smoothJump);
            jumpRequest = true;
            audioSource.clip = clipJump;
            audioSource.Play();
        }*/

        //if (!dashController.isDashing)
        if (_player.canChangeGravity)
        {
            if (_player.rigidBody.velocity.y < 0.0f)
            {
                _player.gravityScale = fallMultiplier;
                mainCamera.ChangeSmoothTimeY(smoothFall);
            }
            else if (_player.rigidBody.velocity.y >= 0.0f)
            {
                //Debug.Log($"{_player.rigidBody.velocity.y}");
                mainCamera.ChangeSmoothTimeY(smoothJump);
                _player.gravityScale = lowJumpMultiplier;
            }
        }
        else
        {
            //mainCamera.ChangeSmoothTimeY(normalSmooth);
            _player.gravityScale = 0;
        }
        

        speedY = _player.rigidBody.velocity.y;
        playerAnimatorController.Fall(speedY);
    }

    private void FixedUpdate()
    {
        /*if (jumpRequest)
        {
            _player.rigidBody.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);

            jumpForce.Set(_player.rigidBody.velocity.x, jumpVelocity, 0.0f);
            //playerAnimatorController.Jump();
            jumpRequest = false;
            isJumping = true;
            jumpTimeCounter = jumpTime;
        }*/

        if(Input.GetButtonDown("Jump") && _player.isGrounded && _player.canJump)
        {
            mainCamera.ChangeSmoothTimeY(smoothJump);
            _player.rigidBody.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
            isJumping = true;
            jumpTimeCounter = jumpTime;

            audioSource.clip = clipJump;
            audioSource.Play();
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                jumpForce.Set(_player.rigidBody.velocity.x, jumpForceToHold, 0.0f);
                _player.rigidBody.velocity = jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
        
    }


}
