using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    Coroutine shaking;

    [SerializeField]
    private bool startOnAwake = false;

    private void Awake()
    {
        if (startOnAwake) Shake();
    }

    public void Shake()
    {
        shaking = StartCoroutine(ShakingAnim());
    }

    private IEnumerator ShakingAnim()
    {
        Vector3 center = transform.localPosition;

        while(true)
        {
            for (int i = 0; i < 15; i++)
            {
                transform.localPosition = new Vector3(Random.Range(-3, 3) + center.x, Random.Range(-3, 3) + center.y, center.z);

                yield return new WaitForSeconds(0.02f);
            }
            //SoundManager.Instance.Play("ltbshake");
            yield return new WaitForSeconds(1f);
        }
    }

    public void StopShake()
    {
        StopCoroutine(shaking);
    }
}
