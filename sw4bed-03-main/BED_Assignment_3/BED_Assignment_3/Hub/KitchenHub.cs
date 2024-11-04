using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp;

namespace BED_Assignment_3.Hub
{
	public class KitchenHub : Hub<IKitchenHub>
	{
		public async Task KitchenUpdate()
		{
			await Clients.All.KitchenUpdate();
		}
	}
}