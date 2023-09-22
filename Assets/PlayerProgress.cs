using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(
    fileName = "Player Progress",
    menuName = "Game Kuis/Player Progress")]
public class PlayerProgress : ScriptableObject
{
    [System.Serializable]
    public struct MainData
    {
        public int koin;
        public Dictionary<string, int> progressLevel;
    }

    [SerializeField]
    private string _filename = "contoh.txt";

    public MainData progressData = new MainData();

    public void SimpanProgress()
    {
        // Sampel Data
        progressData.koin = 200;
        if (progressData.progressLevel == null)
            progressData.progressLevel = new();
        progressData.progressLevel.Add("Level Pack 1", 3);
        progressData.progressLevel.Add("Level Pack 3", 5);

        // Informasi penyimpanan data
        var directory = Application.dataPath + "/Temporary";
        var path = directory + "/" + _filename;

        // Membuat Directory Temporary
        // Jika directory belum ada sama sekali, maka buat directory baru.
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            Debug.Log("Directory has been Created: " + directory);
        }

        //Membuat file baru
        // Jika file belum ada sama sekali, maka buat file baru.
        if (File.Exists(path))
        {
            File.Create(path).Dispose();
            Debug.Log("File created: " + path);
        }

        // \n ditulis untuk menambahkan baris baru, atau sama dengan pencet "Enter" pada keyboard.
        //var konten = $"{progressData.koin}\n";
        var fileStream = File.Open(path, FileMode.Open);

        // Menyimpan data ke dalam file menggunakan binari writer
        var writer = new BinaryWriter(fileStream);

        writer.Write(progressData.koin);
        foreach (var i in progressData.progressLevel)
        {
            writer.Write(i.Key);
            writer.Write(i.Value);
        }

        //Putuskan aliran memori dengan File menggunakan Dispose, untuk menghindari memory leak.
        writer.Dispose();

        //foreach (var i in progressData.progressLevel)
        //{
        //    konten += $"{i.Key} {i.Value}\n";
        //}

        //File.WriteAllText(path, konten);

        Debug.Log($"{_filename} Berhasil Disimpan");
    }

    public void MuatProgress()
    {

    }
}
