namespace PAC_MAN
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //MessageBox.Show("Hru plánuji dát do soutìže, akorát kvùli množství vìcí ve høe jsem jí ještì nestihl plnì dodìlat");
            Application.Run(new MainForm());
        }
    }
}