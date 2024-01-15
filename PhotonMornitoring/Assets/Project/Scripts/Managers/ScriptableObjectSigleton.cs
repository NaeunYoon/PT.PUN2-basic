using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ScriptableObject 를 상속받는 추상클래스를 만듬
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ScriptableObjectSigleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T _inst;
    public static T Instance
    {
        get
        {
            if (_inst == null)
            {
                T[] result = Resources.FindObjectsOfTypeAll<T>();
                if (result.Length == 0)
                {
                    Debug.LogError("ScriptableObjectSingleton.Instance.result.Length == 0 " + typeof(T).ToString());
                    return null;
                }

                if (result.Length > 1)
                {
                    Debug.LogError("ScriptableObjectSingleton.Instance.result.Length > 1" + typeof(T).ToString());
                    return null;
                }

                _inst = result[0];
            }

            return _inst;
        }
    }
}
