﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Songs {


    public string SongName;

    public string SongArtist;

    public string SongPath;

    public Songs( string name, string artist, string path){

        this.SongName = name;
        this.SongArtist = artist;
        this.SongPath = path;
    }

    public string getName()
    {
        return SongName;
    }
    public string getArtist()
    {
        return SongArtist;
    }
    public string getPath()
    {
        return SongPath;
    }

}
