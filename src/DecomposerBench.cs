using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace DecompositionPlayground
{
	[SimpleJob(RuntimeMoniker.Net60), RankColumn]
	public class DecomposerBench
	{
		[Params(4, 6, 8)]
		public int K { get; set; }

		[Params(0.05, 0.5, 50.0)]
		public Double TargetDouble { get; set; }

		[Params(0.0, 0.000001, 0.00005)]
		public Double ToleranceDouble { get; set; }

		[Params(0.000001, 0.00005)]
		public Double DustDouble { get; set; }

		public long[]? Denominations { get; set; }

		[GlobalSetup]
		public void Setup()
		{
			var denoms = new long[] {
				1, 2, 3, 4, 5, 6, 8, 9, 10, 16, 18, 20, 27, 32, 50, 54, 64, 81, 100, 128, 162, 200,
				243, 256, 486, 500, 512, 729, 1000, 1024, 1458, 2000, 2048, 2187, 4096, 4374, 5000,
				6561, 8192, 10000, 13122, 16384, 19683, 20000, 32768, 39366, 50000, 59049, 65536,
				100000, 118098, 131072, 177147, 200000, 262144, 354294, 500000, 524288, 531441,
				1000000, 1048576, 1062882, 1594323, 2000000, 2097152, 3188646, 4194304, 4782969,
				5000000, 8388608, 9565938, 10000000, 14348907, 16777216, 20000000, 28697814,
				33554432, 43046721, 50000000, 67108864, 86093442, 100000000, 129140163, 134217728,
				200000000, 258280326, 268435456, 387420489, 500000000, 536870912, 774840978,
				1000000000, 1073741824, 1162261467, 2000000000, 2147483648, 2324522934, 3486784401,
				4294967296, 5000000000, 6973568802, 8589934592, 10000000000, 10460353203, 17179869184,
				20000000000, 20920706406, 31381059609, 34359738368, 50000000000, 62762119218,
				68719476736, 94143178827, 100000000000, 137438953472, 188286357654, 200000000000,
				274877906944, 282429536481, 500000000000, 549755813888, 564859072962, 847288609443,
				1000000000000, 1099511627776, 1694577218886, 2000000000000, 2199023255552, 2541865828329
			};

			var target = (long)(TargetDouble * 100_000_000);
			var dust = (long)(DustDouble * 100_000_000);

			Denominations = denoms
				.SkipWhile(x => x < dust)
				.TakeWhile(x => x <= target)
				.Reverse()
				.ToArray();
		}

		[Benchmark]
		public void Process()
		{
			var target = (long)(TargetDouble * 100_000_000);
			var tolerance = (long)(ToleranceDouble * 100_000_000);
			var count = Decomposer.Combinations(target, tolerance, K, Denominations!).Count();
		}
	}
}