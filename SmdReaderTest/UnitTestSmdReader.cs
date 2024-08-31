using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SMD_READER_LIB;
using System.IO;
using System.Text;

namespace SmdReaderTest
{

    [TestClass]
    public class UnitTestSmdReader
    {

        [TestMethod]
        public void TestMethodVersion1()
        {
            //verifica se a primeira linha valida começa com "version"
            //o objeto smd não é nulo
            //as lista internas não são nulas, porem o "count" é 0, nesse caso

            byte[] data = Encoding.UTF8.GetBytes("version 1");
            var reader = new StreamReader(new MemoryStream(data), Encoding.UTF8);
            SMD smd = SmdReader.Reader(reader);
            Assert.IsNotNull(smd);
            Assert.IsNotNull(smd.Nodes);
            Assert.IsNotNull(smd.Times);
            Assert.IsNotNull(smd.Triangles);
            Assert.IsNotNull(smd.VertexAnimation);
            Assert.IsTrue(smd.Nodes.Count == 0);
            Assert.IsTrue(smd.Times.Count == 0);
            Assert.IsTrue(smd.Triangles.Count == 0);
            Assert.IsTrue(smd.VertexAnimation.Count == 0);
        }

        [TestMethod]
        public void TestMethodCommandEmpty()
        {
            //verifica quando o conteudo dos comandos são vazios

            byte[] data = Encoding.UTF8.GetBytes(fileCommandEmpty);
            var reader = new StreamReader(new MemoryStream(data), Encoding.UTF8);
            SMD smd = SmdReader.Reader(reader);
            Assert.IsNotNull(smd);
            Assert.IsNotNull(smd.Nodes);
            Assert.IsNotNull(smd.Times);
            Assert.IsNotNull(smd.Triangles);
            Assert.IsNotNull(smd.VertexAnimation);
            Assert.IsTrue(smd.Nodes.Count == 0);
            Assert.IsTrue(smd.Times.Count == 0);
            Assert.IsTrue(smd.Triangles.Count == 0);
            Assert.IsTrue(smd.VertexAnimation.Count == 0);
        }

        private static string fileCommandEmpty =
@"version 1
nodes
end
skeleton
end
triangles
end
vertexanimation
end
";

        [TestMethod]
        public void TestMethodThrowNotVersion1()
        {
            // se o primeiro conteudo valido for diferente de "version"
            // será gerado uma ArgumentException

            byte[] data = Encoding.UTF8.GetBytes("error");
            var reader = new StreamReader(new MemoryStream(data), Encoding.UTF8);

            try
            {
                SmdReader.Reader(reader);
            }
            catch (ArgumentException)
            {
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void TestMethodThrowEmpty()
        {
            // se o conteudo for vazio
            // será gerado uma ArgumentException

            byte[] data = Encoding.UTF8.GetBytes("");
            var reader = new StreamReader(new MemoryStream(data), Encoding.UTF8);

            try
            {
                SmdReader.Reader(reader);
            }
            catch (ArgumentException)
            {
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void TestMethodThrowInvalidCommand()
        {
            // se um dos comando for invalido
            // será gerado uma ArgumentException

            byte[] data = Encoding.UTF8.GetBytes("version 1\r\nerror");
            var reader = new StreamReader(new MemoryStream(data), Encoding.UTF8);

            try
            {
                SmdReader.Reader(reader);
            }
            catch (ArgumentException)
            {
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void TestMethodThrowNotEndTag()
        {
            // se não tiver a tag end
            // será gerado um ArgumentException

            byte[] data = Encoding.UTF8.GetBytes("version 1\r\nnodes\r\n");
            var reader = new StreamReader(new MemoryStream(data), Encoding.UTF8);

            try
            {
                SmdReader.Reader(reader);
            }
            catch (ArgumentException)
            {
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void TestMethodNodes()
        {
            //verifica se o conteudo da tag "nodes" esta correto

            byte[] data = Encoding.UTF8.GetBytes(fileNodes);
            var reader = new StreamReader(new MemoryStream(data), Encoding.UTF8);
            SMD smd = SmdReader.Reader(reader);

            Assert.IsTrue(smd.Nodes.Count == 3);

            Assert.IsTrue(smd.Nodes[0].ID == 0);
            Assert.IsTrue(smd.Nodes[1].ID == 1);
            Assert.IsTrue(smd.Nodes[2].ID == 3);

            Assert.IsTrue(smd.Nodes[0].ParentID == -1);
            Assert.IsTrue(smd.Nodes[1].ParentID == 2);
            Assert.IsTrue(smd.Nodes[2].ParentID == 4);

            Assert.IsTrue(smd.Nodes[0].BoneName == "root");
            Assert.IsTrue(smd.Nodes[1].BoneName == "bone1");
            Assert.IsTrue(smd.Nodes[2].BoneName == "bone2");

        }

        private static string fileNodes =
@"version 1
nodes
0 ""root"" -1
1 ""bone1"" 2
3 ""bone2"" 4
end
";
        [TestMethod]
        public void TestMethodSkeleton()
        {
            //verifica se o conteudo da tag "skeleton" esta correto

            byte[] data = Encoding.UTF8.GetBytes(fileSkeleton);
            var reader = new StreamReader(new MemoryStream(data), Encoding.UTF8);
            SMD smd = SmdReader.Reader(reader);

            Assert.IsTrue(smd.Times.Count == 3);

            Assert.IsTrue(smd.Times[0].ID == 0);
            Assert.IsTrue(smd.Times[1].ID == 1);
            Assert.IsTrue(smd.Times[2].ID == 2);

            Assert.IsTrue(smd.Times[0].Skeletons.Count == 2);
            Assert.IsTrue(smd.Times[1].Skeletons.Count == 1);
            Assert.IsTrue(smd.Times[2].Skeletons.Count == 0);

            Assert.IsTrue(smd.Times[0].Skeletons[0].BoneID == 0);
            Assert.IsTrue(smd.Times[0].Skeletons[1].BoneID == 21);


            Assert.IsTrue(smd.Times[0].Skeletons[0].PosX == 3f);
            Assert.IsTrue(smd.Times[0].Skeletons[0].PosY == 6f);
            Assert.IsTrue(smd.Times[0].Skeletons[0].PosZ == 9f);
            Assert.IsTrue(smd.Times[0].Skeletons[0].RotX == 12f);
            Assert.IsTrue(smd.Times[0].Skeletons[0].RotY == 15f);
            Assert.IsTrue(smd.Times[0].Skeletons[0].RotZ == 18f);
        }

        private static string fileSkeleton =
@"version 1
skeleton
time 0
0 3 6 9 12 15 18
21 24 27 30 33 36 39
time 1
42 45 48 51 54 57 60
time 2
end
";
        [TestMethod]
        public void TestMethodTriangles()
        {
            //verifica se o conteudo da tag "triangles" esta correto

            byte[] data = Encoding.UTF8.GetBytes(fileTriangles);
            var reader = new StreamReader(new MemoryStream(data), Encoding.UTF8);
            SMD smd = SmdReader.Reader(reader);

            Assert.IsTrue(smd.Triangles.Count == 2);

            Assert.IsTrue(smd.Triangles[0].ID == 0);
            Assert.IsTrue(smd.Triangles[1].ID == 1);

            Assert.IsTrue(smd.Triangles[0].Material == "MATERIAL_001");
            Assert.IsTrue(smd.Triangles[1].Material == "MATERIAL_002");

            Assert.IsTrue(smd.Triangles[0].Vertexs.Count == 3);
            Assert.IsTrue(smd.Triangles[1].Vertexs.Count == 3);

            Assert.IsTrue(smd.Triangles[0].Vertexs[0].VertexID == 0);
            Assert.IsTrue(smd.Triangles[0].Vertexs[1].VertexID == 1);
            Assert.IsTrue(smd.Triangles[0].Vertexs[2].VertexID == 2);
            Assert.IsTrue(smd.Triangles[1].Vertexs[0].VertexID == 3);
            Assert.IsTrue(smd.Triangles[1].Vertexs[1].VertexID == 4);
            Assert.IsTrue(smd.Triangles[1].Vertexs[2].VertexID == 5);

            Assert.IsTrue(smd.Triangles[0].Vertexs[0].ParentBone == 10);
            Assert.IsTrue(smd.Triangles[0].Vertexs[1].ParentBone == 11);
            Assert.IsTrue(smd.Triangles[0].Vertexs[2].ParentBone == 12);

            Assert.IsTrue(smd.Triangles[0].Vertexs[1].PosX == -3f);
            Assert.IsTrue(smd.Triangles[0].Vertexs[1].PosY == -6f);
            Assert.IsTrue(smd.Triangles[0].Vertexs[1].PosZ == -9f);

            Assert.IsTrue(smd.Triangles[0].Vertexs[1].NormX == -12f);
            Assert.IsTrue(smd.Triangles[0].Vertexs[1].NormY == -15f);
            Assert.IsTrue(smd.Triangles[0].Vertexs[1].NormZ == -18f);

            Assert.IsTrue(smd.Triangles[0].Vertexs[1].U == -21f);
            Assert.IsTrue(smd.Triangles[0].Vertexs[1].V == -24f);

            Assert.IsTrue(smd.Triangles[0].Vertexs[0].Links.Count == 0);
            Assert.IsTrue(smd.Triangles[0].Vertexs[1].Links.Count == 1);
            Assert.IsTrue(smd.Triangles[0].Vertexs[2].Links.Count == 2);

            Assert.IsTrue(smd.Triangles[0].Vertexs[2].Links[0].BoneID == 9);
            Assert.IsTrue(smd.Triangles[0].Vertexs[2].Links[0].Weight == 12f);

            Assert.IsTrue(smd.Triangles[0].Vertexs[2].Links[1].BoneID == 15);
            Assert.IsTrue(smd.Triangles[0].Vertexs[2].Links[1].Weight == 18f);
        }

        private static string fileTriangles =
@"version 1
triangles
MATERIAL_001
10   3  6  9   12  15  18   21  24  0
11  -3 -6 -9  -12 -15 -18  -21 -24  1 3 6
12   1  1  1    1   1   1    0   0  2 9 12 15 18
MATERIAL_002
13   1  1  1    1   1   1    0   0  2 9 12 15 18
14  -3 -6 -9  -12 -15 -18  -21 -24  1 3  6
15   3  6  9   12  15  18   21  24  0
end
";

        [TestMethod]
        public void TestMethodVertexAnimation()
        {
            //verifica se o conteudo da tag "vertexanimation" esta correto

            byte[] data = Encoding.UTF8.GetBytes(fileVertexAnimation);
            var reader = new StreamReader(new MemoryStream(data), Encoding.UTF8);
            SMD smd = SmdReader.Reader(reader);

            Assert.IsTrue(smd.VertexAnimation.Count == 3);

            Assert.IsTrue(smd.VertexAnimation[0].ID == 10);
            Assert.IsTrue(smd.VertexAnimation[1].ID == 11);
            Assert.IsTrue(smd.VertexAnimation[2].ID == 12);

            Assert.IsTrue(smd.VertexAnimation[0].Vextexs.Count == 2);
            Assert.IsTrue(smd.VertexAnimation[1].Vextexs.Count == 1);
            Assert.IsTrue(smd.VertexAnimation[2].Vextexs.Count == 0);

            Assert.IsTrue(smd.VertexAnimation[0].Vextexs[0].VertexID == 15);
            Assert.IsTrue(smd.VertexAnimation[0].Vextexs[1].VertexID == 16);
            Assert.IsTrue(smd.VertexAnimation[1].Vextexs[0].VertexID == 17);

            Assert.IsTrue(smd.VertexAnimation[0].Vextexs[0].PosX == 3);
            Assert.IsTrue(smd.VertexAnimation[0].Vextexs[0].PosY == 6);
            Assert.IsTrue(smd.VertexAnimation[0].Vextexs[0].PosZ == 9);
            Assert.IsTrue(smd.VertexAnimation[0].Vextexs[0].NormX == 12);
            Assert.IsTrue(smd.VertexAnimation[0].Vextexs[0].NormY == 15);
            Assert.IsTrue(smd.VertexAnimation[0].Vextexs[0].NormZ == 18);
        }

        private static string fileVertexAnimation =
@"version 1
vertexanimation
time 10
15  3  6  9   12  15  18
16 -3 -6 -9  -12 -15 -18
time 11
17 1  1  1    1   1   1
time 12
end
";

        [TestMethod]
        public void TestMethodInvalidContent()
        {
            //verifica quando o conteudo dos comando são invalidos

            byte[] data = Encoding.UTF8.GetBytes(fileInvalidContent);
            var reader = new StreamReader(new MemoryStream(data), Encoding.UTF8);
            SMD smd = SmdReader.Reader(reader);
            Assert.IsTrue(smd.Nodes.Count == 1);
            Assert.IsTrue(smd.Times.Count == 1);
            Assert.IsTrue(smd.Triangles.Count == 1);
            Assert.IsTrue(smd.VertexAnimation.Count == 1);

            Assert.IsTrue(smd.Times[0].Skeletons.Count == 1);

            Assert.IsTrue(smd.Triangles[0].Vertexs.Count == 1);

            Assert.IsTrue(smd.VertexAnimation[0].Vextexs.Count == 1);

        }

        private static string fileInvalidContent =
@"version 1
nodes
aa bb cc
end
skeleton
//aa bb cc dd ee ff gg hh System.NullReferenceException
time bb
aa bb cc dd ee ff gg hh
end
triangles
material
a b c d e f g h i j
end
vertexanimation
//a b c d e f g System.NullReferenceException
time cc
a b c d e f g
end
";



    }
}
