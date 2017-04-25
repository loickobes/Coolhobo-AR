using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Text;
using System.Threading;
//using System.Diagnostics;

public class SqliteDbManager {

	static private SQLiteDB db = null;
	static private string statsCreateQuery = "CREATE TABLE IF NOT EXISTS stats (id INTEGER primary key NOT NULL, deviceId varchar(50) NOT NULL, email varchar(100) NOT NULL, sessionId varchar(20) NOT NULL, label varchar(255), data varchar(255), creationDate date NOT NULL)";
	static private string emailsCreateQuery = "CREATE TABLE IF NOT EXISTS emails (id INTEGER primary key NOT NULL, deviceId varchar(50) NOT NULL, email varchar(100) NOT NULL, sessionId varchar(20) NOT NULL, creationDate date NOT NULL)";
	static private string surveysCreateQuery = "CREATE TABLE IF NOT EXISTS survey (id INTEGER primary key NOT NULL, deviceId varchar(50) NOT NULL, email varchar(100) NOT NULL, sessionId varchar(20) NOT NULL, question varchar(512), checked integer NOT NULL DEFAULT 0, creationDate date NOT NULL)";
	static private string uniqueId = "";
	//public Text logTexts;

	// Use this for initialization
	static public void Init () {
		if (uniqueId == "") {
			uniqueId = SystemInfo.deviceUniqueIdentifier;

			string dbPath = Application.persistentDataPath + "/darm_" + uniqueId + ".db";
			Debug.Log ("Db Path: " + dbPath);
			openOrCreateDb (dbPath);

			Debug.Log ("Database started...");
			insertStat ("------", "------", "startApp", "");
		}
	}
	

	// Check if db exists else create it
	static private void openOrCreateDb(string dbPath)
	{
		db = new SQLiteDB();

		if (!File.Exists (dbPath)) {
			try {
				SQLiteQuery qr;

				//
				// initialize database
				//
				db.Open (dbPath);

				qr = new SQLiteQuery (db, statsCreateQuery); 
				qr.Step ();												
				qr.Release (); 

				qr = new SQLiteQuery (db, emailsCreateQuery); 
				qr.Step ();												
				qr.Release (); 

				qr = new SQLiteQuery (db, surveysCreateQuery); 
				qr.Step ();												
				qr.Release ();
				Debug.Log("Database created and opened");
				
			} catch (Exception e) {
				Debug.Log ("Error during db initialization " + e.ToString ());
				//logTexts.text += "\nError during db initialization " + e.ToString ();
			}
		} else {
			try {
				db.Open (dbPath);
				Debug.Log("Database opened");
			}
			catch (Exception e) {
				Debug.Log ("Error during db opening " + e.ToString());
				//logTexts.text += "\nError during db opening " + e.ToString();
			}
		}
	}

	static public void insertStat(string email, string sessionId, string label, string data)
	{
		SQLiteQuery qr;
		string queryInsertStats = "INSERT INTO stats (deviceId, email, sessionId, label, data, creationDate) VALUES(?,?,?,?,?,?);";

		string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

		qr = new SQLiteQuery (db, queryInsertStats); 
		qr.Bind(uniqueId);
		qr.Bind(email);
		qr.Bind(sessionId);
		qr.Bind(label);
		qr.Bind(data);
		qr.Bind (currentDateTime);
		qr.Step ();												
		qr.Release ();
	}

	static public void insertEmail(string email, string sessionId)
	{
		SQLiteQuery qr;
		string queryInsertStats = "INSERT INTO emails (deviceId, email, sessionId, creationDate) VALUES(?,?,?,?);";

		string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

		qr = new SQLiteQuery (db, queryInsertStats); 
		qr.Bind(uniqueId);
		qr.Bind(email);
		qr.Bind(sessionId);
		qr.Bind (currentDateTime);
		qr.Step ();												
		qr.Release ();
	}

	static public void insertSurvey(string email, string sessionId, string question, int status)
	{
		SQLiteQuery qr;
		string queryInsertStats = "INSERT INTO survey (deviceId, email, sessionId, question, checked, creationDate) VALUES(?,?,?,?,?,?);";

		string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

		qr = new SQLiteQuery (db, queryInsertStats); 
		qr.Bind(uniqueId);
		qr.Bind(email);
		qr.Bind(sessionId);
		qr.Bind(question);
		qr.Bind(status);
		qr.Bind (currentDateTime);
		qr.Step ();												
		qr.Release ();
	}

	static public string exportDb()
	{
		string json = "";
		json += "{";
		json += "   \"uniqueId\": \"" + uniqueId + "\", ";

		//**************** Emails *******************
		json += "   \"emails\": [";

		SQLiteQuery qr;
		string query = "SELECT * FROM emails";

		int cpt = 0;
		qr = new SQLiteQuery (db, query);
		while( qr.Step() )
		{
			if (cpt > 0)
			{ json += ", "; }

			json += "    {\"id\": \""+qr.GetInteger("id")+"\", \"deviceId\": \""+qr.GetString("deviceId")+"\", \"email\": \""+qr.GetString("email")+"\", \"sessionId\": \""+qr.GetString("sessionId")+"\", \"creationDate\": \""+qr.GetString("creationDate")+"\" }";

			cpt++;
		}
		qr.Release();
		json += "   ],";


		//**************** Stats *******************
		json += "   \"stats\": [";
		
		//SQLiteQuery qr;
		query = "SELECT * FROM stats";
		
		cpt = 0;
		qr = new SQLiteQuery (db, query);
		while( qr.Step() )
		{
			if (cpt > 0)
			{ json += ", "; }
			
			json += "    {\"id\": \""+qr.GetInteger("id")+"\", \"deviceId\": \""+qr.GetString("deviceId")+"\", \"email\": \""+qr.GetString("email")+"\", \"label\": \""+qr.GetString("label")+"\", \"data\": \""+qr.GetString("data")+"\", \"sessionId\": \""+qr.GetString("sessionId")+"\", \"creationDate\": \""+qr.GetString("creationDate")+"\" }";
			
			cpt++;
		}
		qr.Release();
		json += "   ],";


		//**************** Survey *******************
		json += "   \"survey\": [";
		
		//SQLiteQuery qr;
		query = "SELECT * FROM survey";
		
		cpt = 0;
		qr = new SQLiteQuery (db, query);
		while( qr.Step() )
		{
			if (cpt > 0)
			{ json += ", "; }
			
			json += "    {\"id\": \""+qr.GetInteger("id")+"\", \"deviceId\": \""+qr.GetString("deviceId")+"\", \"email\": \""+qr.GetString("email")+"\", \"question\": \""+qr.GetString("question")+"\", \"checked\": \""+qr.GetInteger("checked")+"\", \"sessionId\": \""+qr.GetString("sessionId")+"\", \"creationDate\": \""+qr.GetString("creationDate")+"\" }";
			
			cpt++;
		}
		qr.Release();
		json += "   ]";

		json += "}";

		return json;
	}
}
