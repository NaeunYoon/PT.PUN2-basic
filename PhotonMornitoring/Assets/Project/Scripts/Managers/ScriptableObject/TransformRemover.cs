using UnityEngine;
public static class Transforms
{
    public static void DestroyChildren(this Transform t, bool destroyImmediately = false)
    {
        foreach (Transform transform in t)
        {
            if (destroyImmediately)
            {
                MonoBehaviour.DestroyImmediate(transform.gameObject);
            }
            else
            {
                MonoBehaviour.Destroy(transform.gameObject);
            }
        }
    }
}
