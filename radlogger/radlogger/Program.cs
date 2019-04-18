using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Timers;
using System.Net;
using System.IO;

namespace radlogger
{
    class Program
    {
        //static WebRequest poster;// = WebRequest.Create("http://10.0.0.118:8086/write?db=radlog");
        static void WritePoint(string data)
        {
            WebRequest poster = WebRequest.Create("http://10.0.0.118:8086/write?db=radlog");
            
            poster.Method = "POST";
            poster.ContentType = "application/x-www-form-urlencoded";
            poster.ContentLength = data.Length;
            Stream datStream = poster.GetRequestStream();

            //Write data to the stream
            datStream.Write(Encoding.ASCII.GetBytes(data), 0, data.Length);
            datStream.Close();
            WebResponse response = poster.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            response.Close();
        }

        static void RecordCount(Object source, ElapsedEventArgs e)
        {
            _port.Write("<GETCPM>>");
            if (_port.BytesToRead > 0)
            {
                byte[] buff = new byte[500];
                for (int i = 0; i < 500; ++i) buff[i] = 0x00;
                _port.Read(buff, 0, _port.BytesToRead);
                byte msb = buff[0];
                byte lsb = buff[1];
                //uint cpm = ((uint)(msb << 8) & 0b00111111);
                uint cpm = ((uint)(msb << 8));
                cpm |= (uint)lsb;

                Console.WriteLine(cpm.ToString());
                WritePoint("rad_data,location=desktop value=" + cpm.ToString());
            }
        }

        static SerialPort _port;
        static System.Timers.Timer countTimer;
        static void Main(string[] args)
        {
            _port = new SerialPort();
            _port.PortName = "COM8";
            _port.BaudRate = 115200;
            _port.DataBits = 8;
            _port.Parity = Parity.None;
            _port.StopBits = StopBits.One;

            countTimer = new System.Timers.Timer(1000);
            countTimer.Elapsed += RecordCount;
            countTimer.AutoReset = true;
            countTimer.Enabled = true;

            //WritePoint("test_data,host=server01 value=41");

            try
            {
                _port.Open();
                

                while (true)
                {
                   
                    

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }
    }
}
