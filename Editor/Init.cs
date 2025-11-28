using UnityEditor;
using UnityEngine;


namespace NaniFutures
{
    public static class PackageInitializer
    {
        [InitializeOnLoadMethod]
        static void OnEditorInitialize()
        {
            Debug.Log("NaniFutures Editor Init");
        }
    }
}