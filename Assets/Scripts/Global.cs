using UnityEngine;
using System.Collections.Generic;

using System.IO;
public class Global
{

    public static long resources;

    public static float flySpeed = 8.0f; // The speed *camera* flying with plane, used to calculated FX.

    // Notice that these should be references to *prefebs*.
    // Should be assigned by reading the save or by the selecting interface.
    public static GameObject wing = null;
    //public static GameObject[] weapons = new GameObject[4];
    //public static GameObject[] devices = new GameObject[4];

    public static List<string> weaponNames;
    public static List<string> deviceNames;



    /// <summary>
    /// Init all inforemation in global from file.
    /// The cross-platform implementation is reserved for an easier code.
    /// </summary>
    public void ReadFromFile()
    {
        weaponNames = new List<string>();
        deviceNames = new List<string>();

        FileStream f = new FileStream("./save.ini", FileMode.Open);
        if (f == null) { Debug.LogError("File open failed on loading!"); return; }
        StreamReader rd = new StreamReader(f);
        while (!rd.EndOfStream)
        {
            string inp = rd.ReadLine();
            inp.Replace(" ", "");
            inp.Replace("\t", "");
            int ct = 0;
            for (; ct != inp.Length && inp[ct] != '='; ct++) ;
            string name = inp.Substring(0, ct);
            string value = inp.Substring(ct + 1);
            Process(name, value);
        }
        f.Close();
    }

    void Process(string name, string value)
    {
        if (name.Contains("weapon") || name.Contains("Weapon"))
        {
            weaponNames.Add(value);
            return;
        }

        if (name.Contains("device") || name.Contains("Device"))
        {

            deviceNames.Add(value);
            return;
        }

        if (name.Contains("res") || name.Contains("Res"))
        {
            resources = int.Parse(value);
            return;
        }
    }

    public void SaveFile()
    {
        FileStream f = new FileStream("./save.ini", FileMode.CreateNew);
        if (f == null) { Debug.LogError("File open failed on saving!"); return; }
        StreamWriter wt = new StreamWriter(f);
        foreach (string name in weaponNames)
            wt.WriteLine("Weapon = " + name);
        foreach (string name in deviceNames)
            wt.WriteLine("Device = " + name);
        wt.WriteLine("Resource = " + resources);
        f.Close();
    }
}
