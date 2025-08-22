namespace FSUIPC
{
    public interface IFSUIPCConnection
    {
        static abstract void Open(int processTimeoutMs = 150);

        static abstract void Process(string groupName);
        
        static abstract double ReadLVar(string LVAR);

        static abstract void WriteLVar(string LVAR, double NewValue);

        static abstract bool GroupsIsolatedToThread { get; set; }

        static abstract void Close(byte classInstance);

    }
}