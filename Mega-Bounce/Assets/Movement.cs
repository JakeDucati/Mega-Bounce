using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private int count;
    public TextMeshProUGUI countText;
    void Start()
    {
        count = PlayerPrefs.GetInt("CubesCollected", 0);
        SetCountText();
        readyToJump = true;
    }
    public void SetCountText()
    {
        count = PlayerPrefs.GetInt("CubesCollected", 0);
        countText.text = "Cubes Collected: " + count.ToString();
    }
    void Update()
    {
        grounded = Physics.Raycast(transform.position,Vector3.down,playerHeight * 0.5f + 0.3f);
        LimitSpeed();
        PlayerInput();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    float horizontalInput;
    float verticalInput;
    bool grounded;
    float playerHeight = 1;
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        horizontalInput = movementVector.x;
        verticalInput = movementVector.y;
    }
    void PlayerInput()
    {
        if (Input.GetKey(KeyCode.Space) && grounded && readyToJump)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), 0.3f);
        }
    }
    void ResetJump()
    {
        readyToJump = true;
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(orientation.up * jumpForce, ForceMode.Impulse);
    }
    public Transform orientation;
    public Rigidbody rb;
    public float moveSpeed;
    public float jumpForce;
    bool readyToJump;
    private void MovePlayer()
    {
        Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10, ForceMode.Force);
    }
    private void LimitSpeed()
    {
        Vector3 limitedVelocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
        if (limitedVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVel = limitedVelocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.GetComponent<PickupManager>().HideCube();
            count = count + 1;
            PlayerPrefs.SetInt("CubesCollected", count);
            SetCountText();
        }
    }
}
