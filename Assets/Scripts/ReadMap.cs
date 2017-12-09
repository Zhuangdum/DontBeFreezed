using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class csvConvert
{
	public static int[,] loadMap(string configpath ) {
		StreamReader sr = null;//根据路径打开文件
		List<string[]> arrayData = new List<string[]> ();

		try {
			string file_url = configpath;

			sr = File.OpenText (file_url);
			Debug.Log ("File Find in " + file_url);
		} catch {
			Debug.Log ("File cannot find ! ");
		}
			
		string line;
		while ((line = sr.ReadLine ()) != null) {   //按行读取
			arrayData.Add (line.Split (';')); 
		}
		sr.Close ();
		sr.Dispose ();

		int col = arrayData.Count;
		int row = arrayData [0].Length;

		int[,] map = new int[col,row];
		for (int i = 0; i < arrayData.Count; i++) {
			for (int j = 0; j < arrayData[i].Length; j++) {
				int m = 0;
				try
				{
					m = Int32.Parse(arrayData[i][j]);
				}
				catch (FormatException e)
				{
					Console.WriteLine(e.Message);
				}
				map[i,j] = m;
			}
		}
		return map;
	}
}
