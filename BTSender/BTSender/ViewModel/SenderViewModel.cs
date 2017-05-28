using BTSender.Model;
using BTSender.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace BTSender.ViewModel
{
	public class SenderViewModel : ViewModelBase
	{
		private readonly ISenderService _senderService;
		
		private string _data;

		public SenderViewModel()
		{
			_senderService = new SenderService();
			RegisterMessages();

			SendCommand = new RelayCommand(
				SendData,
				() => true);

			ValueChangedCommand = new RelayCommand(
				SendChangeAngel,
				() => true);
			//() => !string.IsNullOrEmpty(Data));
			//() => !string.IsNullOrEmpty(Data) && SelectDevice != null && SelectDevice.DeviceInfo != null));

			Devices = new ObservableCollection<BluetoothDevice>
			{
				new BluetoothDevice(null) { DeviceName = "Searching..." }
			};

			Messenger.Default.Send(new DiscoverDevicesMessage(true));
		}

		public ICommand SendCommand { get; private set; }
		public ICommand ValueChangedCommand { get; private set; }

		public ObservableCollection<BluetoothDevice> Devices { get; set; }

		public string Data
		{
			get { return _data; }
			set { Set(() => Data, ref _data, value); }
		}

		public BluetoothDevice SelectDevice { get; set; }

		public byte Angel { get; set; }

		#region Private
		private async void SendData()
		{
			string[] d = Data.Split(',');

			byte[] data = new byte[d.Length];

			for (int i = 0; i < d.Length; i++) 
			{
				data[i] = byte.Parse(d[i]);
			}

			SendDataInfo wasSent = await _senderService.Send(SelectDevice, SlipPacket.MakePacket(data));
		}

		private async void SendChangeAngel()
		{			
			byte[] data = new byte[] { 2, Angel};
			SendDataInfo wasSent = await _senderService.Send(SelectDevice, SlipPacket.MakePacket(data));
		}

		private async void ShowDevices(DiscoverDevicesMessage deviceMessage)
		{
			SelectDevice = null;

			IList<BluetoothDevice> items = await _senderService.GetDevices();

			Devices.Clear();

			foreach (BluetoothDevice device in items)
			{
				Devices.Add(device);
			}

			Data = string.Empty;
			// Now there are devices. Will found especial device
			Messenger.Default.Send(new ConnectToDeviceMessage(Constants.DeviceName));
		}

		private void ConnectToDevice(ConnectToDeviceMessage message)
		{
			BluetoothDevice device = Devices.FirstOrDefault(x => x.DeviceInfo.DeviceName.Equals(
				message.DeviceName,
				StringComparison.InvariantCultureIgnoreCase));

			if (device == null)
			{
				device = new BluetoothDevice(null) { DeviceName = "Not found" };
			}

			Devices.Clear();

			SelectDevice = device;

			Devices.Add(device);
		}

		private void RegisterMessages()
		{
			Messenger.Default.Register<ConnectToDeviceMessage>(this, ConnectToDevice);
			Messenger.Default.Register<DiscoverDevicesMessage>(this, ShowDevices);
		}

		#endregion
	}
}