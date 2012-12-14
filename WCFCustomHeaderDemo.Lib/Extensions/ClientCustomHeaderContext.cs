namespace WCFCustomHeaderDemo.Lib.Extensions
{
    public static class ClientCustomHeaderContext
    {
        public static CustomHeader HeaderInformation;

        static ClientCustomHeaderContext()
        {
            HeaderInformation = new CustomHeader();
        }
    }
}