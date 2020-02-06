using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordThrow : MonoBehaviour
{
    public GameObject sword;
    private void Start()
    {
        if (sword)
        {
            StartCoroutine(ThrowSword());
        }
    }

    private IEnumerator ThrowSword()
    {
        yield return new WaitForSeconds(2);
        Instantiate(sword, transform.position, Quaternion.identity);
        yield return null;
        StartCoroutine(ThrowSword());
    }
}
