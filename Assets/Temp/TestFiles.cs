using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFiles : MonoBehaviour
{
    // private string fileName = "testFile";
    [SerializeField] private TextAsset textAsset;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        // List<string> lines = FileManager.ReadTextAsset(fileName, false);
        List<string> lines = FileManager.ReadTextAsset(textAsset, false);

        foreach (string line in lines)
        {
            Debug.Log(line);
        }

        yield return null;
    }
}
