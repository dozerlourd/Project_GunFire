using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    #region
    //Vector3 dir;
    //[SerializeField] float lerpScolar;
    //[SerializeField] float lerpSpeed;
    //float lerpRate = 0;

    //Vector3 startPos;

    //private void Start()
    //{
    //    dir = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
    //    dir.Normalize();
    //    startPos = transform.position;
    //    StartCoroutine(LerpVector());
    //}

    //IEnumerator LerpVector()
    //{
    //    while(true)
    //    {
    //        lerpRate += 0.3f/*(Time.deltaTime * lerpSpeed)*/;
    //        transform.position = Vector3.Lerp(startPos, startPos + dir * lerpScolar, lerpRate);
    //        Camera.main.WorldToScreenPoint(transform.position);
    //        yield return new WaitForEndOfFrame();
    //    }
    //}
    #endregion
    [SerializeField] Text _text;
    [SerializeField] float lifetime = 1.0f;
    [SerializeField] float minDist = 2f, maxDist = 3f;

    private Vector3 initPos, targetPos;
    private float timer;

    private void Start()
    {
        transform.SetParent(ParentSystem.Instance.DamageUICanvas);
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        float dir = Random.rotation.eulerAngles.z;
        initPos = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPos = initPos + Quaternion.Euler(0, 0, dir) * new Vector3(dist, dist, 0f);
        transform.localScale = Vector3.one;
    }

    float t = 0.25f;
    float tt = 0;
    private void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifetime / 2f;
        tt += (t *= 0.85f);

        if (timer > lifetime) Destroy(gameObject);
        else if (timer > fraction) _text.color = Color.Lerp(_text.color, Color.clear, (timer - fraction) / (lifetime - fraction));



        transform.position = Vector3.Lerp(initPos, targetPos, /*tt); */Mathf.Sin(timer * 1.8f / lifetime));

        //transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifetime));
    }

    public void SetDamageText(int _damage)
    {
        _text.text = _damage.ToString();
    }
}
