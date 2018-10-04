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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuckBookFitler
{
	public static class Utils
	{

		public static double DateTimeToUnixTimestamp(DateTime dateTime)
		{
			DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			long unixTimeStampInTicks = (dateTime.ToUniversalTime() - unixStart).Ticks;
			return (double)unixTimeStampInTicks / TimeSpan.TicksPerSecond;
		}

		public static DateTime UnixTimestampToDateTime(double unixTime)
		{
			DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
			return new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc);
		}

		public static DateTime UnixTimestampToDateTime(double unixTime, DateTimeKind timezone)
		{
			DateTime dateTime = UnixTimestampToDateTime(unixTime);

			switch (timezone)
			{
				case DateTimeKind.Unspecified:
				case DateTimeKind.Utc:
					return dateTime;
				case DateTimeKind.Local:
					return dateTime.ToLocalTime();
				default:
					return dateTime;
			}
		}

		public static string PrintHR(char paddingChar, int width)
		{
			return @"".PadRight(width, paddingChar);
		}

		public static string PrintHR(char paddingChar)
		{
			return PrintHR(paddingChar, 80);
		}

	}
}
