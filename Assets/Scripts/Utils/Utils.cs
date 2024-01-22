using System;
using System.Collections;
using UnityEngine;

public class Utils
{
    public static T GetOrAddComponet<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if(go == null)
            return null;

        if(recursive == false)
        {
            Transform transform = go.transform.Find(name);
            if(transform != null)
                return transform.GetComponent<T>();
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform != null)
            return transform.gameObject;
        return null;
    }

    public static string GetEnumName<T>(Enum @enum)
    {
        string name = Enum.GetName(typeof(T), @enum);

        return name;
    }

    public static float AbsValue(float value)
    {        
        return value > 0 ? value : -value;
    }

}
