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
using System.Collections.Generic;

namespace FuckBookFitler.FuckinModels
{
	/// <summary>
	/// Entry type enums.
	/// </summary>	
	public enum FBEntryType
	{
		Other = 0,
		Post,
		Comment,
		Share,
		Photo,
		Video
	}

	/// <summary>
	/// Fitler data models
	/// </summary>
	public class FBEntryParsed
	{
		public DateTime timestamp { get; set; }
		public string title { get; set; }
		public string content { get; set; }
		public string author { get; set; }
		public string group { get; set; }
		public string post_author { get; set; }
		public string uri { get; set; }
		public FBEntryType entry_type { get; set; }
		public List<string> words_matched { get; set; }
	}

}
