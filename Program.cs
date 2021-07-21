using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace atualizafotofm
{
    class Program
    {
        private const string caminho = @"C:\Users\cristiano\Documents\Sports Interactive\Football Manager 2021\graphics\Players\sortitoutsi\faces";

        static void Main(string[] args)
        {
            IList<string> arquivos = Directory.GetFiles(caminho).ToList().Where(c => c.Contains(".png")).ToList();
            if (arquivos.Count > 0)
            {
                ExcluirConfig();

                using (StreamWriter sw = new StreamWriter(CaminhoConfig()))
                {
                    sw.WriteLine(@"<record>");
                    sw.WriteLine(@"    <!-- resource manager options -->");
                    sw.WriteLine(@"");
                    sw.WriteLine(@"    <!-- dont preload anything in this folder -->");
                    sw.WriteLine(@"    <boolean id=""preload"" value=""False""/>");
                    sw.WriteLine(@"");
                    sw.WriteLine(@"    <!-- turn off auto mapping -->");
                    sw.WriteLine(@"    <boolean id=""amap"" value=""False""/>");
                    sw.WriteLine(@"");
                    sw.WriteLine(@"    <!-- logo mappings -->");
                    sw.WriteLine(@"    <!-- the following XML maps pictures inside this folder into other positions");
                    sw.WriteLine(@"             in the resource system, which allows this folder to be dropped into any");
                    sw.WriteLine(@"             place in the graphics folder and still have the game pick up the graphics");
                    sw.WriteLine(@"             files from the correct places");
                    sw.WriteLine(@"    -->");
                    sw.WriteLine(@"");
                    sw.WriteLine(@"    <list id=""maps"">");

                    foreach (string arquivo in arquivos)
                    {
                        sw.WriteLine(string.Format(@"        <record from=""{0}"" to=""graphics/pictures/person/{0}/portrait""/>", 
                                                    arquivo.Substring(arquivo.LastIndexOf(@"\") + 1).ToLower().Replace(".png", "")));
                    }

                    sw.WriteLine(@"    </list>");
                    sw.WriteLine(@"</record>");
                }
            }

            Console.WriteLine("Hello World!");
        }

        static void ExcluirConfig()
        {
            if (File.Exists(CaminhoConfig()))
                File.Delete(CaminhoConfig());
        }

        static string CaminhoConfig()
        {
            return string.Format(@"{0}\Config.xml", caminho);
        }
    }
}
