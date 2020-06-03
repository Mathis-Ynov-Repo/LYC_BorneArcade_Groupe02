using UnityEngine;
using System.Data;
using System;
using System.Collections;
using Mono.Data.Sqlite;
using System.IO;
using System.Collections.Generic;

public class HighScoreManager : MonoBehaviour
{

	private string connectionString;

	private List<HighScore> highScores = new List<HighScore>();

	public GameObject scorePrefab;

	public Transform scoreParent;

	// Use this for initialization
	void Start()
	{
		connectionString = "URI=file:" + Application.persistentDataPath + "/" + "GAME_DB.db";
		Debug.Log(connectionString);

		showScores();

	}

	private void GetScores()
	{
		highScores.Clear();
		using (IDbConnection dbConnection = new SqliteConnection(connectionString))
		{
			dbConnection.Open();

			using (IDbCommand dbCmd = dbConnection.CreateCommand())
			{
				string sqlQuery = "SELECT * FROM HighScores ORDER BY Score DESC limit 10";

				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader())
				{
					while (reader.Read())
					{
						highScores.Add(new HighScore(reader.GetInt32(0), reader.GetInt32(2), reader.GetString(1), reader.GetDateTime(3)));
						Debug.Log(reader.GetString(1));
					}

					dbConnection.Close();
					reader.Close();
				}
			}
		}

	}

	private void showScores()
    {
		GetScores();
		for (int i = 0; i< highScores.Count; i++)
        {
			GameObject tmpObject = Instantiate(scorePrefab);

			HighScore tmpScore = highScores[i];

			tmpObject.GetComponent<HighScoreScript>().SetScore(tmpScore.Name, tmpScore.Score.ToString(), "#" + (i + 1).ToString());

			tmpObject.transform.SetParent(scoreParent);
        }
    }
}
