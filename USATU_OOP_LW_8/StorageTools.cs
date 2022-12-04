using System;
using System.IO;
using System.Text;

namespace USATU_OOP_LW_8;

public class StorageTools
{
    private string _fileName;

    public StorageTools(string fileName) => _fileName = fileName;

    public bool IsFileExists()
    {
        return File.Exists(_fileName);
    }

    public StringReader GetFormattedDataFromStorage()
    {
        var readText = File.ReadAllText(_fileName);
        readText = readText.Replace("\t", "");
        while (readText.Contains(Environment.NewLine + Environment.NewLine))
        {
            readText = readText.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine);
        }

        var stringReader = new StringReader(readText);
        var formattedText = new StringBuilder();
        while (stringReader.ReadLine() is { } line)
        {
            line = line.Substring(line.LastIndexOf(':') + 2);
            formattedText.AppendLine(line);
        }

        return new StringReader(formattedText.ToString());
    }

    public void WriteDataToStorage(string data)
    {
        File.WriteAllText(_fileName, data);
    }
}