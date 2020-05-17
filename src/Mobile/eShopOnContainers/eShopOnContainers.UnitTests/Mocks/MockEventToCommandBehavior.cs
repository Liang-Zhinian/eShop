using eShop.Core.Behaviors;

namespace eShop.UnitTests
{
	public class MockEventToCommandBehavior : EventToCommandBehavior
	{
		public void RaiseEvent(params object[] args)
		{
			_handler.DynamicInvoke(args);
		}
	}
}
