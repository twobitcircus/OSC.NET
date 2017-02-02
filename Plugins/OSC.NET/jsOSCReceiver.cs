using System;
using System.Net;
using System.Net.Sockets;

public class jsOSCReceiver {}

namespace OSC.NET
{
	/// <summary>
	/// OSCReceiver
	/// </summary>
	
	public class OSCReceiver
	{
		private static OSCReceiver _instance;
		public static OSCReceiver instance {
			get {
				if(_instance == null)
					_instance = new OSCReceiver(5002);
				return _instance;
			}
		}
		
		protected UdpClient udpClient;
		protected int localPort;

		public OSCReceiver(int localPort)
		{
			this.localPort = localPort;
			Connect();
		}

		public void Connect()
		{
			if(this.udpClient != null) Close();
			this.udpClient = new UdpClient(this.localPort);
		}

		public void Close()
		{
			if (this.udpClient!=null) this.udpClient.Close();
			this.udpClient = null;
		}

		public OSCPacket Receive()
		{
            try
            {
                IPEndPoint ip = null;
                byte[] bytes = this.udpClient.Receive(ref ip);
                if (bytes != null && bytes.Length > 0)
                    return OSCPacket.Unpack(bytes);

            } catch (Exception e) { 
                Console.WriteLine(e.Message);
                return null;
            }

			return null;
		}
	}
}
