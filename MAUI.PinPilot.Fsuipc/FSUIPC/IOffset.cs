namespace FSUIPC
{
    public interface IOffset
    {

        string Group { get; }

        public int Address { get; }

        int DataLength { get; }

        public byte[] DataValue { get; set; }

        bool OnceOnly { get; set; }
        
        bool Write { get; set; }

        bool WriteOnly { get; }
       
        //void CheckForChanges();
     
        //public bool LastReadEqualsData();

        public long FileAddress { get; set; }

        OffsetAction ActionAtNextProcess { get; set; }

        //internal void UpdatePrevious();

    }
}