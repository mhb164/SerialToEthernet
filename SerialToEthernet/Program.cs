using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;

[assembly: Guid("8ced053a-959f-424a-8cc4-3fad0669c130")]
[assembly: AssemblyTitle("SerialToEthernet"), AssemblyProduct("SerialToEthernet")]
[assembly: AssemblyDescription(""), AssemblyCopyright("Copyright ©  2022"), AssemblyConfiguration("")]
[assembly: AssemblyCompany(""), AssemblyTrademark(""), AssemblyCulture(""), ComVisible(false)]
[assembly: AssemblyVersion(SerialToEthernetController.Version), AssemblyFileVersion(SerialToEthernetController.Version)]

namespace SerialToEthernet
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator) == false)
            {
                MessageBox.Show($"{Application.ProductName} application must run as administrator!", "Error");
                return;
            }
            Application.Run(new MainForm());
        }
    }
}
