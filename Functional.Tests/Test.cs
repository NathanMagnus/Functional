﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Functional.Tests
{
    public class Test
    {
		[Fact]
		public async Task SyncSelectors()
		{
			var x = 
				from item1 in new[] { 1, 2, 3 }
				from item2 in new[] { 4, 5, 6 }
				select (item1, item2);
			
			var y = await
				from item1 in Task.FromResult(new[] { 1, 2, 3 }).AsEnumerable()
				from item2 in new[] { 4, 5, 6 }
				from item3 in Task.FromResult(new[] { 7, 8, 9 }).AsEnumerable()
				from item4 in new[] { 10, 11, 11 }
				select (item1, item2, item3, item4);

			var z = await
				from item1 in new[] { 1, 2, 3 }
				from item2 in Task.FromResult(new[] { 4, 5, 6 }).AsEnumerable()
				from item3 in new[] { 7, 8, 9 }
				from item4 in Task.FromResult(new[] { 10, 11, 11 }).AsEnumerable()
				select (item1, item2, item3, item4);

			var a =
				from one in Result.Success<int, string>(1)
				from item in new[] { 1, 2, 3 }
				select (one, item);

			var b =
				from item in new[] { 1, 2, 3 }
				from one in Result.Success<int, string>(1)
				select (item, one);

			var c =
				from item1 in new[] { 1, 2, 3 }
				from one in Result.Success<int, string>(1)
				from item2 in new[] { 4, 5, 6 }
				from two in Result.Success<int, string>(1)
				select (item1, one, item2, two);

			var d1 =
				(
					from item1 in new[] { 1, 2, 3 }
					from one in Result.Create(item1 % 2 == 1, item1, "Failed!")
					from item2 in new[] { 4, 5, 6 }
					from two in Result.Success<int, string>(1)
					from item3 in new[] { 8 }
					select (item1, one, item2, two, item3)
				)
				.TakeUntilFailure();

			var d2 =
				(
					from item1 in new[] { 1, 2, 3 }
					from one in Result.Create(item1 % 2 == 1, item1, "Failed!")
					from two in Result.Success<int, string>(1)
					from item2 in new[] { 4, 5, 6 }
					from item3 in new[] { 8 }
					select (item1, one, item2, two, item3)
				)
				.TakeUntilFailure();

			var e = await
				(
					from one in Task.FromResult(Result.Success<int, string>(1))
					from item in Task.FromResult(new[] { 1, 2, 3 }).AsEnumerable()
					select (one, item)
				)
				.AsEnumerable();

			var f = await
				(
					from item in Task.FromResult(new[] { 1, 2, 3 }).AsEnumerable()
					from one in Task.FromResult(Result.Success<int, string>(1))
					select (item, one)
				)
				.AsEnumerable();

			var g = await
				(
					from item1 in Task.FromResult(new[] { 1, 2, 3 }).AsEnumerable()
					from one in Task.FromResult(Result.Success<int, string>(1))
					from item2 in new[] { 4, 5, 6 }
					from two in Result.Success<int, string>(2)
					from three in Result.Success<int, string>(2)
					select (item1, one, item2, two, three)
				)
				.AsEnumerable();

			var h = await
				(
					from item1 in new[] { 1, 2, 3 }
					from one in Result.Success<int, string>(1)
					from item2 in Task.FromResult(new[] { 4, 5, 6 }).AsEnumerable()
					from two in Task.FromResult(Result.Success<int, string>(1))
					select (item1, one, item2, two)
				)
				.AsEnumerable();

			var i = await
				(
					from item1 in Task.FromResult(new[] { 1, 2, 3 }).AsEnumerable()
					from item2 in Task.FromResult(new[] { 7, 8, 9 }).AsEnumerable()
					from one in Task.FromResult(Result.Create(item1 % 2 == 1, item1, "Failed!"))
					from item3 in new[] { 4, 5, 6 }
					from two in Result.Success<int, string>(1)
					select (item1, item2, one, item3, two)
				)
				.TakeUntilFailure();

			var j = await
				(
					from item1 in new[] { 1, 2, 3 }
					from one in Result.Create(item1 % 2 == 1, item1, "Failed!")
					from item2 in Task.FromResult(new[] { 4, 5, 6 }).AsEnumerable()
					from two in Task.FromResult(Result.Success<int, string>(1))
					select (item1, one, item2, two)
				)
				.TakeUntilFailure();
		}

		[Fact]
		public async Task AsyncSelectors()
		{
			var x = await
				from item1 in new[] { 1, 2, 3 }
				from item2 in new[] { 4, 5, 6 }
				select Task.FromResult((item1, item2));

			var y = await
				from item1 in Task.FromResult(new[] { 1, 2, 3 }).AsEnumerable()
				from item2 in new[] { 4, 5, 6 }
				from item3 in Task.FromResult(new[] { 7, 8, 9 }).AsEnumerable()
				from item4 in new[] { 10, 11, 11 }
				select Task.FromResult((item1, item2, item3, item4));

			var z = await
				from item1 in new[] { 1, 2, 3 }
				from item2 in Task.FromResult(new[] { 4, 5, 6 }).AsEnumerable()
				from item3 in new[] { 7, 8, 9 }
				from item4 in Task.FromResult(new[] { 10, 11, 11 }).AsEnumerable()
				select Task.FromResult((item1, item2, item3, item4));

			var a = await
				(
					from one in Result.Success<int, string>(1)
					from item in new[] { 1, 2, 3 }
					select Task.FromResult((one, item))
				)
				.AsEnumerable();

			var b = await
				(
					from item in new[] { 1, 2, 3 }
					from one in Result.Success<int, string>(1)
					select Task.FromResult((item, one))
				)
				.AsEnumerable();

			var c = await
				(
					from item1 in new[] { 1, 2, 3 }
					from one in Result.Success<int, string>(1)
					from item2 in new[] { 4, 5, 6 }
					from two in Result.Success<int, string>(1)
					select Task.FromResult((item1, one, item2, two))
				)
				.AsEnumerable();

			var d1 = await
				(
					from item1 in new[] { 1, 2, 3 }
					from one in Result.Create(item1 % 2 == 1, item1, "Failed!")
					from item2 in new[] { 4, 5, 6 }
					from two in Result.Success<int, string>(1)
					from item3 in new[] { 8 }
					select Task.FromResult((item1, one, item2, two, item3))
				)
				.TakeUntilFailure();

			var d2 = await
				(
					from item1 in new[] { 1, 2, 3 }
					from one in Result.Create(item1 % 2 == 1, item1, "Failed!")
					from two in Result.Success<int, string>(1)
					from item2 in new[] { 4, 5, 6 }
					from item3 in new[] { 8 }
					select Task.FromResult((item1, one, item2, two, item3))
				)
				.TakeUntilFailure();

			var e = await
				(
					from one in Task.FromResult(Result.Success<int, string>(1))
					from item in Task.FromResult(new[] { 1, 2, 3 }).AsEnumerable()
					select Task.FromResult((one, item))
				)
				.AsEnumerable();

			var f = await
				(
					from item in Task.FromResult(new[] { 1, 2, 3 }).AsEnumerable()
					from one in Task.FromResult(Result.Success<int, string>(1))
					select Task.FromResult((item, one))
				)
				.AsEnumerable();

			var g = await
				(
					from item1 in Task.FromResult(new[] { 1, 2, 3 }).AsEnumerable()
					from one in Task.FromResult(Result.Success<int, string>(1))
					from item2 in new[] { 4, 5, 6 }
					from two in Result.Success<int, string>(2)
					from three in Result.Success<int, string>(2)
					select Task.FromResult((item1, one, item2, two, three))
				)
				.AsEnumerable();

			var h = await
				(
					from item1 in new[] { 1, 2, 3 }
					from one in Result.Success<int, string>(1)
					from item2 in Task.FromResult(new[] { 4, 5, 6 }).AsEnumerable()
					from two in Task.FromResult(Result.Success<int, string>(1))
					select Task.FromResult((item1, one, item2, two))
				)
				.AsEnumerable();

			var i = await
				(
					from item1 in Task.FromResult(new[] { 1, 2, 3 }).AsEnumerable()
					from item2 in Task.FromResult(new[] { 7, 8, 9 }).AsEnumerable()
					from one in Task.FromResult(Result.Create(item1 % 2 == 1, item1, "Failed!"))
					from item3 in new[] { 4, 5, 6 }
					from two in Result.Success<int, string>(1)
					select Task.FromResult((item1, item2, one, item3, two))
				)
				.TakeUntilFailure();

			var j = await
				(
					from item1 in new[] { 1, 2, 3 }
					from one in Result.Create(item1 % 2 == 1, item1, "Failed!")
					from item2 in Task.FromResult(new[] { 4, 5, 6 }).AsEnumerable()
					from two in Task.FromResult(Result.Success<int, string>(1))
					select Task.FromResult((item1, one, item2, two))
				)
				.TakeUntilFailure();
		}
	}
}
