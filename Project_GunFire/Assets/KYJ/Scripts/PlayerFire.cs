using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 플레이어의 키 입력을 받아 총알을 발사하고 싶다
    // 필요요소 : 총알 생성, 총알 생성위치
    public GameObject bulletFactory;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 만일, 마우스 좌 클릭을 하면
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //1.레이 생성
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        //2.부딪힌 물체 정보 담을 변수 생성
        RaycastHit hitInfo;

        //3. 레이 발사
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {

        }
    }
}
