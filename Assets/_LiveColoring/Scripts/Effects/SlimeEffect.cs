using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _sprites;

    public void ShowEffect()
    {
        if(transform.localPosition.x < 0)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        if (transform.localPosition.y > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        }

        StartCoroutine(Show());
    }

    private IEnumerator Show()
    {
        _sprites[0].SetActive(true);

        for (int i = 1; i < _sprites.Length; i++)
        {
            _sprites[i - 1].SetActive(false);

            _sprites[i].SetActive(true);

            yield return new WaitForSeconds(0.05f);
        }

        //SoundManager.Instance.Play("monsterexplode", 0.05f);

        _sprites[_sprites.Length - 1].SetActive(false);

        //Behaviour.Instance.GetEffectsComponent().PlayMonsterEffect();
    }
}
