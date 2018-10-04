/*
	Copyright(C) 2018 Stelio Kontos <steliokontosxbl@gmail.com>

	This file is part of FuckBookFitler.

	This program is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program. If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using Newtonsoft.Json.Linq;
using FuckBookFitler.FuckinModels;

namespace FuckBookFitler
{
	public partial class Form1 : Form
	{
		internal static List<string> Wordlist = new List<string>();
		internal static List<FBEntryParsed> FBEntriesOriginal = new List<FBEntryParsed>();
		internal static List<FBEntryParsed> FBEntries = new List<FBEntryParsed>();
		internal static string logpath = String.Empty;

		public Form1()
		{
			InitializeComponent();
			this.AllowDrop = true;
			this.DragDrop += new DragEventHandler(this.Form1_DragDrop);
			this.DragEnter += new DragEventHandler(this.Form1_DragEnter);
			this.DragLeave += new EventHandler(this.Form1_DragLeave);
		}

		private void Form1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
				e.Effect = DragDropEffects.All;
			else
				e.Effect = DragDropEffects.None;

			labelFileDrop.Text = "...now drop them";
			labelFileDrop.ForeColor = Color.FromArgb(0, 90, 190);
		}

		private void Form1_DragLeave(object sender, EventArgs e)
		{
			labelFileDrop.Text = @"WTF? You have to drop them..." + Environment.NewLine + @"Let's try again:" + Environment.NewLine + @"Drag AND drop files here.";
			labelFileDrop.ForeColor = Color.FromArgb(140, 140, 140);
		}

		private void Form1_DragDrop(object sender, DragEventArgs e)
		{
			string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

			for (int i = 0; i < s.Length; i++)
			{
				ReceivedFileController(s[i]);
			}

			labelFileDrop.Text = @"Oh ye, yummy...mmmmm..." + Environment.NewLine + @"Got more for me?" + Environment.NewLine + @"Drag them here.";
			labelFileDrop.ForeColor = Color.FromArgb(140, 140, 140);
		}

		private void btnQueryByWordlist_Click(object sender, EventArgs e)
		{
			if (FBEntriesOriginal.Count.Equals(0))
			{
				Console.WriteLine("You haven't given me any shit to fitler, dumbass.");
				return;
			}

			new Thread(delegate ()
			{
				string _logpathSfx = @" Fitlered by wordlist.log";
				StringBuilder sb = new StringBuilder();

				Console.WriteLine("Crunching your shit...");
				Console.WriteLine("Be fucking patient....");

				QueryByWordlist();

				Console.WriteLine("Generating log file containing positive matches...");

				//todo make temp list of matches to return stats (popular cuss words, percentage of posts are dirty, etc)
				//todo probably don't do that ^ because it's cool but pointless
				sb.AppendLine(Utils.PrintHR('=', 40));
				sb.AppendLine($"{FBEntriesOriginal.Count} entries fitlered against {Wordlist.Count} words from wordlist.");
				sb.AppendLine($"{FBEntries.Count(x => x.words_matched.Count > 0)} potentially offensive entries found.");
				sb.AppendLine();

				Console.WriteLine(sb);

				LogGenerator(_logpathSfx, sb, true);
			}).Start();
		}

		private void btnQueryByInput_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrWhiteSpace(textBox1.Text))
			{
				Console.WriteLine("You didn't type anything in the fucking fitler box, stupid.");
				return;
			}

			if (FBEntriesOriginal.Count.Equals(0))
			{
				Console.WriteLine("You haven't given me any shit to fitler, dumbass.");
				return;
			}

			new Thread(delegate ()
			{
				string keyword = textBox1.Text;
				string _logpathSfx = @" Fitlered by " + keyword + @".log";
				StringBuilder sb = new StringBuilder();

				Console.WriteLine("Crunching your shit...");
				Console.WriteLine("Be fucking patient....");

				QueryByInput(keyword);

				Console.WriteLine("Generating log file containing positive matches...");

				sb.AppendLine(Utils.PrintHR('=', 40));
				sb.AppendLine($"{FBEntriesOriginal.Count} entries fitlered.");
				sb.AppendLine($"{FBEntries.Count(x => x.words_matched.Count > 0)} entries found containing \"{keyword}\".");
				sb.AppendLine();

				Console.WriteLine(sb);

				LogGenerator(_logpathSfx, sb, true);
			}).Start();
		}

		private static void ReceivedFileController(string filepath)
		{
			Console.WriteLine($"Received file: {filepath}");

			if (Path.GetFileName(filepath).Equals("wordlist.txt"))
			{
				logpath = Path.GetFullPath(filepath).Replace("wordlist.txt", "");
				Console.WriteLine("Logpath set to: " + logpath);

				ParseWordlistAsLines(filepath);
			}
			else if (Path.GetExtension(filepath).Equals(".json"))
			{
				ParseEntriesAsJson(filepath);
			}
			else
			{
				Console.WriteLine($"WTF file is this nigga?" + Environment.NewLine);
			}
		}

		private static void ParseWordlistAsLines(string filepath)
		{
			Console.Write("Building wordlist...");

			string[] wordlist = File.ReadAllLines(filepath);
			Wordlist.Clear();
			Wordlist.AddRange(wordlist);

			Console.WriteLine($"{wordlist.Count()} words added to list." + Environment.NewLine);
		}

		private static void ParseEntriesAsJson(string filepath)
		{
			string jsonString = File.ReadAllText(filepath, Encoding.UTF8);
			List<FBEntryParsed> fbEntries = new List<FBEntryParsed>();

			JArray a = JArray.Parse(jsonString);
			Console.WriteLine($"Analyzing {a.Count} entries...");

			foreach (JToken o in a)
			{
				if (o.SelectToken("data[0].post") != null)
				{
					var entry = new FBEntryParsed();
					entry.timestamp = Utils.UnixTimestampToDateTime((double)o.SelectToken("timestamp", false), DateTimeKind.Local);
					entry.title = (string)o.SelectToken("title", false);
					entry.content = (string)o.SelectToken("data[0].post", false);
					entry.group = null;
					entry.post_author = null; //todo parse out post author
					entry.uri = null; //todo generate link for entry on fb
					entry.entry_type = FBEntryType.Post;
					entry.words_matched = new List<string>();
					fbEntries.Add(entry);
				}
				else if (o.SelectToken("data[0].comment.comment") != null)
				{
					var entry = new FBEntryParsed();
					entry.timestamp = Utils.UnixTimestampToDateTime((double)o.SelectToken("data[0].comment.timestamp", false), DateTimeKind.Local);
					entry.title = (string)o.SelectToken("title", false);
					entry.content = (string)o.SelectToken("data[0].comment.comment", false);
					entry.author = (string)o.SelectToken("data[0].comment.author", false);
					entry.group = (string)o.SelectToken("data[0].comment.group", false);
					entry.post_author = null; //todo parse out post author
					entry.uri = null; //todo generate link for entry on fb
					entry.entry_type = FBEntryType.Comment;
					entry.words_matched = new List<string>();
					fbEntries.Add(entry);
				}
				else
					Console.Write(".");
			}

			foreach (FBEntryParsed p in fbEntries)
			{
				if (!FBEntriesOriginal.Exists(x => x.timestamp == p.timestamp))
					FBEntriesOriginal.Add(p);
			}

			Console.Write(Environment.NewLine);
			Console.WriteLine($"{FBEntriesOriginal.Count(x => x.entry_type == FBEntryType.Post)} posts indexed.");
			Console.WriteLine($"{FBEntriesOriginal.Count(x => x.entry_type == FBEntryType.Comment)} comments indexed.");
			Console.WriteLine(Environment.NewLine);
		}

		private static void QueryByWordlist()
		{
			FBEntries.Clear();
			FBEntries.AddRange(FBEntriesOriginal);
			foreach (FBEntryParsed f in FBEntries)
			{
				List<string> _matches = new List<string>();

				foreach (string b in Wordlist)
				{
					if (Regex.IsMatch(f.content, String.Format(@"\b{0}\b", Regex.Escape(b)), RegexOptions.IgnoreCase))
						_matches.Add(b);
				}

				_matches.AddRange(f.words_matched);

				List<string> distinct = _matches.Distinct().ToList();

				f.words_matched.Clear();
				f.words_matched.AddRange(distinct);
			}
		}

		private static void QueryByInput(string keyword)
		{
			FBEntries.Clear();
			FBEntries.AddRange(FBEntriesOriginal);
			foreach (FBEntryParsed f in FBEntries)
			{
				List<string> _matches = new List<string>();

				if (Regex.IsMatch(f.content, String.Format(@"\b{0}\b", Regex.Escape(keyword)), RegexOptions.IgnoreCase))
					_matches.Add(keyword);

				f.words_matched.Clear();
				f.words_matched.AddRange(_matches);
			}
		}

		private static void LogGenerator(string filenameSuffix, StringBuilder infoText, bool openFileOnComplete)
		{
			StringBuilder sb = new StringBuilder();
			string _logpath = logpath + DateTime.Now.ToString("yyyy-MM-MMM hhmm") + filenameSuffix;

			List<FBEntryParsed> fitlered = FBEntries.FindAll(x => x.words_matched.Count > 0).OrderBy(x => x.timestamp.Ticks).ToList();
			sb.Append(infoText);

			if (fitlered.Count > 0)
			{
				foreach (FBEntryParsed f in fitlered)
				{
					sb.AppendLine(Utils.PrintHR('_', 40));
					if (f.timestamp != null)
						sb.Append(f.timestamp.ToString() + " ");
					sb.AppendLine("(" + f.entry_type.ToString() + ")");
					if (f.title != null)
						sb.AppendLine(f.title);
					if (f.content != null)
						sb.AppendLine(f.content);
					if (f.group != null)
						sb.AppendLine(f.group);
					if (f.post_author != null)
						sb.AppendLine(f.post_author);
					if (f.uri != null)
						sb.AppendLine(f.uri);
					sb.Append("Matches: ");
					foreach (string m in f.words_matched)
						sb.Append(m + ", ");
					sb.Remove(sb.Length - 2, 2);
					sb.AppendLine();
				}
			}
			else
				sb.AppendLine("Squeeky clean. Not one shit line to fucking fitler. Motherfucker...");

			File.AppendAllText(_logpath, sb.ToString());

			Console.WriteLine($"Log file generated at {_logpath}." + Environment.NewLine);

			if (openFileOnComplete)
				Process.Start(_logpath);
		}
	}
}
