using Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string initialString;
            string pathInput = Directory.GetCurrentDirectory() + @"\..\..\..\Logic\Data\1.txt";
            string pathOutput1 = Directory.GetCurrentDirectory() + @"\..\..\..\Logic\Data\1_encrypted.txt";
            string pathOutput2 = Directory.GetCurrentDirectory() + @"\..\..\..\Logic\Data\1_decrypted.txt";
            using (var reader = new StreamReader(pathInput))
            {
                initialString = reader.ReadToEnd();
            }
            //initialString = "Hello!!! My name is Sasha. Nice to meet you:)";
            string encrypted = HuffmanEncryptionService.Encrypt(initialString);
            string decrypted = HuffmanEncryptionService.Decrypt(encrypted);

            WriteCompressedFile(encrypted, pathOutput1);

            using (var writer = new StreamWriter(pathOutput2))
            {
                writer.Write(decrypted);
            }

            //Console.WriteLine($"initial string:\n" +
            //$"{initialString}\n\n" +
            //$"encrypted:\n" +
            //$"{encrypted}\n\n" +
            //$"decrypted:\n" +
            //$"{decrypted}");


        }

        private static void WriteCompressedFile(string encrypted, string path)
        {
            using (var writer = new BinaryWriter(File.OpenWrite(path)))
            {
                writer.Write(encrypted.ToArray());
            }

            //File.WriteAllBytes(path, Convert.ToBase64String(bytes.ToArray()));
        }
    }
}
