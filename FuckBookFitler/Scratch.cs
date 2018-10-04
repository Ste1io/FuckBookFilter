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

using FuckBookFitler.FuckinModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FuckBookFitler
{
	class Scratch
	{
		/// <summary>
		/// Facebook data models (incomplete).
		/// </summary>
		public class FBEntry
		{
			public int timestamp { get; set; }
			public IList<FuckinAttachments> attachments { get; set; }
			public IList<FuckinData> data { get; set; }
			public string title { get; set; }
			public IList<string> tags { get; set; }
			public string post_author { get; set; }
			public string query_comment_uri { get; set; }
			public FBEntryType entry_type { get; set; }
		}

		public class FuckinAttachments
		{
			public IList<FuckinData> data { get; set; }
		}

		public class FuckinTags
		{
			public IList<FuckinData> data { get; set; }
		}

		public class FuckinData
		{
			[JsonProperty("post")]
			public string post { get; set; }
			public FuckinComment comment { get; set; }
			public FuckinMedia media { get; set; }
			public FuckinExternalContext external_context { get; set; }
			public string name { get; set; }
		}

		public class FuckinMedia
		{
			public string uri { get; set; }
			public FuckinMediaMetaData media_metadata { get; set; }
			public IList<FuckinComment> comments { get; set; }
			public int? creation_timestamp { get; set; }
			public FuckinThumbnail thumbnail { get; set; }
		}

		public class FuckinComment
		{
			public int timestamp { get; set; }
			public string comment { get; set; }
			public string author { get; set; }
			public string group { get; set; }
		}

		public class FuckinMediaMetaData
		{
			public FuckinPhotoMetaData photo_metadata { get; set; }
			public FuckinVideoMetaData video_metadata { get; set; }
		}

		public class FuckinPhotoMetaData
		{
			public int? iso_speed { get; set; }
			public int? orientation { get; set; }
			public int? original_width { get; set; }
			public int? original_height { get; set; }
			public int? taken_timestamp { get; set; }
			public int? modified_timestamp { get; set; }
			public string upload_ip { get; set; }
			public double? latitude { get; set; }
			public double? longitude { get; set; }
		}

		public class FuckinVideoMetaData
		{
			public int upload_timestamp { get; set; }
			public string upload_ip { get; set; }
			public string title { get; set; }
		}

		public class FuckinThumbnail
		{
			public string uri { get; set; }
		}

		public class FuckinExternalContext
		{
			public string url { get; set; }
			public string name { get; set; }
			public string source { get; set; }
		}
		

		private void ParseComments(string filepath)
		{
			Console.WriteLine("Analyzing comments...");
			string commentStringNoRoot = File.ReadAllText(filepath);
			string commentStringRoot = @"{ 'result': " + File.ReadAllText(filepath) + "}";
			string commentString = commentStringRoot;

			dynamic entry = JObject.Parse(commentString);
			Console.WriteLine("Count: {0}", entry.result.Count);
			var cnt = entry.result.Count;
			for (int i = 0; i < cnt; i++)
			{
				//Console.WriteLine($"{Utils.UnixTimestampToDateTime(Convert.ToInt32(entry.result[i].timestamp)).ToLocalTime()}");
				Console.WriteLine($"#{i}. {entry.result[i].title}");
				Console.WriteLine($"\"{entry.result[i].TokenType}");
				Console.WriteLine(Environment.NewLine);

				//try
				//{
				//	if (entry.result[i].data[0].comment.comment.Value.Contains("cunt"))
				//	{
				//		Console.WriteLine($"\"{entry.result[i].data[0].comment.comment.Value}{Environment.NewLine}\"");
				//	}
				//}
				//catch { }
			}
			//Console.WriteLine("first title: {0}", entry.result[0].title);
			//var cnt = entry;
			//foreach(var c in entry)
			//{
			//	Console.WriteLine("===========");
			//	Console.WriteLine(c.timestamp.ToString());
			//}
		}
	
		
		private static void ParseEntriesAsStream(string filepath)
		{
			Console.WriteLine("Analyzing comments..." + Environment.NewLine);

			string jsonString = File.ReadAllText(filepath).Trim('[', ']');

			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
			{
				IEnumerable<FBEntry> fbEntries = ReadJsonStream<FBEntry>(stream);

				foreach (var entry in fbEntries)
				{
					//FBEntries.Add(entry);
					Console.WriteLine(Utils.UnixTimestampToDateTime(entry.timestamp, DateTimeKind.Local));
					Console.WriteLine(entry.title);
					Console.WriteLine(Environment.NewLine);
				}

				Console.WriteLine($"Finished analyzing {fbEntries.Count()} posts and comments." + Environment.NewLine);
			}
		}

		public static IEnumerable<TResult> ReadJsonStream<TResult>(Stream stream)
		{
			JsonSerializer serializer = new JsonSerializer();

			using (StreamReader streamReader = new StreamReader(stream))
			using (JsonTextReader jsonReader = new JsonTextReader(streamReader))
			{
				jsonReader.SupportMultipleContent = true;

				while (jsonReader.Read())
				{
					yield return serializer.Deserialize<TResult>(jsonReader);
				}
			}
		}
		

//		using (JsonTextReader reader = new JsonTextReader(new StringReader((jsonString))))
//		{
//			while (reader.Read())
//			{
//				if (reader.Depth == 2)
//				{
//					if (reader.TokenType.Equals(JsonToken.StartObject))
//					{
//						Console.WriteLine(Environment.NewLine + "New object member");
//					}
//					else if (reader.TokenType.Equals(JsonToken.PropertyName))
//					{
//						if (reader.Value.Equals("attachments"))

//							Console.WriteLine($"{reader.Depth} {reader.Value} ({reader.Path})");

//					}
//				}

//				//{
//				//	Console.WriteLine($"Token: {reader.TokenType}, Value: {reader.Value}");
//				//}
//				//else
//				//{
//				//	Console.WriteLine($"Token: {reader.TokenType}");
//				//}
//			}
//		}

//		FuckinData fuckinData = JsonConvert.DeserializeObject<FuckinData>(commentString);
//		for (int i = 0; i<fuckinData.result.Length; i++)
//		{
//			Console.WriteLine(UnixTimestampToDateTime(fuckinData.result[i].timestamp).ToLocalTime());
//			Console.WriteLine(fuckinData.result[i].title);
//			if (!String.IsNullOrEmpty(fuckinData.result[i].data[0].comment.group))
//				Console.WriteLine(@"Group: " + fuckinData.result[i].data[0].comment.group);
//			Console.WriteLine(fuckinData.result[i].data[0].comment.author);
//			Console.WriteLine(fuckinData.result[i].data[0].comment.comment);
//		}



//		JObject commentObj = JObject.Parse(commentString);

//IList<JToken> lotsOfEntries = commentObj["result"].Children().ToList();

//IList<ThatFuckinEntry> fuckinComments = new List<ThatFuckinEntry>();

//		foreach(JToken entry in lotsOfEntries)
//		{
//			//Console.WriteLine(comment.ToString());
//			ThatFuckinEntry aFuckinComment = JsonConvert.DeserializeObject<ThatFuckinEntry>(entry.ToString());
//fuckinComments.Add(aFuckinComment);
//		}

//		foreach (ThatFuckinEntry fuckup in fuckinComments)
//		{
//			Console.WriteLine(UnixTimestampToDateTime(fuckup.timestamp).ToLocalTime());
//			Console.WriteLine(fuckup.title);
//			if (!String.IsNullOrEmpty(fuckup.data.comment.group))
//				Console.WriteLine(@"Group: " + fuckup.data.comment.group);
//			Console.WriteLine(fuckup.data.comment.author);
//			Console.WriteLine(fuckup.data.comment.comment);
//		}

//				private static List<string> InvalidElements; 

//				private static IList<JToken> DeserializeToList<JToken>(string filepath)
//				{
//					string jsonString = File.ReadAllText(filepath);

//					InvalidElements = null;
//					var array = JArray.Parse(jsonString);
//					IList<JToken> objectsList = new List<JToken>();

//					foreach(var item in array)
//					{
//						try
//						{
//							objectsList.Add(item.ToObject<JToken>());
//						}
//						catch(Exception ex)
//						{
//							InvalidElements = InvalidElements ?? new List<string>();
//							InvalidElements.Add(item.ToString());
//						}
//					}

//					return objectsList;
//				}

//				private void ParseComments(string filepath)
//				{
//					Console.WriteLine("Analyzing comments...");

//					ThatFuckinEntry thatFuckinEntry = new ThatFuckinEntry();
//					IList<ThatFuckinEntry> commentEntries;
//					IList<string> invalidEntries;

//					commentEntries = DeserializeToList<ThatFuckinEntry>(filepath);

//					if(InvalidElements.Count != 0)
//					{
//						invalidEntries = InvalidElements;
//					}

//					foreach (ThatFuckinEntry fuckup in commentEntries)
//					{
//						Console.WriteLine(UnixTimestampToDateTime(fuckup.timestamp).ToLocalTime());
//						Console.WriteLine(fuckup.title);
//						Console.WriteLine(fuckup.data);
//						Console.WriteLine(fuckup.data.comment.group);
//						Console.WriteLine(fuckup.data.comment.comment);
//						Console.WriteLine(fuckup.data.comment.author);
//					}
//				}

		//public void DisplayAllContent()
		//{
		//	foreach (FuckBookContent f in FuckedResult)
		//	{
		//		Console.Write($"{f.timestamp} = {UnixTimestampToDateTime(f.timestamp).ToLocalTime()} - ");
		//		Console.WriteLine($"{f.title}");
		//	}
		//}

		//public void DisplayFuckedContent()
		//{
		//	var cnt = 0;
		//	foreach (FuckBookContent f in FuckedResult)
		//	{
		//		foreach (string b in Wordlist)
		//		{
		//			if (f.title.Contains(b))
		//			{
		//				cnt++;
		//				Console.Write($"{f.timestamp} = {UnixTimestampToDateTime(f.timestamp).ToLocalTime()} - ");
		//				Console.WriteLine($"{f.data.comment.comment}");
		//				Console.WriteLine($"{f.title}");
		//			}
		//		}
		//	}
		//	Console.WriteLine("{0} fucked comments matched fucked phrases.\n", cnt);
		//}

	}
}
