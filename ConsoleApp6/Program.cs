using System;
using System.Net;
using System.IO;

class Program
{
    static void Main()
    {
        string url = "https://challenge.photier.com/start";

        string yourEmail = "cpt.mfs@gmail.com";  

        string postData = $"email={yourEmail}";  

        byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(postData);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = dataBytes.Length;

        using (Stream requestStream = request.GetRequestStream())
        {
            requestStream.Write(dataBytes, 0, dataBytes.Length);
        }

        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string responseText = reader.ReadToEnd();

            Console.WriteLine(responseText);
        }
        catch (WebException ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
        }
    }
}
