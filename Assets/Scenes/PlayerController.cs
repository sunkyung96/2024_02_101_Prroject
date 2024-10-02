using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //�÷��̾��� ������ �ӵ��� �����ϴ� ����
    [Header("Player Movement")]
    public float moveSpeed = 5.0f; //�̵� �ӵ�
    public float jumpForce = 5.0f; //���� ��

    //ī�޶� ���� ����
    [Header("Camera Settings")]
    public Camera firstPersonCamera; //1��Ī ī�޶�
    public Camera thiedPersonCamera; //3��Ī ī�޶�

    public float radius = 5.0f; //3��Ī ī�޶�� �÷��̾� ���� �Ÿ�
    public float minRadius = 1.0f; //ī�޶� �ּ� �Ÿ�
    public float maxRadius = 10.0f; //ī�޶� �ִ� �Ÿ�

    private float yMinLimit = 30; //ī�޶� ���� ȸ�� �ּҰ�
    private float yMaxLimit = 90; //ī�޶� ���� ȸ�� �ִ밢

    private float theta = 0.0f;  //ī�޶��� ���� ȸ�� ����
    private float phi = 0.0f;  //ī�޶��� ���� ȸ�� ����
    private float targetVerticalRoataion = 0; //��ǥ ���� ȸ�� ����
    private float verticalRotationSpeed = 240f;  //���� ȸ�� �ӵ�

    public float mouseSenesitivity = 2f; //���콺 ����

    //���� ������
    private bool isFirstPerson = true; //1��Ī ��� ���� ����
    private bool isGrounded; //�÷��̾ ���� ���� ����
    private Rigidbody rb; //�÷��̾��� Rigidbody

    //Ȱ��ȭ�� ī�޶� �����ϴ� �Լ�
    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(isFirstPerson); //1��Ī ī�޶� Ȱ��ȭ ����
        thiedPersonCamera.gameObject.SetActive(!isFirstPerson); //3��Ī ī�޶� Ȱ��ȭ ����
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //RigidBody ������Ʈ�� �����´�.

        Cursor.lockState = CursorLockMode.Locked; //���콺 Ŀ���� ��װ� �����.
        SetupCameras();
        SetActiveCamera();
    }
    //Start is called before the first frame update


    //Update is called once per frame
    void Update()
    {
        HandleJump();
        HandleRotation();
        HandleMovement();
        HandleCameraToggle();
    }

    //ī�޶� �� ĳ���� ȸ���� �����ϴ� �Լ�
    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity; //���콺 �¿� �Է�
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity; //���콺 ���� �Է�

        //���� ȸ��(theta ��)
        theta += mouseX; //���콺 �Է°� �߰�
        theta = Mathf.Repeat(theta, 360f); //���� ���� 360�� ���� �ʵ��� ����

        //���� ȸ�� ó��
        targetVerticalRoataion -= mouseY;
        targetVerticalRoataion = Mathf.Clamp(targetVerticalRoataion, yMinLimit, yMaxLimit); //���� ȸ�� ����
        phi = Mathf.MoveTowards(phi, targetVerticalRoataion, verticalRotationSpeed * Time.deltaTime);

        //�÷��̾� ȸ��(ĳ���Ͱ� �������θ� ȸ��)
        transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);

        if (isGrounded)
        {
            firstPersonCamera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);//1��Ī ī�޶� ���� ȸ��
        }
        else
        {
            //3��Ī ī�޶� ���� ��ǥ�迡�� ��ġ �� ȸ�� ���
            float x = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Cos(Mathf.Deg2Rad * theta);
            float y = radius * Mathf.Cos(Mathf.Deg2Rad * phi);
            float z = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Sin(Mathf.Deg2Rad * theta);

            thiedPersonCamera.transform.position = transform.position + new Vector3(x, y, z);
            thiedPersonCamera.transform.LookAt(transform); //ī�޶� �׻� �÷��̾ �ٶ󺸵��� ����

            //���콺 ��ũ���� ����Ͽ� ī�޶� �� ����
            radius = Mathf.Clamp(radius - Input.GetAxis("Mause ScrollWheel") * 5, minRadius, maxRadius);
        }

    }
    //Update is called once per frame

    //ī�޶� �ʱ� ��ġ �� ȸ���� �����ϴ� �Լ�
    void HandleCameraToggle()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFirstPerson = !isFirstPerson;//ī�޶� ��� ��ȯ
            SetActiveCamera();
        }
    }
    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0f, 0.6f, 0f); //1��Ī ī�޶� ��ġ
        firstPersonCamera.transform.localRotation = Quaternion.identity; //1��Ī ī�޶� ȸ�� �ʱ�ȭ
    }
    // �÷��̾� ������ ó���ϴ� �Լ�
    void HandleJump()
    {
        //���� ��ư�� ������ �翡 ���� ��
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //�������� ���� ���� ����
            isGrounded = false; //���߿� �ִ� ���·� ��ȯ
        }
    }

    //�÷��̾ �翡 ��� �ִ��� ����
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true; //�浹 ���̸� �÷��̾�� ���� �ִ�.
    }

    //�÷��̾��� �̵��� ó���ϴ� �Լ�
    void HandleMovement()
    {
        float moveHorizontat = Input.GetAxis("Horizontal"); //�¿� �Է�(-1,1)
        float moveVerical = Input.GetAxis("Vertical");  //�յ� �Է�(1,-1)

        if (!isFirstPerson)//3��Ī ��� �� ��, ī�޶� �������� �̵�ó��
        {
            Vector3 cameraForward = thiedPersonCamera.transform.forward; //ī�޶� �� ����
            cameraForward.y = 0f; //���� ���� ����
            cameraForward.Normalize(); //���� ���� ����ȭ (0~1) ������ ������ ������ش�.

            Vector3 cameraRight = thiedPersonCamera.transform.right; //ī�޶� ������ ����
            cameraRight.y = 0f;
            cameraRight.Normalize();

            //�̵� ���� ���
            Vector3 movement = transform.right * moveHorizontat + cameraRight * moveHorizontat;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime); //���� ��� �̵�
        }

        else
        {

            //ĳ���� �������� �̵�(1��Ī)
            Vector3 movement = transform.right * moveHorizontat + transform.forward * moveVerical;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime); //���� ��� �̵�

        }

    }
}
