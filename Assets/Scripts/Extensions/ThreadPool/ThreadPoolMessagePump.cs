using UnityEngine;

public class ThreadPoolMessagePump : MonoBehaviour
{
	//protected static MessageLog Log = new MessageLog("[ThreadPoolMessagePump]");

	public int NumThreads = ThreadPool.DEFAULT_MAX_THREAD_POOL_COUNT;
	[Tooltip("The amount of time the threadpool has to process updates and completed work in milliseconds.")]
	public int Timeout = ThreadPool.DEFAULT_UPDATE_TIMEOUT;

	void Awake()
	{
		//Log.Message(LogLevel.Info, "ThreadPoolMessagePump.Awake()");
		ThreadPool.Initialize(NumThreads, Timeout);
	}

	void Update()
	{
		ThreadPool.ProcessUpdatedProgress();
		ThreadPool.ProcessCompletedWork();
	}

	void OnDestroy()
	{
		//Log.Message(LogLevel.Info, "ThreadPoolMessagePump.OnDestroy()");
		ThreadPool.Shutdown();
	}
}