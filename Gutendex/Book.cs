namespace Gutendex
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string[] Subjects { get; set; }
		public Person[] Authors { get; set; }
		public Person[] Translators { get; set; }
		public string[] Bookshelves { get; set; }
		public string[] Languages { get; set; }
		public bool Copyright { get; set; }
		public string MediaType { get; set; }
		public Dictionary<string, string> Formats { get; set; }
		public int DownloadCount { get; set; }
	}
}
