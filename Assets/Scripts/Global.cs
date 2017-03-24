using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour 
{
    public static long resources;

    public static float flySpeed; // The speed *camera* flying with plane, used to calculated FX.

    // Notice that these should be references to *prefebs*.
    // Should be assigned by reading the save or by the selecting interface.
    public static GameObject wing = null;
    public static GameObject[] weapons = new GameObject[4];
    public static GameObject[] devices = new GameObject[4];
}
