using Logic;
using System;
using System.Collections;
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
            string pathInput = Directory.GetCurrentDirectory() + @"\..\..\..\Logic\Data\hello.txt";
            string pathOutput1 = Directory.GetCurrentDirectory() + @"\..\..\..\Logic\Data\hello_encrypted.txt";
            string pathOutput2 = Directory.GetCurrentDirectory() + @"\..\..\..\Logic\Data\hello_decrypted.txt";

            using (var reader = new StreamReader(pathInput))
            {
                initialString = reader.ReadToEnd();
            }

            //string encrypted = HuffmanEncryptionService.Encrypt(initialString);
            //WriteCompressedFile(encrypted, pathOutput1);

            //string encryptedFromFile = GetEncryptedFromFile(pathOutput1);
            //string decrypted = HuffmanEncryptionService.Decrypt(encryptedFromFile);
            //Console.WriteLine($"decrypted: {decrypted}");
            //using (var writer = new BinaryWriter(File.OpenWrite(pathOutput2), Encoding.UTF8))
            //{
            //    writer.Write(decrypted);
            //}


            string encrypted = HuffmanEncryptionService.Encrypt(initialString);
            string decrypted = HuffmanEncryptionService.Decrypt(encrypted);

            Console.WriteLine($"initial string:\n" +
            $"{initialString}\n\n" +
            $"encrypted:\n" +
            $"{encrypted}\n\n" +
            $"decrypted:\n" +
            $"{decrypted}");
        }

        private static void WriteCompressedFile(string encrypted, string path)
        {
            int separatorIndex = encrypted.IndexOf('#');
            string encryptingTableString = encrypted.Substring(0, separatorIndex + 1);
            string sourceString = encrypted.Substring(separatorIndex + 1, encrypted.Length - separatorIndex - 1);

            var bytes = new List<byte>();

            bytes.AddRange(encryptingTableString.ToCharArray().Select(c => (byte)c));

            var bitArray = new BitArray(sourceString.Select(c => c == '1').ToArray());
            byte[] bytesFromBits = new byte[bitArray.Length / 8 + (bitArray.Length % 8 == 0 ? 0 : 1)];
            bitArray.CopyTo(bytesFromBits, 0);
            bytes.AddRange(bytesFromBits);

            using (var writer = new BinaryWriter(File.OpenWrite(path), Encoding.UTF8))
            {
                writer.Write(bytes.ToArray());
            }
        }

        private static string GetEncryptedFromFile(string path)
        {
            string encryptedFromFile = "";

            byte[] bytes = File.ReadAllBytes(path);
            List<byte> until = bytes.TakeWhile(b => b != 35).ToList();
            List<byte> after = bytes.SkipWhile(b => b != 35).ToList();
            after.RemoveAt(0);
            encryptedFromFile = Encoding.UTF8.GetString(until.ToArray());
            var array = new BitArray(after.ToArray());
            encryptedFromFile += "#";
            foreach (var bit in array)
            {
                encryptedFromFile += (bool)bit ? "1" : "0";
            }

            return encryptedFromFile;
        }
    }
}
