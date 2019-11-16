/**
 * @author Jtil
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBase<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T instance;
    private static object lockObj = new object();
    

    public static T Instance
    {
        get
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance != null && FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.Log("잘못된 싱글톤입니다.");
                    }

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton)" + typeof(T).ToString();
                    }
                }

                return instance;
            }
        }
    }

    public virtual void OnDestroy()
    {
        instance = null;
    }
}