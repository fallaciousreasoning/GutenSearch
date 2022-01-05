using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GutenSearch.Utils
{
	public class Debouncer
	{
		public static Action Debounce(Func<Task> action, int debounce)
		{
			CancellationTokenSource? cts = null;
			return () =>
			{
				if (cts != null) cts.Cancel();
				cts = new CancellationTokenSource();

				try
				{
					Task.Delay(debounce, cts.Token).ContinueWith(t => action());
				}
				catch (TaskCanceledException) { }
			};
		}
		public static Action Debounce(Action action, int debounce) => Debounce(async () => action(), debounce);
	}
}
