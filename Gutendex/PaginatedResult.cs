﻿namespace Gutendex
{
	public class PaginatedResult
	{
		public int Count { get; set; }
		public string Next { get; set; }
		public string Previous { get; set; }
		public Book[] Results { get; set; }
	}
}
