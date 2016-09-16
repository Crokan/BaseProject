using UnityEngine;
using UnityEditor;
using UnityEditor.VersionControl;
using System.Collections;
using System.Collections.Generic;

public class MasterParamManager : ScriptableObject
{
    public enum Types
    {
        Int,
        Float,
        String,
    }

    [System.Serializable]
    public class SerializeTypeInfo
    {
        public string Id;
        public TypeInfo Info;
    }

    [System.Serializable]
    public class TypeInfo
    {
        public Types Type;
        public string Value;
    }

    public SerializeTypeInfo[] Values = new SerializeTypeInfo[0];
    public string[] ParamNames = new string[0];
    public Dictionary<string, TypeInfo> mDict;

    static MasterParamManager Instance;

    const string AssetDirectory = "Assets/Resources/MasterParams/";


    public string GetString(string Id)
    {
        if (mDict[Id].Type == Types.String)
        {
            return mDict[Id].Value;
        }

        return string.Empty;
    }

    public int GetInt(string Id)
    {
        if (mDict[Id].Type == Types.Int)
        {
            return int.Parse(mDict[Id].Value);
        }

        return 0;
    }

    public float GetFloat(string Id)
    {
        if (mDict[Id].Type == Types.Float)
        {
            return float.Parse(mDict[Id].Value);
        }

        return 0.0f;
    }

    void Start()
    {
        if (Values.Length > 0)
        {
            mDict = new Dictionary<string, TypeInfo>(Values.Length);
            for (int i = 0; i < Values.Length; ++i)
            {
                mDict.Add(Values[i].Id, Values[i].Info);
            }
        }
    }

    public static MasterParamManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = Load();
            Instance.Start();
        }

        return Instance;
    }

    static MasterParamManager Load()
    {
#if false
        string assetPath = AssetDirectory + typeof(MasterParamManager).Name + ".asset";
        MasterParamManager asset = (MasterParamManager)AssetDatabase.LoadAssetAtPath(assetPath, typeof(MasterParamManager));
        if (asset == null)
        {
            // 新しいアセットを作る
            asset = CreateInstance<MasterParamManager>();
            Save(asset, true);
        }
#endif
        string managerPath = AssetDirectory + typeof(MasterParamManager).Name + ".asset";
        MasterParamManager manager = (MasterParamManager)AssetDatabase.LoadAssetAtPath(managerPath, typeof(MasterParamManager));
        if (manager == null)
        {
            manager = CreateInstance<MasterParamManager>();
            Save<MasterParamManager>(manager, true);
        }

        for (int i = 0; i < manager.ParamNames.Length; ++i)
        {
            string assetPath = AssetDirectory + manager.ParamNames[i] + ".asset";
            MasterParam _asset = (MasterParam)AssetDatabase.LoadAssetAtPath(assetPath, typeof(MasterParam));
            if (_asset == null)
            {
                // 新しいアセットを作る
                _asset = CreateInstance<MasterParam>();
                _asset.name = manager.ParamNames[i];
                Save<MasterParam>(_asset, true);
            }
        }

        Flash();

        return manager;
    }

    static void Save<T>(T asset, bool notExist) where T : ScriptableObject
    {
        string assetPath = AssetDirectory + asset.name + ".asset";

        T _asset = null;
        if (!notExist)
        {
            _asset = (T)AssetDatabase.LoadAssetAtPath(assetPath, typeof(T));
        }

        if (_asset == null)
        {
            AssetDatabase.CreateAsset(asset, assetPath);
        }
    }

    static void Flash()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
