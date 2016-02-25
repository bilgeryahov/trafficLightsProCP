using System;
using System.ComponentModel;

namespace TrafficComponentsLibrary
{
	public class Messenger : Component
	{
		private System.ComponentModel.Container components;

		private int sentCount;

		private int receiveCount;

		public int[] SendBuffer;

		public int[] ReceiveBuffer;

		public int ReceiverId;

		public int ReceiveCount
		{
			get
			{
				return this.receiveCount;
			}
		}

		public int SentCount
		{
			set
			{
				if (value > 0)
				{
					this.sentCount = value;
					this.SendBuffer = new int[this.sentCount];
				}
			}
		}

		public Messenger(IContainer container)
		{
			container.Add(this);
			this.InitializeComponent();
		}

		public Messenger()
		{
			this.InitializeComponent();
			this.sentCount = 0;
			this.receiveCount = 0;
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
			this.components = new System.ComponentModel.Container();
		}

		protected void OnMessenger(Messenger sender, int formNummer)
		{
			if (this.MessengerEvent != null)
			{
				this.MessengerEvent(sender, formNummer);
			}
		}

		public void Receive(Messenger sender)
		{
			if (this.ReceiveEvent != null)
			{
				this.receiveCount = (int)sender.SendBuffer.Length;
				if (this.receiveCount != 0)
				{
					this.ReceiveBuffer = new int[this.receiveCount];
					for (int i = 0; i < (int)this.ReceiveBuffer.Length; i++)
					{
						if (i < (int)sender.SendBuffer.Length)
						{
							this.ReceiveBuffer[i] = sender.SendBuffer[i];
						}
					}
					this.ReceiveEvent(this);
				}
			}
		}

		public void Transmit()
		{
			this.OnMessenger(this, this.ReceiverId);
		}

		public event Messenger.MessengerHandler MessengerEvent;

		public event Messenger.ReceiveHandler ReceiveEvent;

		public delegate void MessengerHandler(Messenger Sender, int FormNumber);

		public delegate void ReceiveHandler(Messenger Sender);
	}
}