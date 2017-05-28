using System.Collections.Generic;

namespace BTSender.Model
{
	static class SlipPacket
	{
		private const byte SLIP_END = 192;
		private const byte SLIP_ESC = 219;
		private const byte SLIP_ESC_END = 220;
		private const byte SLIP_ESC_ESC = 221;

		public static byte[] MakePacket(byte[] data)
		{
			List<byte> result = new List<byte>();

			result.Add(SLIP_END);

			foreach (byte item in data)
			{
				switch (item)
				{
					case SLIP_END:
						result.Add(SLIP_ESC);
						result.Add(SLIP_ESC_END);
						break;
					case SLIP_ESC:
						result.Add(SLIP_ESC);
						result.Add(SLIP_ESC_ESC);
						break;
					default:
						result.Add(item);
						break;
				}
			}

			result.Add(SLIP_END);
			return result.ToArray();
		}
	}
}
