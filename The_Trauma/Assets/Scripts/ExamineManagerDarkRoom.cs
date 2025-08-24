using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineManagerDarkRoom : MonoBehaviour
{
    public static ExamineManagerDarkRoom Instance;
    public Transform examinePoint;
    private GameObject currentObject;
    private GameObject wrapperObject;

    public Player _Player;
    public GameObject _UI;
    public bool PlayerController;

    public bool IsExamining;
    [SerializeField] private float rotationSpeed = 200f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        IsExamining = false;
    }

    void Update()
    {
        if (IsExamining)
        {
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            examinePoint.Rotate(Vector3.up, -rotX, Space.World);
            examinePoint.Rotate(Vector3.right, rotY, Space.Self);

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
                ExitExamine();
        }
    }

    public void ShowObject(GameObject obj)
    {
        PlayerController = _Player.GetComponent<Player>().enabled;

        _Player.GetComponent<Player>().enabled = false;
        _Player.GetComponent<CharacterController>().enabled = false;

        _UI.SetActive(false);

        currentObject = obj;
        currentObject.SetActive(true);
        currentObject.transform.SetParent(examinePoint);
        currentObject.transform.localPosition = Vector3.zero;
        currentObject.transform.localRotation = Quaternion.identity;

        IsExamining = true;
    }

    public void ExitExamine()
    {
        _Player.GetComponent<Player>().enabled = PlayerController;
        _Player.GetComponent<CharacterController>().enabled = PlayerController;

        _UI.SetActive(true);

        if (currentObject != null)
        {
            currentObject.SetActive(false);
            currentObject.transform.SetParent(null);
        }

        IsExamining = false;
    }
}
