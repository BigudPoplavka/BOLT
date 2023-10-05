using UnityEngine;

public abstract class Singletone<T>: Singletone where T: MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(ApplicationQuiting)
            {
                return null;
            }
            else
            {
                if (_instance != null)
                {
                    return _instance;
                }

                var otherInstances = FindObjectsOfType<T>();
                var count = otherInstances.Length;

                if (count > 0)
                {
                    if(count == 1)
                    {
                        return _instance = otherInstances[0];
                    }

                    for(int i = 0; i < count; i++)
                    {
                        Destroy(otherInstances[i]);
                    }

                    return _instance = otherInstances[0];
                }

                return _instance = new GameObject($"Instance {typeof(T)}").AddComponent<T>();
            }    
        }
    }

}

public abstract class Singletone : MonoBehaviour
{
    public static bool ApplicationQuiting { get; private set; }

    public void OnApplicationQuit()
    {
        ApplicationQuiting = true;
    }
}
