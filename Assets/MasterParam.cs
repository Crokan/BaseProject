using UnityEngine;
using System.Collections;

[System.Serializable]
public class MasterParam : ScriptableObject {

    public string ParamName = "";
    public MasterParamManager.SerializeTypeInfo Info;
}

[System.Serializable]
public class UnitParam : ScriptableObject
{

    public string ParamName = "UnitParam";
    public MasterParamManager.SerializeTypeInfo Info;
}