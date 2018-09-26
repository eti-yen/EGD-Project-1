using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using UnityEngine;

public class SendData : MonoBehaviour
{
	public static int timesRegenerated = 0;
	private string playerName = "der";
	private static int playerNum = 0;
	private static string ip = "127.0.0.1";
	private const int port = 8192;

	public void SetPlayerName(string name)
	{
		playerName = name.Replace(' ', '_');
	}

	public static void SetIP(string ipAddress)
	{
		ip = ipAddress;
	}

	public void SendScore()
	{
		try
		{
			TcpClient client = new TcpClient(ip, port);
			NetworkStream stream = client.GetStream();

			string actualScoreEvent = playerName + " " + playerNum + " regen " + timesRegenerated;
			byte[] data = Encoding.ASCII.GetBytes(actualScoreEvent + '\n');
			stream.Write(data, 0, data.Length);

			stream.Close();
			client.Close();
		}
		catch (Exception) { }
		playerNum = 1 - playerNum;
		timesRegenerated = 0;
	}

}
