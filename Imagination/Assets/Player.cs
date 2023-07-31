using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Vector3 velocity;
    bool isGrounded;

    public static Player Instance;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public CharacterController controller;
    public float speed = 12f;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else if (Instance != this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance,groundMask);
        if (isGrounded && velocity.y <0)
        {
            velocity.y = -1f;
        }
        
        velocity.y += gravity * Time.deltaTime;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime * Time.deltaTime);
    }
    public void OnSwitch()
    {
        if (SceneManager.GetActiveScene().name == "Earth")
        {
            SceneManager.LoadScene("TirNaNog");
        }
        else if (SceneManager.GetActiveScene().name == "TirNaNog")
        {
            SceneManager.LoadScene("Earth");
        }
    }
    public void OnJump() { 
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
