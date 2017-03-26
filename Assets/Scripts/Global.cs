using UnityEngine;
using System.Collections;

using System.IO;
public class Global
{
    public static long resources;

    public static float flySpeed = 8.0f; // The speed *camera* flying with plane, used to calculated FX.

    // Notice that these should be references to *prefebs*.
    // Should be assigned by reading the save or by the selecting interface.
    public static GameObject wing = null;
    public static GameObject[] weapons = new GameObject[4];
    public static GameObject[] devices = new GameObject[4];




    /// <summary>
    /// Init all inforemation in global from file.
    /// The cross-platform implementation is reserved for an easier code.
    /// </summary>
    public void ReadFromFile()
    {
        FileStream f = new FileStream("./save.ini", FileMode.Open);
        if (f == null) { Debug.LogError("File open Failed!"); return; }
        StreamReader rd = new StreamReader(f);
        while (!rd.EndOfStream)
        {
            string inp = rd.ReadLine();
            inp.Replace(" ", "");
            inp.Replace("\t", "");
            int ct = 0;
            for (; ct != inp.Length && inp[ct] != '='; ct++) ;
            //string name = inp.Substring(0, ct);
            //string value = inp.Substring(ct+1);
        }
    }

}
