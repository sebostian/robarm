using BTSender.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTSender.Services
{
	interface ISenderService
	{
		Task<IList<BluetoothDevice>> GetDevices();
		Task<SendDataInfo> Send(BluetoothDevice toDevice, byte[] data);
	}
}
