using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class db_controller : MonoBehaviour
{
    private string conStr;
    public SqliteConnection con;
    public SqliteCommand cmd_db;
    public SqliteDataReader rdr;
    public Text text;

    string id;
    string log;
    string pass;

    public InputField idfield;
    public InputField login;
    public InputField password;

    public Text txtId;
    public Text txtLogin;
    public Text txtPass;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void connection()
    {
        try
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                conStr = Application.dataPath + "/dbMG.db";
            }
            else
            {
                conStr = Application.persistentDataPath + "/dbMG.db";
                if (!File.Exists(conStr))
                {
                    WWW load = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "dbMG.db");
                    while (!load.isDone) { }
                    File.WriteAllBytes(conStr, load.bytes);
                }
            }

            con = new SqliteConnection("URI=file:" + conStr);
            con.Open();
     
        }
        catch (Exception ex)
        {
            text.text = ex.ToString();
        }

    }

    public void Disconnect(SqliteConnection con_db)
    {
        con.Close();
    }

    public void Disconnect(SqliteConnection con_db, SqliteDataReader r)
    {
        con.Close();
        r.Close();
        r = null;
    }

    public void Disconnect(SqliteConnection con_db, SqliteDataReader r, SqliteCommand c)
    {
        con.Close();

        r = null;
        cmd_db.Dispose();
    }

    
    public SqliteDataReader ExecuteReader(String query)
    {
        connection();
        try
        {
            cmd_db = new SqliteCommand(query, con);
            rdr = cmd_db.ExecuteReader();
            if(rdr.HasRows)
            {
                return rdr;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            return null;
        }
    }

    public void toreg()
    {
        Application.LoadLevel("register");
    }

    public void UpdateData(string querry)
    {
        connection();
        try
        {
            cmd_db = new SqliteCommand(querry, con);
            cmd_db.ExecuteNonQuery();
        }
        catch(Exception ex)
        {
            print(ex.ToString());
        }
    }
}
