using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    // 사용자의 마우스 드래그 입력을 받아서 캐릭터를 상하좌우로 회전시키고 싶다!
    // 필요 요소: 마우스 드래그 입력, 회전의 방향(축), 회전 속력
    [SerializeField] float rotSpeed = 300.0f, minRotY = -70.0f, maxRotY = 70.0f;

    [SerializeField] bool isRotX = false, isRotY;

    float rotX = 0;
    float rotY = 0;

    void LateUpdate()
    {
        // 마우스의 드래그 방향 입력을 받는다.
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        rotX += isRotX ? x * rotSpeed * Time.deltaTime : 0;

        // rotY의 값을 -60도 ~ 60도 사이로 제한한다.
        rotY = isRotY ? Mathf.Clamp(rotY + y * rotSpeed * Time.deltaTime, minRotY, maxRotY) : 0;
        // 회전 벡터(오일러 각)를 만든다.
        Vector3 dir = new Vector3(-rotY, rotX, 0);

        transform.localEulerAngles = dir;
    }
}