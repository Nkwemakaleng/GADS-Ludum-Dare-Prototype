using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //SINGLETON SETUP
    //private static PlayerManager _Instance;
    //public static PlayerManager Instance
    //{
    //    get
    //    {
    //        if (!_Instance)
    //        {
    //            // NOTE: read docs to see directory requirements for Resources.Load!
    //            var prefab = Resources.Load<GameObject>("PathToYourSingletonViaPrefab");

    //            // create the prefab in your scene
    //            var inScene = Instantiate<GameObject>(prefab);

    //            // try find the instance inside the prefab
    //            _Instance = inScene.GetComponentInChildren<PlayerManager>();

    //            // guess there isn't one, add one
    //            if (!_Instance) _Instance = inScene.AddComponent<PlayerManager>();

    //            // mark root as DontDestroyOnLoad();
    //            DontDestroyOnLoad(_Instance.transform.root.gameObject);
    //        }
    //        return _Instance;
    //    }
    //}

    // implement your Awake, Start, Update, or other methods here...
}
