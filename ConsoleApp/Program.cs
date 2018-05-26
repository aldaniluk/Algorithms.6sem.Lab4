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
            //initialString = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            string encrypted = HuffmanEncriptionService.Encrypt(initialString);
            string decrypted = HuffmanEncriptionService.Decrypt(encrypted);

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
            var bytes = new List<byte>();
            foreach (var character in encrypted.ToCharArray())
            {
                bytes.Add((byte)character);
            }

            using (var writer = new BinaryWriter(File.OpenWrite(path), Encoding.ASCII))
            {
                writer.Write(bytes.ToArray());
            }

            //File.WriteAllBytes(path, Convert.ToBase64String(bytes.ToArray()));
        }
    }
}
