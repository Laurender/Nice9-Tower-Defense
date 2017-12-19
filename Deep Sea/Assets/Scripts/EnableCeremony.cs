using UnityEngine;
using System.Collections;
using System;

// This adds a scale out effect to various GUI elements being enabled
public class EnableCeremony : MonoBehaviour
{
    [SerializeField, Tooltip("This is needed for GUI elements that have layout components active.")]
    private bool _unscaled;

    private static Vector3[] _scales = { new Vector3(.5f,.6f,1), new Vector3(.5f, .6f, 1), new Vector3(.6f, .7f, 1), new Vector3(.8f, .9f, 1), new Vector3(1.1f, 1.1f, 1), Vector3.one };
    private Vector3 _originalScale;

    void OnEnable()
    {
        if (_unscaled)
        {
            _originalScale = Vector3.one;
        }
        else
        {
            _originalScale = gameObject.transform.localScale;
        }

        StartCoroutine(DoCeremony());
    }

    IEnumerator DoCeremony()
    {
        Vector3 temp;

        //Debug.Log(_originalScale);

        foreach(Vector3 scale in _scales)
        {
            temp.x = scale.x * _originalScale.x;
            temp.y = scale.y * _originalScale.y;
            temp.z = scale.z * _originalScale.z;

            gameObject.transform.localScale = temp;

            yield return new WaitForSecondsRealtime(.03f);
        }

    }
}
