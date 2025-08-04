namespace CleverenceTestTasks.ThreadSafeCounter
{
	public static class CounterServer
	{
		private static int count = 0;
		private static readonly ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

		public static int GetCount()
		{
			try
			{
				rwLock.EnterReadLock();
				return count;
			}
			finally
			{
				rwLock.ExitReadLock();
			}
		}

		public static void AddToCount(int value)
		{
			try
			{
				rwLock.EnterWriteLock();
				count += value;
			}
			finally
			{
				rwLock.ExitWriteLock();
			}
		}
	}
}
