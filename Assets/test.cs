using UnityEngine;
using System.Collections;


public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        MasterParamManager instance = MasterParamManager.GetInstance();
        Debug.Log("INT : " + instance.GetInt("INT_10"));
        //Debug.Log("float : " + instance.GetFloat("FLOT_05"));
        //Debug.Log("string : " + instance.GetString("STRING_TEST"));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
