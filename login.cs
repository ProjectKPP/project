using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class login : MonoBehaviour {

    private db_controller _dbctrl = new db_controller();
    public SqliteDataReader dataread;
    public string query;

    public InputField logininput;
    public InputField passinput;
    public Text text;

    string log;
    string pass;

    public void logintog()
    {
        log = logininput.text.ToString();
        pass = passinput.text.ToString();

        query = "Select id from User where login='" + log + "'And pass ='" + pass + "'";
        try
        {
            dataread = _dbctrl.ExecuteReader(query);
            if(dataread.HasRows & dataread != null)
            {
                while (dataread.Read())
                {
                    Application.LoadLevel("Lvl1");
                }
            }
            else
            {
                text.text = "Error login, please retry";
            }

        }
        catch(Exception ex)
        {

        }
    }
}
