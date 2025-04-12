namespace Blitz.Infrastructure.Helpers
{
    public static class InsertBatchHelper
    {
        public static List<List<T>> SplitIntoBatches<T>(List<T> source, int batchSize)
        {
            var batches = new List<List<T>>();
            for (int i = 0; i < source.Count; i += batchSize)
            {
                batches.Add(source.GetRange(i, Math.Min(batchSize, source.Count - i)));
            }

            return batches;
        }
    }
}