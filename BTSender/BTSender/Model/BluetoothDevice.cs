using InTheHand.Net;
using InTheHand.Net.Sockets;

namespace BTSender.Model
{
	public class BluetoothDevice
	{
		public BluetoothDevice(BluetoothDeviceInfo bluetoothDeviceInfo)
		{
			DeviceInfo = bluetoothDeviceInfo;
			if (bluetoothDeviceInfo != null)
			{
				DeviceAddress = bluetoothDeviceInfo.DeviceAddress;
				DeviceName = bluetoothDeviceInfo.DeviceName;
			}
		}

		public string DeviceName { get; set; }

		public BluetoothDeviceInfo DeviceInfo { get; }

		public BluetoothAddress DeviceAddress { get; internal set; }

		public override string ToString()
		{
			return DeviceInfo != null ? DeviceInfo.DeviceName : DeviceName;
		}
	}
}
