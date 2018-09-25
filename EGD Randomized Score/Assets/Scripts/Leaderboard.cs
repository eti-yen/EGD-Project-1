using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Leaderboard : MonoBehaviour 
{
	[SerializeField] string scoreFileName = "scores.txt";
	[SerializeField] TextAsset initialScoreFile;
	[SerializeField] bool randomize = true;

	int numScores;
	List<string> names;
	List<int> scores;

	string scorePath;
	FileStream scoreFile;

	// Use this for initialization
	void Start() 
	{
		scorePath = Path.Combine(Application.persistentDataPath, scoreFileName);
		if (File.Exists(scorePath))
		{
			scoreFile = new FileStream(scorePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
		}
		else
		{
			scoreFile = new FileStream(scorePath, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.ReadWrite);
			StreamWriter writer = new StreamWriter(scoreFile);
			writer.Write(initialScoreFile.text);
			writer.Flush();
			writer.Close();
			scoreFile.Close();
			scoreFile = new FileStream(scorePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
		}
		ReadScores();
	}

	void ReadScores()
	{
		// Make File Reader
		StreamReader reader = new StreamReader(scoreFile);
		// First line should be a number telling us how many scores/names there are.
		int.TryParse(reader.ReadLine(), out numScores);
		names = new List<string>(numScores);
		scores = new List<int>(numScores);
		// Read all names
		for (int i = 0; i < numScores; ++i)
		{
			names.Add(reader.ReadLine());
		}
		// Read all scores
		for (int i = 0; i < numScores; ++i)
		{
			int score;
			int.TryParse(reader.ReadLine(), out score);
			scores.Add(score);
		}
		reader.Close();
	}

	// Returns the randomly reassigned score, or the score if not randomizing.
	public int AddScore(string name, int score)
	{
		int scoreReturn = score;
		// Put new name and score in.
		numScores++;
		names.Add(name);
		scores.Add(score);

		// Sort scores in descending order
		scores.Sort();
		scores.Reverse();

		// Switch the new name with a random name (chance that it will just switch with itself).
		if (randomize)
		{
			int switchIndex = Random.Range(0, numScores);
			string nameSwitch = names[switchIndex];
			names[numScores - 1] = nameSwitch;
			names[switchIndex] = name;
			scoreReturn = scores[switchIndex];
		}
		SaveScores();
		return score;
	}

	void SaveScores()
	{
		// Create a new file to overwrite the old one - not elegant, but less mistake-prone
		scoreFile.Close();
		scoreFile = new FileStream(scorePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
		StreamWriter writer = new StreamWriter(scoreFile);

		// First line is how many scores/names.
		writer.WriteLine(numScores);

		// Next write all names.
		for (int i = 0; i < numScores; ++i)
			writer.WriteLine(names[i]);

		// Next write all scores (should already be in descending order)
		for (int i = 0; i < numScores; ++i)
			writer.WriteLine(scores[i]);

		writer.Flush();
		writer.Close();
	}

	public string GetNameList()
	{
		string list = "";
		foreach (string s in names)
		{
			list += s + "\n";
		}
		return list;
	}

	public string GetScoreList()
	{
		string list = "";
		foreach (int s in scores)
		{
			list += s + "\n";
		}
		return list;
	}
}
