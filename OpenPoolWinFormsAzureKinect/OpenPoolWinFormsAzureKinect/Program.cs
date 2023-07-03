using System;

namespace OpenPoolWinFormsAzureKinect
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

            var mf = new MyForm();

            var Work = new KinectManager();
            var mTh = new Thread(new ParameterizedThreadStart(Work.KinectManagerThread!));
            mTh.IsBackground = true;
            mTh.Start(mf);

            mf.SetKinectManagerThread(mTh);

            Application.Run(mf);
        }
    }
}