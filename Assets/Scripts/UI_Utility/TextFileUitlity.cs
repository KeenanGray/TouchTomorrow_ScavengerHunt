using UnityEngine;
using UnityEditor;
using System.IO;

public class TextFileUtility
{

    public static void WriteString(string path, string newLine)
    {
        //Write some text to the test.txt file
        var writer = new StreamWriter(path, true);
        writer.Write("\n"+newLine);
        writer.Close();
    }

    public static string ReadString(string path)
    {
        string readValue = "";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        readValue = reader.ReadToEnd();
        reader.Close();
        return readValue;
    }

    public static void CleanTextFile(string path)
    {

        //Write some text to the test.txt file
        var testStream = File.Create(path);
        testStream.Close();
    }

}