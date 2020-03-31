using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    CharacterController controller;
    Transform cam;
    public float speed=0.1f,runSpeedFactor=2f,jumpForce=2,rotSpeed;
    public Vector3 gravity;
    public bool editMode = false;
    public CodeEditor codeEditor;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        cam=transform.GetChild(0);
    }
    Vector3 yVel=new Vector3();
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                editMode ^= true;
                Cursor.lockState = editMode ? CursorLockMode.None : CursorLockMode.Locked;
                codeEditor.SetActive(editMode);
            }
        }
        transform.Rotate(0, Input.GetAxis("Mouse X")*rotSpeed*Time.deltaTime,0);
        cam.Rotate(-Input.GetAxis("Mouse Y") * Time.deltaTime * rotSpeed, 0,0);

        if (editMode) return;//------------------------

        if (controller.isGrounded)
        {
            yVel = new Vector3(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.Space) && controller.isGrounded)
        {
            yVel = new Vector3(0, jumpForce,0);
        }
        yVel += gravity * Time.deltaTime;
        
        Vector3 motion = new Vector3();
        motion+= Input.GetAxis("Horizontal")*transform.right;
        motion+= Input.GetAxis("Vertical")*transform.forward;
        motion*=speed * Time.deltaTime;
        if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Vertical") != 0)
            motion *= 0.71f;
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            motion *= runSpeedFactor;
        motion += yVel;
        controller.Move(motion );

    }
    

}
