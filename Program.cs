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
            //MessageBox.Show("Hru pl�nuji d�t do sout�e, akor�t kv�li mno�stv� v�c� ve h�e jsem j� je�t� nestihl pln� dod�lat");
            Application.Run(new MainForm());
        }
    }
}