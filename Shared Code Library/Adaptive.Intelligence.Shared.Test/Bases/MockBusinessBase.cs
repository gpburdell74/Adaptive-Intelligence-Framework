namespace Adaptive.Intelligence.Shared.Tests.Bases
{
	public sealed class MockBusinessBase : BusinessBase
	{
		public bool SaveFailureFlagForTest { get; set; }

		public override bool PerformDelete()
		{
			bool success = false;

			if (!SaveFailureFlagForTest)
				success = true;
			else
				throw new Exception("Test Delete Exception");

			return success;
		}
		public override async Task<bool> PerformDeleteAsync()
		{
			await Task.Yield();
			bool success = false;

			if (!SaveFailureFlagForTest)
				success = true;
			else
				throw new Exception("Test Delete Exception");

			return success;

		}
		protected override bool PerformLoad<IdType>(IdType? id) where IdType : default
		{
			if (!SaveFailureFlagForTest)
				return true;
			else
				throw new Exception("Test Load Exception");

		}
		protected override async Task<bool> PerformLoadAsync<IdType>(IdType? id) where IdType : default
		{
			await Task.Yield();
			if (!SaveFailureFlagForTest)
				return true;
			else
				throw new Exception("Test Load Exception");
		}

		protected override bool PerformSave()
		{
			if (!SaveFailureFlagForTest)
				return true;
			else
				throw new Exception("Test Save Exception");

		}
		protected override async Task<bool> PerformSaveAsync()
		{
			await Task.Yield();
			if (!SaveFailureFlagForTest)
				return true;
			else
				throw new Exception("Test Save Exception");

		}
	}
}