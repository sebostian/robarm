namespace BTSender.Model
{
	class SendDataInfo
	{
		public enum SendResult
		{
			Ok,
			Error,
		}

		public SendResult Result { get; set; }
		public string Info { get; set; }
	}
}
