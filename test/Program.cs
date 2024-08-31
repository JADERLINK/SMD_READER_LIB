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

            SMD_READER_LIB.SMD smd = null;
            StreamReader stream = null;

            if (args.Length >= 1 && File.Exists(args[0]))
            {
                try
                {
                    stream = new FileInfo(args[0]).OpenText();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error1: " + ex);
                }

                if (stream != null)
                {
                    try
                    {
                        smd = SMD_READER_LIB.SmdReader.Reader(stream);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error2: " + ex);
                    }
                    finally
                    {
                        stream.Close();
                    }

                    if (smd != null)
                    {
                        Console.WriteLine("smd.Nodes.Count: " + smd.Nodes.Count);
                        Console.WriteLine("smd.Times.Count: " + smd.Times.Count);
                        Console.WriteLine("smd.Triangles.Count: " + smd.Triangles.Count);
                        Console.WriteLine("smd.VertexAnimation.Count: " + smd.VertexAnimation.Count);
                    }

                }
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
