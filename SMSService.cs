using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Lib.SMS
{
    public class SMSService
    {
        private string smsGatewayID;
        private string smsGatewayName;
        public SMSService(string smsGatewayID, string smsGatewayName)
        {
            this.smsGatewayID = smsGatewayID;
            this.smsGatewayName = smsGatewayName;
        }
        public void Send(string nr, string message)
        {
            try
            {
                if (message.Count() <= 150)
                {
                    SendSMS(nr, message);
                }
                else
                {
                    message.Remove(145);
                    SendSMS(nr, message);
                }
            }
            catch (Exception ex)
            {
                // OBS
            }
        }


        /// <summary>
        /// Sender SMS via URI og post 
        /// </summary>
        /// <param name="nr">Mobilnr</param>
        /// <param name="Message">Besked maks 135 karaktere.</param>
        private void SendSMS(string nr, string Message)
        {

            Message = Message.Replace(" ", "+");
            Message = Message.Replace("\n\\", "%0A");
            Message = Message.Replace("\n", "%0A");
            Message = Message.Replace("\r\\", "%0D");
            Message = Message.Replace("\r", "%0D");
            Message = Message.Replace("Ø", "%D8");
            Message = Message.Replace("ø", "%F8");
            Message = Message.Replace("æ", "%E6");
            Message = Message.Replace("Æ", "%C6");
            Message = Message.Replace("å", "%E5");
            Message = Message.Replace("Å", "%C5");

            // Create a request using a URL that can receive a post. 
            Uri Urlhandler = new Uri("x");  // OBS
            WebRequest request = WebRequest.Create(Urlhandler);
            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Get the request stream.
            // Create POST data and convert it to a byte array.
            string postData = "";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();

            response.Close();
        }
    }
}
