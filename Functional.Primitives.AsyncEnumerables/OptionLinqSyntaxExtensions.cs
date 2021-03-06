﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class OptionLinqSyntaxExtensions
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IEnumerable<TSuccess> source, Func<TSuccess, Option<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.Select(value => bind
					.Invoke(value)
					.Select(success => resultSelector.Invoke(value, success))
				)
				.AsOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IEnumerable<TSuccess> source, Func<TSuccess, Task<Option<TBind>>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.AsAsyncEnumerable()
				.SelectAsync(value => bind
					.Invoke(value)
					.Select(success => resultSelector.Invoke(value, success))
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this Task<IEnumerable<TSuccess>> source, Func<TSuccess, Option<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.AsAsyncEnumerable()
				.Select(value => bind
					.Invoke(value)
					.Select(success => resultSelector.Invoke(value, success))
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this Task<IEnumerable<TSuccess>> source, Func<TSuccess, Task<Option<TBind>>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.AsAsyncEnumerable()
				.SelectAsync(value => bind
					.Invoke(value)
					.Select(success => resultSelector.Invoke(value, success))
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IAsyncEnumerable<TSuccess> source, Func<TSuccess, Option<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.Select(value => bind
					.Invoke(value)
					.Select(success => resultSelector.Invoke(value, success))
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IAsyncEnumerable<TSuccess> source, Func<TSuccess, Task<Option<TBind>>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.SelectAsync(value => bind
					.Invoke(value)
					.Select(success => resultSelector.Invoke(value, success))
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IOptionEnumerable<TSuccess> source, Func<TSuccess, IEnumerable<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.SelectMany(result => result
					.Match(success =>
						bind
						.Invoke(success)
						.Select(obj => resultSelector.Invoke(success, obj))
						.Select(Option.Some),
						() => Enumerable.Repeat(Option.None<TResult>(), 1)
					)
				)
				.AsOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IOptionEnumerable<TSuccess> source, Func<TSuccess, Task<IEnumerable<TBind>>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.AsAsyncEnumerable()
				.SelectMany(result => result
					.Match(success =>
						bind
						.Invoke(success)
						.AsAsyncEnumerable()
						.Select(obj => resultSelector.Invoke(success, obj))
						.Select(Option.Some),
						() => AsyncEnumerable.Repeat(Option.None<TResult>(), 1)
					)
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IOptionEnumerable<TSuccess> source, Func<TSuccess, IAsyncEnumerable<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.SelectMany(result => result
					.Match(success =>
						bind
						.Invoke(success)
						.Select(obj => resultSelector.Invoke(success, obj))
						.Select(Option.Some),
						() => AsyncEnumerable.Repeat(Option.None<TResult>(), 1)
					)
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IOptionEnumerable<TSuccess> source, Func<TSuccess, Option<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.Select(result => result
					.Match(success =>
						bind
						.Invoke(success)
						.Select(value => resultSelector.Invoke(success, value)),
						Option.None<TResult>
					)
				)
				.AsOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IOptionEnumerable<TSuccess> source, Func<TSuccess, Task<Option<TBind>>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.AsAsyncEnumerable()
				.SelectAsync(result => result
					.MatchAsync(success =>
						bind
						.Invoke(success)
						.Select(value => resultSelector.Invoke(success, value)),
						() => Task.FromResult(Option.None<TResult>())
					)
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IAsyncOptionEnumerable<TSuccess> source, Func<TSuccess, IEnumerable<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.SelectMany(result => result
					.Match(success =>
						bind
						.Invoke(success)
						.Select(obj => resultSelector.Invoke(success, obj))
						.Select(Option.Some),
						() => Enumerable.Repeat(Option.None<TResult>(), 1)
					)
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IAsyncOptionEnumerable<TSuccess> source, Func<TSuccess, Task<IEnumerable<TBind>>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.SelectMany(result => result
					.Match(success =>
						bind
						.Invoke(success)
						.AsAsyncEnumerable()
						.Select(obj => resultSelector.Invoke(success, obj))
						.Select(Option.Some),
						() => AsyncEnumerable.Repeat(Option.None<TResult>(), 1)
					)
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IAsyncOptionEnumerable<TSuccess> source, Func<TSuccess, IAsyncEnumerable<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.SelectMany(result => result
					.Match(success =>
						bind
						.Invoke(success)
						.Select(obj => resultSelector.Invoke(success, obj))
						.Select(Option.Some),
						() => AsyncEnumerable.Repeat(Option.None<TResult>(), 1)
					)
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IAsyncOptionEnumerable<TSuccess> source, Func<TSuccess, Option<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.Select(result => result
					.Match(success =>
						bind
						.Invoke(success)
						.Select(value => resultSelector.Invoke(success, value)),
						Option.None<TResult>
					)
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this IAsyncOptionEnumerable<TSuccess> source, Func<TSuccess, Task<Option<TBind>>> bind, Func<TSuccess, TBind, TResult> resultSelector)
		{
			if (bind == null)
				throw new ArgumentNullException(nameof(bind));

			if (resultSelector == null)
				throw new ArgumentNullException(nameof(resultSelector));

			return source
				.SelectAsync(result => result
					.MatchAsync(success =>
						bind
						.Invoke(success)
						.Select(value => resultSelector.Invoke(success, value)),
						() => Task.FromResult(Option.None<TResult>())
					)
				)
				.AsAsyncOptionEnumerable();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this Option<TSuccess> source, Func<TSuccess, IEnumerable<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
			=> Enumerable.Repeat(source, 1).AsOptionEnumerable().SelectMany(bind, resultSelector);

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this Option<TSuccess> source, Func<TSuccess, Task<IEnumerable<TBind>>> bind, Func<TSuccess, TBind, TResult> resultSelector)
			=> Enumerable.Repeat(source, 1).AsOptionEnumerable().SelectMany(bind, resultSelector);

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this Option<TSuccess> source, Func<TSuccess, IAsyncEnumerable<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
			=> Enumerable.Repeat(source, 1).AsOptionEnumerable().SelectMany(bind, resultSelector);

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this Task<Option<TSuccess>> source, Func<TSuccess, IEnumerable<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
			=> AsyncEnumerable.Repeat(source, 1).AsAsyncOptionEnumerable().SelectMany(bind, resultSelector);

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this Task<Option<TSuccess>> source, Func<TSuccess, Task<IEnumerable<TBind>>> bind, Func<TSuccess, TBind, TResult> resultSelector)
			=> AsyncEnumerable.Repeat(source, 1).AsAsyncOptionEnumerable().SelectMany(bind, resultSelector);

		[EditorBrowsable(EditorBrowsableState.Never)]
		public static IAsyncOptionEnumerable<TResult> SelectMany<TSuccess, TBind, TResult>(this Task<Option<TSuccess>> source, Func<TSuccess, IAsyncEnumerable<TBind>> bind, Func<TSuccess, TBind, TResult> resultSelector)
			=> AsyncEnumerable.Repeat(source, 1).AsAsyncOptionEnumerable().SelectMany(bind, resultSelector);
	}
}