namespace BTSender.Model
{
	class ConnectToDeviceMessage
	{
		private readonly string _deviceName;

		public ConnectToDeviceMessage(string deviceName)
		{
			_deviceName = deviceName;
		}

		public string DeviceName
		{
			get { return _deviceName; }
		}
	}
}
