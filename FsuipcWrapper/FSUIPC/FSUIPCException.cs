 
namespace FSUIPC;

public class FSUIPCException(FSUIPCError FSUIPCErrorCode, string Message) : Exception("FSUIPC Error #" + (int)FSUIPCErrorCode + ": " + FSUIPCErrorCode.ToString() + ". " + Message)
{
	private readonly FSUIPCError fsuipcErrorCode = FSUIPCErrorCode;

	public FSUIPCError FSUIPCErrorCode => fsuipcErrorCode;
}
