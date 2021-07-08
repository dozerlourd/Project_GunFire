using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Granade : MonoBehaviour
{
    // 일정 시간이 지나면 터지는 수류탄 구현
    // 적에 닿으면 바로 터짐

    public float PointPadDuration
    {
        get
        {
            //// 만약 각성 1이면 리턴 chainReleasedTime + 4.
            //if () return poisonPadDuration;
            //// 그렇지 않고, 각성 2이면 리턴 chainReleasedTime + 8.
            //else if () return poisonPadDuration;
            //// 그렇지 않고, 각성 3이면 리턴 chainReleasedTime + 12.
            //else if () return poisonPadDuration;
            //// 그렇지 않고, 각성 2이면 리턴 chainReleasedTime.
            //else
            return poisonPadDuration;
        }
    }
    [SerializeField, Tooltip("수류탄 장판의 지속시간")] float poisonPadDuration = 3.0f;
    [SerializeField] float effectDuration = 3.5f;
    float count = 0;
    [SerializeField] GameObject granadeVFX, AuraPad;

    [SerializeField] float rayCheckDist = 3.5f;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "T_Ground" || count == 1)
        {
            StartCoroutine(Explosion(poisonPadDuration));
        }
        else count++;
    }

    IEnumerator Explosion(float dur)
    {
        RaycastHit rayHitInfo;
        Physics.Raycast(transform.position, Vector3.down * rayCheckDist, out rayHitInfo, LayerMask.GetMask("L_Ground"));

        //하단으로 L_Ground를 검색하는 레이를 쏴서 레이의 point에 독장판 생성
        GameObject poisonPadClone = Instantiate(AuraPad, rayHitInfo.point + (Vector3.up * 0.1f), Quaternion.LookRotation(-rayHitInfo.normal));
        Destroy(poisonPadClone, dur);

        GameObject effectClone = Instantiate(granadeVFX);
        effectClone.transform.position = transform.position;
        Destroy(effectClone, effectDuration);
        //gameObject.SetActive(false);
        Destroy(gameObject);
        yield return null;
    }
}
