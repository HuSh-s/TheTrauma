using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitChair : MonoBehaviour
{
    public GameObject _Player;
    public GameObject SittingText;
    public GameObject SittingPoint;
    public GameObject ExitPoint;
    public Camera _Camera;
    public ExamineObjectDarkRoom _ExamineObjectDarkRoom;
    public QuestionSystem _QuestionSystem;


    public bool InReach;
    public bool IsSitting;

    [Header("Look Settings")]
    public float lookSpeed = 2f;
    public float maxYawAngle = 90f;   // saða-sola max açý
    public float maxPitchAngle = 60f; // yukarý-aþaðý max açý

    private float currentYaw = 0f;
    private float currentPitch = 0f;

    void Start()
    {
        InReach = false;
        IsSitting = false;
        SittingText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!this.enabled) return;

        if (other.CompareTag("Reach"))
        {
            InReach = true;
            SittingText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!this.enabled) return;

        if (other.CompareTag("Reach"))
        {
            InReach = false;
            SittingText.SetActive(false);
        }
    }

    void Update()
    {
        if (InReach && Input.GetKeyDown(KeyCode.B))
        {
            if (!IsSitting && InReach)
            {
                SitDown();
                _QuestionSystem.InReach = false;
            }
            else if (IsSitting)
            {
                StandUp();
                _QuestionSystem.InReach = true;
            }
        }

        if (IsSitting && !ExamineManagerDarkRoom.Instance.IsExamining)
        {
            HandleLook();
        }
    }

    private void SitDown()
    {
        IsSitting = true;

        // Player kontrolünü kapat
        _Player.GetComponent<Player>().enabled = false;
        _Player.GetComponent<CapsuleCollider>().enabled = false;
        _Player.GetComponent<CharacterController>().enabled = false;

        // Oturma noktasýna gönder
        _Player.transform.position = SittingPoint.transform.position;
        _Player.transform.rotation = SittingPoint.transform.rotation;
        _Player.transform.SetParent(SittingPoint.transform);

        SittingText.SetActive(false);

        // Cursor kilit
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void StandUp()
    {
        if (ExamineManagerDarkRoom.Instance != null && ExamineManagerDarkRoom.Instance.IsExamining)
        {
            ExamineManagerDarkRoom.Instance.ExitExamine();
            _ExamineObjectDarkRoom.ExamineText.SetActive(false);
        }
        IsSitting = false;

        _Player.transform.SetParent(null);

        // Kalkýþ noktasýna gönder
        _Player.transform.position = ExitPoint.transform.position;
        Vector3 flatRotation = ExitPoint.transform.eulerAngles;
        _Player.transform.rotation = Quaternion.Euler(0f, flatRotation.y, 0f);

        // Player kontrolünü aç
        _Player.GetComponent<Player>().enabled = true;
        _Player.GetComponent<CapsuleCollider>().enabled = true;
        _Player.GetComponent<CharacterController>().enabled = true;

        SittingText.SetActive(false);

        // FPS moduna dönerken cursor kilitli kalsýn
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InReach = false;
    }

    private void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        currentYaw += mouseX;
        currentYaw = Mathf.Clamp(currentYaw, -maxYawAngle / 2f, maxYawAngle / 2f);

        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(currentPitch, -maxPitchAngle, maxPitchAngle);

        _Camera.transform.localRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
    }
}
