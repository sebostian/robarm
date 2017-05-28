using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.IO;
using System.Net.Sockets;

namespace BTSender.Model
{
	class BluetoothSender
	{
		private readonly BluetoothClient _bluetoothClient;
		private Stream _bluetoothStream;

		public BluetoothSender()
		{
			_bluetoothClient = new BluetoothClient();
		}

		public SendDataInfo SendData(BluetoothDevice device, byte[] data)
		{
			if (device == null)
			{
				throw new NullReferenceException("device");
			}

			SendDataInfo result = new SendDataInfo()
			{
				Info = string.Empty,
				Result = SendDataInfo.SendResult.Ok,
			};

			try
			{
				if (!_bluetoothClient.Connected)
				{
					_bluetoothClient.SetPin(Constants.Pin);
					_bluetoothClient.Connect(device.DeviceAddress, BluetoothService.SerialPort);
				}

				if (_bluetoothStream == null)
				{
					_bluetoothStream = _bluetoothClient.GetStream();
				}

				_bluetoothStream.Write(data, 0, data.Length);

			}
			catch (Exception ex)
			{
				result.Info = string.Format("Error {0} {1}", ex.Message, ex.StackTrace);
				result.Result = SendDataInfo.SendResult.Error;
			}
			return result;
		}
	}
}
