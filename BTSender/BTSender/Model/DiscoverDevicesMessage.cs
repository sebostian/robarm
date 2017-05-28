namespace BTSender.Model
{
	public class DiscoverDevicesMessage
	{
		private readonly bool _isToShowDevices;

		public DiscoverDevicesMessage(bool isToShowDevices)
		{
			_isToShowDevices = isToShowDevices;
		}

		public bool IsToShowDevices
		{
			get { return _isToShowDevices; }
		}
	}
}
