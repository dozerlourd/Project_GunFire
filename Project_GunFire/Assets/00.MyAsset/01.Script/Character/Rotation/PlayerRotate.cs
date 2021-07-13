using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // ������� ���콺 �巡�� �Է��� �޾Ƽ� ĳ���͸� �����¿�� ȸ����Ű�� �ʹ�!
    // �ʿ� ���: ���콺 �巡�� �Է�, ȸ���� ����(��), ȸ�� �ӷ�
    [SerializeField] float rotSpeed = 300.0f, minRotY = -70.0f, maxRotY = 70.0f;

    [SerializeField] bool isRotX = false, isRotY;

    float rotX = 0;
    float rotY = 0;

    void LateUpdate()
    {
        // ���콺�� �巡�� ���� �Է��� �޴´�.
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        rotX += isRotX ? x * rotSpeed * Time.deltaTime : 0;

        // rotY�� ���� -60�� ~ 60�� ���̷� �����Ѵ�.
        rotY = isRotY ? Mathf.Clamp(rotY + y * rotSpeed * Time.deltaTime, minRotY, maxRotY) : 0;
        // ȸ�� ����(���Ϸ� ��)�� �����.
        Vector3 dir = new Vector3(-rotY, rotX, 0);

        transform.localEulerAngles = dir;
    }
}