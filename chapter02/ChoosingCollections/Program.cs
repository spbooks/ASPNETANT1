using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChoosingCollectionsDemo
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Using an array");
			Console.WriteLine("------------------------------");
			byte[] data = new byte[] { 0x0f, 0x0e, 0x13 };
			data[0] = 0xff; // random access
			//Now we can put that byte array in a memory stream...
			MemoryStream stream = new MemoryStream(data);
			Console.WriteLine();

			Console.WriteLine("Using an ArrayList");
			Console.WriteLine("------------------------------");
			ArrayList items = new ArrayList(new byte[] { 0x0f, 0x0e, 0x13 });
			items[0] = 0x00; // Random access
			items.Add(0xff); // Dynamically growing the ArrayList
			Console.WriteLine();

			Console.WriteLine("Using a generic List class");
			Console.WriteLine("------------------------------");
			List<byte> listitems = new List<byte>(
			new byte[] { 0x0f, 0x0e, 0x13 });
			listitems[0] = 0x00;
			listitems.Add(0xff);
			Console.WriteLine();

			Console.WriteLine("Using a generic list of ints");
			Console.WriteLine("------------------------------");
			List<int> scores = new List<int>(new int[] { 962, 175, 238 });
			Console.WriteLine("At index 0 we have: " + scores[0]);
			scores.Insert(0, 23);
			Console.WriteLine(scores[1] + " is now t index 1.");
			Console.WriteLine();


			Console.WriteLine("Using a Hashtable");
			Console.WriteLine("------------------------------");
			Hashtable moreScores = new Hashtable();
			moreScores.Add("Phil", 196); // boxing occurs
			moreScores.Add("Jon", 250);
			moreScores.Add("Scott", 750);
			moreScores.Add("Jeff", 901);
			Console.WriteLine("Phil's Score is: " + moreScores["Phil"]);
			Console.WriteLine();

			Console.WriteLine("Using a Dictionary");
			Console.WriteLine("------------------------------");
			Dictionary<string, int> evenMoreScores = new Dictionary<string, int>();
			evenMoreScores.Add("Phil", 196); //no boxing occurs.
			evenMoreScores.Add("Jon", 250);
			evenMoreScores.Add("Scott", 750);
			evenMoreScores.Add("Jeff", 901);
			Console.WriteLine("Phil's Score is: " + evenMoreScores["Phil"]);
			Console.WriteLine();

			SortedList<string, int> yetMoreScores = new SortedList<string, int>();
			yetMoreScores.Add("Phil", 196);
			yetMoreScores.Add("Jon", 250);
			yetMoreScores.Add("Scott", 750);
			yetMoreScores.Add("Jeff", 901);
			Console.WriteLine("Scores in alphabetical order");
			foreach (string key in yetMoreScores.Keys)
			{
				Console.WriteLine("{0}: {1}", key, yetMoreScores[key]);
			}
			// I can still access score by key.
			Console.WriteLine("Phil's Score is: " + yetMoreScores["Phil"]);
			Console.WriteLine();

			//Demo of using a custom IComparer
			SortedList<string, int> sortedScores = new SortedList<string, int>(new KeyLengthComparer());
		}
	}

	public class KeyLengthComparer : IComparer<string>
	{
		public int Compare(string x, string y)
		{
			return x.Length.CompareTo(y.Length);
		}
	}
}
