using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject player;
    Vector3 playerOffsetVector;

    Vector3 cameraPole;

    float cameraZoom;
    float zoomOutLimit;
    float zoomInLimit;
    float zoomSpeed;

    float leftRightRotation;
    float upDownRotation;
    float downLimit;
    float upLimit;

    float buttonRotateSpeed;

    bool rightClickDrag;
    float mouseRotateSpeed;

    void Start()
    {
        this.player = GameObject.Find("Player");
        this.playerOffsetVector = new Vector3(0f, .6f, 0f);

        this.cameraPole = new Vector3(0, 1f, -1f);

        this.cameraZoom = 5f;
        this.zoomOutLimit = 8f;
        this.zoomInLimit = 1.5f;
        this.zoomSpeed = 60f;

        this.leftRightRotation = 0f;
        this.upDownRotation = 0f;
        this.downLimit = .1f;
        this.upLimit = 1.3f;

        this.buttonRotateSpeed = 110f;

        this.rightClickDrag = false;
        this.mouseRotateSpeed = 400f;
    }

    void Update()
    {
        // check for zooming in/out
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && this.cameraZoom > this.zoomInLimit)
        {
            this.cameraZoom -= this.zoomSpeed * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && this.cameraZoom < this.zoomOutLimit)
        {
            this.cameraZoom += this.zoomSpeed * Time.deltaTime;
        }

        // toggle right-click-drag camera rotation
        if (Input.GetMouseButtonDown(1))
        {
            this.rightClickDrag = true;
            this.leftRightRotation = 0f;
            this.upDownRotation = 0f;
        }
        if (Input.GetMouseButtonUp(1))
        {
            this.rightClickDrag = false;
            this.leftRightRotation = 0f;
            this.upDownRotation = 0f;
        }

        // if right-click-dragging, adjust rotation by change in mouse positions
        if (this.rightClickDrag)
        {
            this.leftRightRotation = Input.GetAxis("Mouse X") * this.mouseRotateSpeed;
            this.upDownRotation = -Input.GetAxis("Mouse Y") * this.mouseRotateSpeed;
        }
        // if not, adjust rotation by button presses
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                this.leftRightRotation += buttonRotateSpeed;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                this.leftRightRotation -= buttonRotateSpeed;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                this.leftRightRotation -= buttonRotateSpeed;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                this.leftRightRotation += buttonRotateSpeed;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                this.upDownRotation += buttonRotateSpeed;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                this.upDownRotation -= buttonRotateSpeed;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                this.upDownRotation -= buttonRotateSpeed;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                this.upDownRotation += buttonRotateSpeed;
            }
        }

        // actually affect positioning / rotation here
        // rotate left/right
        this.cameraPole = Quaternion.AngleAxis(this.leftRightRotation * Time.deltaTime, Vector3.up) * this.cameraPole;
        // rotate up/down (after check for up/down limits)
        if ((this.cameraPole.y < this.upLimit && this.cameraPole.y > this.downLimit) || (this.cameraPole.y >= this.upLimit && this.upDownRotation < 0f) || (this.cameraPole.y <= this.downLimit && this.upDownRotation > 0f))
        {
            this.cameraPole = Quaternion.AngleAxis(this.upDownRotation *Time.deltaTime, this.transform.right) * this.cameraPole;
        }
        // actually place camera at new position (player postion with offset + "camera pole")
        this.transform.position = this.player.transform.position + this.playerOffsetVector + (this.cameraPole * this.cameraZoom);
        this.transform.LookAt(this.player.transform.position + this.playerOffsetVector);
    }
}
