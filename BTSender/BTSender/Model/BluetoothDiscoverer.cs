using InTheHand.Net.Sockets;
using System.Collections.Generic;

namespace BTSender.Model
{
	class BluetoothDiscoverer
	{
		public IList<BluetoothDevice> DiscoverDevices()
		{
			var devices = new List<BluetoothDevice>();
			using (var bluetoothClient = new BluetoothClient())
			{
				BluetoothDeviceInfo[] discoveredDevices = bluetoothClient.DiscoverDevices();
				int count = discoveredDevices.Length;
				for (int i = 0; i < count; i++)
				{
					devices.Add(new BluetoothDevice(discoveredDevices[i]));
				}
			}

			return devices;
		}
	}
}
