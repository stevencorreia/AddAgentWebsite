using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace AddCampaignB2b.CommonClasses
{
    public class CommonCode
    {
        #region "Error Stuff"

        public static void HandleException(Exception ex)
        {
            string assemblyVersion = Assembly.GetAssembly(typeof(CommonCode)).GetName().Version.ToString();
            string assemblyName = Assembly.GetAssembly(typeof(CommonCode)).GetName().Name;

            SmtpClient mailServer = new SmtpClient(ConfigurationManager.AppSettings["ErrorSMTP"]);
            MailMessage mailMessage = new MailMessage();
            MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["ErrorFromAddress"]);

            mailMessage.To.Add(ConfigurationManager.AppSettings["ErrorToAddress"]);
            mailMessage.From = fromAddress;
            mailMessage.Subject = string.Format("{0}, Version: {1} Error Has Occurred!", assemblyName, assemblyVersion);

            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;
            mailMessage.Body = string.Format(@"
<html>
<body>
  <h3>An Error Has Occurred!</h3>
  <table cellpadding=""5"" cellspacing=""0"" border=""1"">
  <tr>
  <td style=""text-align: right;font-weight: bold"">Exception Type:</td>
  <td>{0}</td>
  </tr>
  <tr>
  <td style=""text-align: right;font-weight: bold"">Message:</td>
  <td>{1}</td>
  </tr>
  <tr>
  <td style=""text-align: right;font-weight: bold"">Inner Exception:</td>
  <td>{2}</td>
  </tr>
  <tr>
  <td style=""text-align: right;font-weight: bold"">Stack Trace:</td>
  <td>{3}</td>
  </tr> 
  </table>
</body>
</html>",
                    ex.GetType(),
                    ex.Message,
                    ex.InnerException != null ? ex.InnerException.Message : "",
                    ex.StackTrace.Replace(Environment.NewLine, "<br />"));

            mailServer.Send(mailMessage);
        }

        private static void Serialize<T>(ref T obj, string fileName, ref Exception ex)
        {
            string fullName = string.Format("{0}{1}.xml", ConfigurationManager.AppSettings["SerializeLocation"], fileName);

            #region Error Xml Format

            XmlDocument doc = new XmlDocument();
            XmlNode ErrorDetailsNode = doc.CreateElement("ErrorDetails");

            XmlNode ExceptionTypeNode = doc.CreateElement("ExceptionType");
            ExceptionTypeNode.InnerText = ex.GetType().ToString();
            ErrorDetailsNode.AppendChild(ExceptionTypeNode);

            XmlNode MessageNode = doc.CreateElement("Message");
            MessageNode.InnerText = ex.Message;
            ErrorDetailsNode.AppendChild(MessageNode);

            XmlNode InnerExceptionNode = doc.CreateElement("InnerException");
            InnerExceptionNode.InnerText = ex.InnerException != null ? ex.InnerException.Message : "";
            ErrorDetailsNode.AppendChild(InnerExceptionNode);

            XmlNode StackTraceNode = doc.CreateElement("StackTrace");
            StackTraceNode.InnerText = ex.StackTrace;
            ErrorDetailsNode.AppendChild(StackTraceNode);

            doc.AppendChild(ErrorDetailsNode);

            #endregion

            DataContractSerializer s = new DataContractSerializer(typeof(T));
            XmlSerializer xs = new XmlSerializer(typeof(XmlDocument));

            using (System.IO.FileStream fs = File.Open(fullName, FileMode.Create))
            {
                XmlDictionaryWriter writer = XmlDictionaryWriter.CreateTextWriter(fs);
                writer.WriteStartDocument(true);
                writer.WriteStartElement("FailedSaveCall");

                s.WriteStartObject(writer, obj);
                s.WriteObjectContent(writer, obj);
                s.WriteEndObject(writer);

                xs.Serialize(writer, doc);

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public static bool Serialize(string serializedXml, string fileName)
        {
            bool returnValue = false;

            try
            {
                string fullName = string.Format("{0}{1}.xml", ConfigurationManager.AppSettings["SerializeLocation"], fileName);
                File.WriteAllText(fullName, serializedXml);

                returnValue = true;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return returnValue;
        }

        /// <summary>
        /// De-Serialize the failed xml file and try to repost
        /// </summary>
        /// <returns>Bool</returns>
        public static bool DeserializeFailedXML<T>(ref object obj, string FileName)
        {
            bool returnValue = false;

            DataContractSerializer s = new DataContractSerializer(typeof(T));
            using (System.IO.FileStream fs = File.Open(FileName, FileMode.Open))
            {
                obj = s.ReadObject(fs);
                returnValue = true;
            }

            return returnValue;
        }

        #endregion
    }
}