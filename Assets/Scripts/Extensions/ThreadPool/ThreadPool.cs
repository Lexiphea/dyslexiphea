using System;
using System.Diagnostics;
using System.Threading;

internal delegate void ThreadPoolReportProgress(ThreadPoolEventProgressUpdate process, uint value);

public delegate void ThreadPoolEventDoWork(ThreadPoolDoWorkEventArgs pArgs);
public delegate void ThreadPoolEventProgressUpdate(ThreadPoolProgressUpdateEventArgs pArgs);
public delegate void ThreadPoolEventComplete(ThreadPoolCompletedEventArgs pArgs);

public static class ThreadPool
{
	internal static MessageLog Log = new MessageLog("[ThreadPool]");

	public const int DEFAULT_MAX_THREAD_POOL_COUNT = 4;
	public const int DEFAULT_UPDATE_TIMEOUT = 4;

	private static BlockingQueue<ThreadPoolEventArgs> events = new BlockingQueue<ThreadPoolEventArgs>();
	private static ConcurrentQueue<ThreadPoolReportProgressEventArgs> updatedProgress = new ConcurrentQueue<ThreadPoolReportProgressEventArgs>();
	private static ConcurrentQueue<ThreadPoolEventArgs> completedWork = new ConcurrentQueue<ThreadPoolEventArgs>();

	private static Stopwatch stopwatch = new Stopwatch();
	private static Thread[] threads = null;
	public static int MaxThreadCount = DEFAULT_MAX_THREAD_POOL_COUNT;
	public static int UpdateTimeout = DEFAULT_UPDATE_TIMEOUT;
	private static volatile bool shutdown = false;

	public static void Initialize()
	{
		Initialize(DEFAULT_MAX_THREAD_POOL_COUNT, DEFAULT_UPDATE_TIMEOUT);
	}
	public static void Initialize(int maxThreads, int updateTimeout)
	{
		MaxThreadCount = maxThreads;
		UpdateTimeout = updateTimeout / 2;
		threads = new Thread[MaxThreadCount];
		for (int i = 0; i < threads.Length; ++i)
		{
			Thread newThread = new Thread(DoWork);
			newThread.Name = "ThreadPool[" + newThread.ManagedThreadId + "]";
			//Log.Message(LogLevel.Info, "Started thread " + newThread.Name);
			//newThread.Priority = ThreadPriority.BelowNormal;
			newThread.IsBackground = true;
			newThread.Start();
			threads[i] = newThread;
		}
	}

	public static void Shutdown()
	{
		events.Shutdown();
		shutdown = true;
		for (int i = 0; i < threads.Length; ++i)
		{
			//Log.Message(LogLevel.Info, threads[i].Name + " exited gracefully..");
			threads[i].Join();
		}
	}

	public static void Enqueue(ThreadPoolEventDoWork onDoWork)
	{
		Enqueue(onDoWork, null, null, null, null);
	}

	public static void Enqueue(ThreadPoolEventDoWork onDoWork, ThreadPoolEventComplete onCompleteWork)
	{
		Enqueue(onDoWork, null, onCompleteWork, null, null);
	}

	public static void Enqueue(ThreadPoolEventDoWork onDoWork, ThreadPoolEventProgressUpdate onUpdateProgress, ThreadPoolEventComplete onCompleteWork)
	{
		Enqueue(onDoWork, onUpdateProgress, onCompleteWork, null, null);
	}

	public static void Enqueue(ThreadPoolEventDoWork onDoWork, ThreadPoolEventComplete onCompleteWork, object data)
	{
		Enqueue(onDoWork, null, onCompleteWork, null, data);
	}

	public static void Enqueue(ThreadPoolEventDoWork onDoWork, ThreadPoolEventProgressUpdate onUpdateProgress, ThreadPoolEventComplete onCompleteWork, object data)
	{
		Enqueue(onDoWork, onUpdateProgress, onCompleteWork, null, data);
	}

	public static void Enqueue(ThreadPoolEventDoWork onDoWork, ThreadPoolCancellationToken cancellationToken)
	{
		Enqueue(onDoWork, null, null, cancellationToken, null);
	}

	public static void Enqueue(ThreadPoolEventDoWork onDoWork, ThreadPoolEventComplete onCompleteWork, ThreadPoolCancellationToken cancellationToken)
	{
		Enqueue(onDoWork, null, onCompleteWork, cancellationToken, null);
	}

	public static void Enqueue(ThreadPoolEventDoWork onDoWork, ThreadPoolEventProgressUpdate onUpdateProgress, ThreadPoolEventComplete onCompleteWork, ThreadPoolCancellationToken cancellationToken)
	{
		Enqueue(onDoWork, onUpdateProgress, onCompleteWork, cancellationToken, null);
	}

	public static void Enqueue(ThreadPoolEventDoWork onDoWork, ThreadPoolEventComplete onCompleteWork, ThreadPoolCancellationToken cancellationToken, object data)
	{
		Enqueue(onDoWork, null, onCompleteWork, cancellationToken, data);
	}

	public static void Enqueue(ThreadPoolEventDoWork onDoWork, ThreadPoolEventProgressUpdate onUpdateProgress, ThreadPoolEventComplete onCompleteWork, ThreadPoolCancellationToken cancellationToken, object data)
	{
		if (onDoWork != null)
		{
			events.Enqueue(new ThreadPoolEventArgs(onDoWork, onUpdateProgress, onCompleteWork, cancellationToken, data));
		}
	}

	internal static void ReportProgress(ThreadPoolEventProgressUpdate process, uint value)
	{
		if (process != null)
		{
			updatedProgress.Enqueue(new ThreadPoolReportProgressEventArgs(process, value));
		}
	}

	//updated progress is processed in the primary thread
	public static void ProcessUpdatedProgress()
	{
		stopwatch.Reset();
		stopwatch.Start();
		while (stopwatch.ElapsedMilliseconds < UpdateTimeout)
		{
			ThreadPoolReportProgressEventArgs args;
			if (!updatedProgress.TryDequeue(out args))
			{
				stopwatch.Stop();
				return;
			}
			if (args.OnProgressUpdate != null)
			{
				args.OnProgressUpdate(new ThreadPoolProgressUpdateEventArgs(args.Value));
			}
		}
		stopwatch.Stop();
	}

	//completed work is processed on the primary thread
	public static void ProcessCompletedWork()
	{
		stopwatch.Reset();
		stopwatch.Start();
		while (stopwatch.ElapsedMilliseconds < UpdateTimeout)
		{
			ThreadPoolEventArgs args;
			if (!completedWork.TryDequeue(out args))
			{
				stopwatch.Stop();
				return;
			}
			if (args.OnCompleteWork != null)
			{
				args.OnCompleteWork(new ThreadPoolCompletedEventArgs(args.CancellationToken != null ? args.CancellationToken.IsCancellationPending : false, args.Result));
			}
		}
		stopwatch.Stop();
	}

	internal static void DoWork()
	{
		while (!shutdown)
		{
			try
			{
				ThreadPoolEventArgs eventArgs;
				if (events.TryDequeue(out eventArgs))
				{
					if (eventArgs.CancellationToken != null && eventArgs.CancellationToken.IsCancellationPending)
					{
						if (eventArgs.OnCompleteWork != null)
						{
							completedWork.Enqueue(eventArgs);
						}
					}
					else
					{
						ThreadPoolDoWorkEventArgs doWorkArgs = new ThreadPoolDoWorkEventArgs(eventArgs.OnUpdateProgress, ReportProgress, eventArgs.CancellationToken, eventArgs.Argument);
						eventArgs.OnDoWork(doWorkArgs);

						if (eventArgs.OnCompleteWork != null)
						{
							eventArgs.Result = doWorkArgs.Result;
							completedWork.Enqueue(eventArgs);
						}
					}
				}
				Thread.Sleep(1);
			}
			catch (Exception e)
			{
				Log.Message(LogLevel.Error, e.ToString());
			}
		}
	}
}