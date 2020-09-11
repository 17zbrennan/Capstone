using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using Mono.Data;
using Mono.Data.Sqlite;

public class DatabaseController : MonoBehaviour {

    private Dropdown dropdown;
    private List<string> dropdownList;
    private Songs[] songList;
    private int currentSong;
	// Use this for initialization
	void Start () {
        dropdownList = new List<string>();
        currentSong = 0;
        songList = new Songs[10];
        dropdown = this.GetComponent<Dropdown>();
        DatabaseInfo();
        dropdown.AddOptions(dropdownList);
    }

    private void Update()
    {
        PickMusic();
    }

    private void PickMusic()
    {
        if (dropdown.value != currentSong )
        {
            Songs temp = songList[dropdown.value];
            GameManager.instance.PlayMusic(temp.getPath());
            currentSong = dropdown.value;
        }
        
    }
    //Connects to a database
    void DatabaseInfo()
    {
       //Opens the database connection to SQLite database
        using (IDbConnection connection = new SqliteConnection("URI=file:" + Application.streamingAssetsPath + "/Music.db"))
        {
            connection.Open();
            //Creates the command to select all information from the music database
            using (IDbCommand command = connection.CreateCommand())
            {
                //Query
                string query = "Select * FROM Music";
                //Sends database the command
                command.CommandText = query;

                //reads the database
                using (IDataReader dbReader = command.ExecuteReader())
                {
                    int i = 0;
                    //Puts all data into a list
                    while (dbReader.Read())
                    {
                       

                        //Concats the artist and music title
                        dropdownList.Add(dbReader.GetString(1) + " by " + dbReader.GetString(2));
                        //Makes a list of songs
                        songList[i] = new Songs(dbReader.GetString(1), dbReader.GetString(2), dbReader.GetString(3));
                        i++;
                    }
                    //Closes the read and database
                    connection.Close();
                    dbReader.Close();
                }
                
                
            }
        }
    }
	
}
