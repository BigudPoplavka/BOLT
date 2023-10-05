using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameLoader : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        var instance = Singletone<GameLoader>.Instance;

        DontDestroyOnLoad(gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerInstantiator"),
                Vector3.zero, Quaternion.identity);
        }
    }
}
