using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.Collections;

public class HighScores : MonoBehaviour
{
    public int[] scores = new int[10];

    string currentDirectory;

    public string scoreFileName = "highscores.txt";

    void Start()
    {
        currentDirectory = Application.dataPath;
        Debug.Log("Our current directory is: " + currentDirectory);

        LoadScoresFromFile();
    }

    void Update()
    {
        
    }

    public void LoadScoresFromFile()
    {

        bool fileExist = File.Exists(currentDirectory + "//" + scoreFileName);
        if(fileExist == true)
        {
            Debug.Log("Found high score file " + scoreFileName);
        }
        else
        {
            Debug.Log("TheF file " + scoreFileName + " does not exist. No scores will be loaded", this);
            return;
        }

        //MAKE A NEW ARay of defult values. this ensures no old values stick around
        scores = new int[scores.Length];
        StreamReader fileReader = new StreamReader(currentDirectory + "//" + scoreFileName);
        //a counter to make sure we dont go past the end of our scores
        int scoreCount = 0;

        // whil loop wich runs as long as ther is data to be read and we havent reached the end of our scores aray.

        while (fileReader.Peek() != 0 && scoreCount < scores.Length)
        {
            string fileLine = fileReader.ReadLine();
            //parse that variable into an int first make a variable
            int readscore = -1;
            //try parse it
            bool didParse = int.TryParse(fileLine, out readscore);
            if (didParse)
            {
                scores[scoreCount] = readscore;
            }
            else
            {
                Debug.Log("Invalid line in scores file at " + scoreCount + ", using defult value.", this);
                scores[scoreCount] = 0;
            }
            scoreCount++;
        }
        //close strream
        fileReader.Close();
        Debug.Log("high score read from " + scoreFileName);

    }

    public void SaveScoresToFile()
    {
        //create a stream writer for our file path
        StreamWriter fileWriter = new StreamWriter(currentDirectory + "//" + scoreFileName);
        //write the lines to this file
        for (int i = 0; i < scores.Length; i++)
        {
            fileWriter.WriteLine(scores[i]);
        }

        //close the stream
        fileWriter.Close();
        //write log message
        Debug.Log("high score written to " + scoreFileName);

    }

    public void AddScore(int newScore)
    {
        //first up we find out what index it belongs at
        int desiredIndex = -1;
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] < newScore || scores[i] == 0)
            {
                desiredIndex = i;
                break;
            }
        }
        //if no desired index was found than the score sint high enough to get on the table, so abort
        if (desiredIndex < 0)
        {
            Debug.Log("score of " + newScore + " not highh enough for high scores list.", this);
            return;
        }

        //then move all of the scorse after index back by one position,
        for (int i = scores.Length - 1; i > desiredIndex; i--)
        {
            scores[i] = scores[i - 1];
        }
        //insert our new scor ein its place
        scores[desiredIndex] = newScore;
        Debug.Log("Score of " + newScore + " entered into high scores at position " + desiredIndex, this);

    }
}
