using BTSender.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BTSender.Services
{
	class SenderService : ISenderService
	{
		private readonly BluetoothSender _bluetoothSender;
		private readonly BluetoothDiscoverer _bluetoothDiscoverer;

		public SenderService()
		{
			_bluetoothSender = new BluetoothSender();
			_bluetoothDiscoverer = new BluetoothDiscoverer();
		}

		public async Task<IList<BluetoothDevice>> GetDevices()
		{
			return await Task.Run(() =>
			{
				return _bluetoothDiscoverer.DiscoverDevices();
			});
		}

		public async Task<SendDataInfo> Send(BluetoothDevice device, byte[] data)
		{
			return await Task.Run(() =>
			{
				return _bluetoothSender.SendData(device, data);
			});
		}
	}
}
