using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class reg : MonoBehaviour {

    private db_controller _dbctrl = new db_controller();
    private string querry;
    private string querrycheck;
    public InputField log;
    public InputField pass;
    public Text text;
    private SqliteDataReader dr;

    public void registr()
    {
        string _log = log.text.ToString();
        string _pass = pass.text.ToString();

        querry = "Insert into User (login,pass) values ('" + _log + "','" + _pass + "')";
        querrycheck = "Select login from User where login='" + _log + "'";

        if (_log != "")
        {
            dr = _dbctrl.ExecuteReader(querrycheck);
            if (dr == null)
            {
                _dbctrl.UpdateData(querry);
                _dbctrl.Disconnect(_dbctrl.con, dr, _dbctrl.cmd_db);
                Application.LoadLevel("q");
            }
            else
            {
                _dbctrl.Disconnect(_dbctrl.con, dr, _dbctrl.cmd_db);
                text.text = "Login is already in use";
            }
        }
        else
        {
            text.text = "Enter login!!!";
        }
    }
}
