using Gutendex;
using GutenSearch.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace GutenSearch.ViewModels
{
	public class SearchPageViewModel : INotifyPropertyChanged
	{
		private readonly GutendexClient client = new GutendexClient();
		private CancellationTokenSource? cts;
		private string search = "Monte Cristo";
		private List<Book> results = new List<Book>();

		public string Search
		{
			get => search;
			set
			{
				search = value;
				DebouncedUpdateResults();
			}
		}
		public List<Book> Results
		{
			get => results;
			set
			{
				results = value;
				OnPropertyChange();
			}
		}

		public SearchPageViewModel()
		{
			DebouncedUpdateResults = Debouncer.Debounce(UpdateResults, 500);
		}

		public Action DebouncedUpdateResults;
		public async Task UpdateResults()
		{
			if (cts != null)
				cts.Cancel();

			cts = new CancellationTokenSource();
			try
			{
				var result = await Task.Run(() => client.GetBooks(Search, cancellationToken: cts.Token));
				Results = result?.Results.ToList() ?? new List<Book>();
			}
			catch (TaskCanceledException e)
			{

			}
		}

		protected void OnPropertyChange([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		public event PropertyChangedEventHandler? PropertyChanged;
	}
}
