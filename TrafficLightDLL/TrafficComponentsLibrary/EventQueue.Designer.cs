using System;
using System.ComponentModel;
using System.Timers;

namespace TrafficComponentsLibrary
{
	public class EventQueue : Component
	{
		public const int QUEMAX = 100;

		private int queueId;

		private int queueLength;

		private EventQueue.EventQueueItem[] queue;

		private Timer timer;

		private System.ComponentModel.Container components;

		public EventQueue(IContainer container)
		{
			container.Add(this);
			this.InitializeComponent();
			if (this.queue == null)
			{
				this.queue = new EventQueue.EventQueueItem[100];
				for (int i = 0; i < 100; i++)
				{
					this.queue[i] = new EventQueue.EventQueueItem();
				}
				this.queueLength = 0;
			}
		}

		public EventQueue()
		{
			this.InitializeComponent();
			if (this.queue == null)
			{
				this.queue = new EventQueue.EventQueueItem[100];
				for (int i = 0; i < 100; i++)
				{
					this.queue[i] = new EventQueue.EventQueueItem();
				}
				this.queueLength = 0;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.timer = new Timer();
			((ISupportInitialize)this.timer).BeginInit();
			this.timer.Elapsed += new ElapsedEventHandler(this.QueueTimeOut);
			((ISupportInitialize)this.timer).EndInit();
		}

		public int PutQueue(int TimeOut, int Parameter)
		{
			if (this.queueLength == 100)
			{
				return 0;
			}
			EventQueue eventQueue = this;
			eventQueue.queueId = eventQueue.queueId + 1;
			this.queue[this.queueLength].Parameter = Parameter;
			this.queue[this.queueLength].Id = this.queueId;
			this.queue[this.queueLength].TimeOut = Environment.TickCount + TimeOut;
			EventQueue eventQueue1 = this;
			eventQueue1.queueLength = eventQueue1.queueLength + 1;
			this.ReviewQueue();
			return this.queueId;
		}

		private void QueueTimeOut(object sender, ElapsedEventArgs e)
		{
			int num = 0;
			int tickCount = Environment.TickCount;
			while (num < this.queueLength)
			{
				if (this.queue[num].TimeOut >= tickCount)
				{
					num++;
				}
				else
				{
					this.timer.Enabled = false;
					if (this.QueueEvent != null)
					{
						this.QueueEvent(this.queue[num].Parameter);
					}
					for (int i = num; i < this.queueLength - 1; i++)
					{
						this.queue[i] = this.queue[i + 1];
					}
					EventQueue eventQueue = this;
					eventQueue.queueLength = eventQueue.queueLength - 1;
				}
			}
			this.ReviewQueue();
		}

		public bool RemoveQueue(int queueId)
		{
			for (int i = 0; i < this.queueLength; i++)
			{
				if (this.queue[i].Id == queueId)
				{
					for (int j = i; j < this.queueLength - 1; j++)
					{
						this.queue[j] = this.queue[j + 1];
					}
					EventQueue eventQueue = this;
					eventQueue.queueLength = eventQueue.queueLength - 1;
					return true;
				}
			}
			return false;
		}

		private void ReviewQueue()
		{
			int timeOut = 0;
			for (int i = 0; i < this.queueLength; i++)
			{
				if (timeOut == 0 || this.queue[i].TimeOut < timeOut)
				{
					timeOut = this.queue[i].TimeOut;
				}
			}
			int tickCount = timeOut - Environment.TickCount;
			if (timeOut != 0)
			{
				if (tickCount <= 1)
				{
					this.timer.Interval = 1;
				}
				else
				{
					this.timer.Interval = (double)tickCount;
				}
			}
			this.timer.Enabled = timeOut != 0;
		}

		[Category("Queue")]
		[Description("Queue event ")]
		public event EventQueue.QueueEventHandler QueueEvent;

		private class EventQueueItem
		{
			public int TimeOut;

			public int Parameter;

			public int Id;

			public EventQueueItem()
			{
			}
		}

		public delegate void QueueEventHandler(int parameter);
	}
}