using UnityEngine;
using System.Data;
using System;
using System.Collections;
using Mono.Data.Sqlite;
using System.IO;

public class DB : MonoBehaviour
{

	private string connectionString;

	// Use this for initialization
	void Start()
	{
		connectionString = "URI=file:" + Application.dataPath + "/" + "GAME_DB.db";

		//InsertScore("Kayne", 111);

		GetScores();

	}

	private void GetScores()
    {

		using(IDbConnection dbConnection = new SqliteConnection(connectionString))
        {
			dbConnection.Open();

			using (IDbCommand dbCmd = dbConnection.CreateCommand())
			{
				string sqlQuery = "SELECT * FROM HighScores";

				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader())
                {
					while (reader.Read())
                    {
						Debug.Log(reader.GetString(1));
                    }

					dbConnection.Close();
					reader.Close();
                }
            }
        }

    }

	public void InsertScore(string name, int newScore)
    {
		using (IDbConnection dbConnection = new SqliteConnection(connectionString))
		{
			dbConnection.Open();

			using (IDbCommand dbCmd = dbConnection.CreateCommand())
			{
				string sqlQuery = "INSERT INTO HighScores(Pseudo,Score) VALUES ('"+name+"', "+newScore+")";

				dbCmd.CommandText = sqlQuery;
				dbCmd.ExecuteNonQuery();

				dbConnection.Close();

			}
		}

	}
	// Update is called once per frame
	void Update()
	{

	}
}
