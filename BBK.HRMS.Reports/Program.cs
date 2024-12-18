using System;
using System.Collections.Generic;
using YourNamespace;

namespace BBK.HRMS.Reports
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<object> reportParameters;
            string reportName;
            reportName = "C:\\Users\\Equilap41\\Documents\\GitHub\\HRMS\\BBK-HRMS-MVC\\HRMS\\Reports\\M5\\rptM5_ListMnthDistribution.rpt";
            string connectionString = "server=.;Database=BBKDB;User Id=sa;Password=123;TrustServerCertificate=True";
            
            reportParameters = new List<object>();
            reportParameters.Add("BBK");
            reportParameters.Add("");
            reportParameters.Add("");
            reportParameters.Add("");
            reportParameters.Add("");
            reportParameters.Add("1763");
            reportParameters.Add("01/01/2021");
            reportParameters.Add("");
            reportParameters.Add("1763");
            reportParameters.Add("A");
            try
            {
                CrystalReportToPDF crystalReportToPDF = new CrystalReportToPDF(reportName, reportParameters, connectionString);
                crystalReportToPDF.CreateReportPDF();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
                Console.WriteLine("Press Enter to exit...");
                Console.ReadLine();
            }
        }
    }
}
