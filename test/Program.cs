using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("start");

            if (args.Length >= 1 && File.Exists(args[0]))
            {
                SMD_READER_API.SMD smd = null;

                var stream = new FileInfo(args[0]).OpenText();
                try
                {
                    smd = SMD_READER_API.SmdReader.Reader(stream);
                }
                catch (Exception ex)
                {
                    stream.Close();
                    Console.WriteLine(ex);
                }

                if (smd != null)
                {
                    Console.WriteLine("smd.Nodes.Count: " + smd.Nodes.Count);
                    Console.WriteLine("smd.Times.Count: " + smd.Times.Count);
                    Console.WriteLine("smd.Triangles.Count: " + smd.Triangles.Count);
                    Console.WriteLine("smd.VertexAnimation.Count: " + smd.VertexAnimation.Count);
                }

                int a = 0;
            }
            else 
            {
                Console.WriteLine("Invalid File");
            }

            Console.WriteLine("end");
            Console.ReadLine();
        }


    }
}
