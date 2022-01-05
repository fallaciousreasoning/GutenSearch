using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;

namespace Gutendex
{
	public class GutendexClient
	{
		private const string BaseUrl = "https://gutendex.com";
		private readonly HttpClient httpClient = new HttpClient();

		public async Task<PaginatedResult?> GetBooks(string? search=null, int[]? ids=null, int? authorYearStart=null, int? authorYearEnd=null, string[]? languages=null, bool? copyright=null, string? mimetype=null, string topic=null)
		{
			var queryBuilder = new QueryBuilder();
			if (search != null) queryBuilder.Add("search", search);
			if (ids != null) queryBuilder.Add("ids", ids.Select(id => id.ToString()));
			if (authorYearStart != null) queryBuilder.Add("author_year_start", authorYearStart.ToString());
			if (authorYearEnd != null) queryBuilder.Add("author_year_end", authorYearEnd.ToString());
			if (languages != null) queryBuilder.Add("languages", languages);
			if (copyright != null) queryBuilder.Add("copyright", copyright.ToString());
			if (mimetype != null) queryBuilder.Add("mime_type", mimetype);
			if (topic != null) queryBuilder.Add("topic", topic);

			var url = new Uri($"{BaseUrl}/books?{queryBuilder.ToString}");
			var text = await httpClient.GetStringAsync(url);

			return JsonConvert.DeserializeObject<PaginatedResult>(text);
		}

		public async Task<Book?> GetBook(int id)
		{
			var result = await GetBooks(ids: new[] { id });
			return result?.Results.FirstOrDefault();
		}
	}
}