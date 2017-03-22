using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour 
{
    public struct PlayerInfo
    {
        string planeName;
        string planeType;

        long resources;

        string[] weapons;
        string[] devices;

        float flySpeed; // The speed camera flying with plane, used to calculated FX.
    };
    public static PlayerInfo playerInfo;



	void Awake()
	{
	
	}
	
	void Start() 
	{
	    
	}
	
	void Update() 
	{
	
	}
	
	void LateUpdate()
	{
	
	}
}
