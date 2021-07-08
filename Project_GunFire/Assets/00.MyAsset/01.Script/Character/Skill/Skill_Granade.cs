using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Granade : MonoBehaviour
{
    // ���� �ð��� ������ ������ ����ź ����
    // ���� ������ �ٷ� ����

    public float PointPadDuration
    {
        get
        {
            //// ���� ���� 1�̸� ���� chainReleasedTime + 4.
            //if () return poisonPadDuration;
            //// �׷��� �ʰ�, ���� 2�̸� ���� chainReleasedTime + 8.
            //else if () return poisonPadDuration;
            //// �׷��� �ʰ�, ���� 3�̸� ���� chainReleasedTime + 12.
            //else if () return poisonPadDuration;
            //// �׷��� �ʰ�, ���� 2�̸� ���� chainReleasedTime.
            //else
            return poisonPadDuration;
        }
    }
    [SerializeField, Tooltip("����ź ������ ���ӽð�")] float poisonPadDuration = 3.0f;
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

        //�ϴ����� L_Ground�� �˻��ϴ� ���̸� ���� ������ point�� ������ ����
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
