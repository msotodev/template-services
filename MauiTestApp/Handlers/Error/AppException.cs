using static MauiTestApp.Handlers.Error.ErrorTypes;

namespace MauiTestApp.Handlers.Error
{
	public class AppException : Exception
	{
		public ErrorType ErrorType { get; }

		public string UserFriendlyKey { get; }

		/**/

		public AppException(
			ErrorType errorType, string userFriendlyKey, string technicalMessage
		) : base(technicalMessage)
		{
			ErrorType = errorType;
			UserFriendlyKey = userFriendlyKey;
		}

		public AppException(
			ErrorType errorType, string userFriendlyKey, string technicalMessage, Exception innerException
		) : base(technicalMessage, innerException)
		{
			ErrorType = errorType;
			UserFriendlyKey = userFriendlyKey;
		}
	}
}