using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float rotationSpeed;
    public GameObject playerObject;
    public Transform playerTransform;
    public GameObject cameraObject;
    public Transform cameraTransform;
    public GameObject scene;
    public SceneController sceneController;
    public float cameraPlayerDistance;
    public float cameraHeight;
    public float cameraTilt;
    private bool pressedA = false, pressedD = false, pressedW = false, pressedS = false;
    public bool pressEToHide = false;

    private void Start()
    {
        Cursor.visible = false;
        playerTransform = playerObject.transform;
        cameraTransform = cameraObject.transform;

        if (SceneManager.GetActiveScene().name == "Cutscene" || SceneManager.GetActiveScene().name == "Press button")
        {
            sceneController = scene.GetComponent<SceneController>();
        }
    }

    private void Update()
    {
        Rigidbody rb = playerObject.GetComponent<Rigidbody>();

        #region Mouse Controls

        if (Input.GetAxis("Mouse X") < 0)
        {
            playerTransform.Rotate(Vector3.down * rotationSpeed, Space.World);
            rb.velocity = Vector3.zero;
            pressedA = pressedD = pressedS = pressedW = false;
        }
        else if (Input.GetAxis("Mouse X") > 0)
        {
            playerTransform.Rotate(Vector3.up * rotationSpeed, Space.World);
            rb.velocity = Vector3.zero;
            pressedA = pressedD = pressedS = pressedW = false;
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }

        #endregion

        #region Keyboard Controls

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            rb.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (!pressedA)
            {
                rb.velocity += -playerTransform.right * walkSpeed;
                pressedA = true;
            }
        } 
        else if (pressedA)
        {
            rb.velocity -= -playerTransform.right * walkSpeed;
            pressedA = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!pressedD)
            {
                rb.velocity += playerTransform.right * walkSpeed;
                pressedD = true;
            }
        }
        else if (pressedD)
        {
            rb.velocity -= playerTransform.right * walkSpeed;
            pressedD = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (!pressedW)
            {
                rb.velocity += playerTransform.forward * walkSpeed;
                pressedW = true;
            }
        }
        else if (pressedW)
        {
            rb.velocity -= playerTransform.forward * walkSpeed;
            pressedW = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (!pressedS)
            {
                rb.velocity += -playerTransform.forward * walkSpeed;
                pressedS = true;
            }
        }
        else if (pressedS)
        {
            rb.velocity -= -playerTransform.forward * walkSpeed;
            pressedS = false;
        }

        if (pressEToHide && Input.GetKey(KeyCode.E))
        {
            sceneController.TriggerScene();
        }

        #endregion

        #region Camera Movement

        cameraTransform.position = playerTransform.position - playerTransform.forward * cameraPlayerDistance + Vector3.up * cameraHeight;

        #endregion

        #region Camera Rotation

        cameraTransform.rotation = playerTransform.rotation;
        cameraTransform.Rotate(Vector3.right * cameraTilt);

        #endregion
    }
}
