using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace YourNamespace
{
    public class CrystalReportToPDF
    {
        private readonly List<object> reportParameters;
        private readonly string connectionString;
        private readonly string reportPath;

        public CrystalReportToPDF(string reportPath, List<object> reportParameters, string connectionString)
        {
            this.connectionString = connectionString;
            this.reportPath = reportPath;
            this.reportParameters = reportParameters;
        }

        public void CreateReportPDF()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                reportDocument.Load(reportPath);

                reportDocument.Database.Tables[0].ApplyLogOnInfo(LogonInfo());

                for (int i = 0; i < reportParameters.Count; i++)
                {
                    string parameterName = reportDocument.ParameterFields[i].Name;
                    reportDocument.SetParameterValue(parameterName, reportParameters[i]);
                }

                Stream stream = reportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
                byte[] bytes;
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                string base64EncodedString = Convert.ToBase64String(bytes);
                Console.Write(base64EncodedString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private TableLogOnInfo LogonInfo()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
            TableLogOnInfo crLogonInfo = new TableLogOnInfo
            {
                ConnectionInfo =
                {
                    ServerName = builder.DataSource,
                    DatabaseName = builder.InitialCatalog,
                    UserID = builder.UserID,
                    Password = builder.Password
                }
            };
            return crLogonInfo;
        }
    }
}
