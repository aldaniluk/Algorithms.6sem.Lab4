using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
    public static class HuffmanEncriptionService
    {
        public static string Encrypt(string source)
        {
            char[] sourceCharacters = source.ToCharArray();

            List<CharEntry> charEntries = Parse(sourceCharacters);
            CharEntryNode rootCharEntry = FormTree(charEntries);
            Dictionary<char, string> encryptingTable = FormeEcryptingTable(rootCharEntry, charEntries);

            return EncryptInternal(sourceCharacters, encryptingTable);
        }

        private static List<CharEntry> Parse(char[] sourceCharacters)
        {
            var result = new Dictionary<char, CharEntry>();

            foreach (char character in sourceCharacters)
            {
                if (result.TryGetValue(character, out CharEntry charEntry))
                {
                    charEntry.Repetitions++;
                }
                else
                {
                    result.Add(character, new CharEntry(character, 1));
                }
            }

            return result.Values.ToList();
        }

        private static CharEntryNode FormTree(List<CharEntry> charEntries)
        {
            List<CharEntryNode> charEntryNodes = charEntries.Select(
                ce => new CharEntryNode(null, null, ce.Character.ToString(), ce.Repetitions)).ToList();

            return FormTreeInternal(charEntryNodes);
        }

        private static CharEntryNode FormTreeInternal(List<CharEntryNode> charEntryNodes)
        {
            while (charEntryNodes.Count() != 1)
            {
                CharEntryNode oneCharEntry = GetMin(charEntryNodes);
                CharEntryNode twoCharEntry = GetMin(charEntryNodes);

                CharEntryNode charEntryNode = new CharEntryNode(oneCharEntry, twoCharEntry,
                    oneCharEntry.Value + twoCharEntry.Value,
                    oneCharEntry.RepetitionsSum + twoCharEntry.RepetitionsSum);

                charEntryNodes.Add(charEntryNode);
            }

            return charEntryNodes.First();
        }

        private static CharEntryNode GetMin(List<CharEntryNode> charEntryNodes)
        {
            CharEntryNode minCharEntry = charEntryNodes
                .First(ce => ce.RepetitionsSum == charEntryNodes.Min(cen => cen.RepetitionsSum));
            charEntryNodes.Remove(minCharEntry);

            return minCharEntry;
        }

        private static Dictionary<char, string> FormeEcryptingTable(CharEntryNode rootCharEntry,
            List<CharEntry> charEntries)
        {
            Dictionary<char, string> encryptingTable = charEntries
                .ToDictionary(ce => ce.Character, ce => (string)null);

            FormEncodingTableInternal(encryptingTable, rootCharEntry, "");

            return encryptingTable;
        }

        private static void FormEncodingTableInternal(Dictionary<char, string> encryptingTable,
            CharEntryNode charEntry, string encoding)
        {
            if (charEntry.LeftNode != null)
            {
                FormEncodingTableInternal(encryptingTable, charEntry.LeftNode, encoding + "0");
            }

            if (charEntry.RightNode != null)
            {
                FormEncodingTableInternal(encryptingTable, charEntry.RightNode, encoding + "1");
            }

            if (charEntry.LeftNode == null && charEntry.RightNode == null)
            {
                encryptingTable[Convert.ToChar(charEntry.Value)] = encoding;
            }
        }

        private static string EncryptInternal(char[] sourceCharacters, Dictionary<char, string> encryptingTable)
        {
            var encryptedSource = new StringBuilder(GetEncryptingTableString(encryptingTable));
            foreach (char character in sourceCharacters)
            {
                encryptedSource.Append(encryptingTable[character]);
            }

            return encryptedSource.ToString();
        }

        private static string GetEncryptingTableString(Dictionary<char, string> encryptingTable)
        {
            var encryptingTableString = new StringBuilder();
            foreach (KeyValuePair<char, string> element in encryptingTable)
            {
                encryptingTableString.Append($"{element.Key}${element.Value}%");
            }

            encryptingTableString.Remove(encryptingTableString.Length - 1, 1);
            encryptingTableString.Append("#");

            return encryptingTableString.ToString();
        }

        public static string Decrypt(string source)
        {
            int newLineIndex = source.IndexOf('#');
            string encryptingTableString = source.Substring(0, newLineIndex);
            string sourceString = source.Substring(newLineIndex + 1, source.Length - newLineIndex - 1);

            Dictionary<string, char> encryptingTable = RestoreEncryptingTable(encryptingTableString);

            return DecryptInternal(sourceString.ToCharArray(), encryptingTable);
        }

        private static Dictionary<string, char> RestoreEncryptingTable(string encryptingTableString)
        {
            var encryptingTable = new Dictionary<string, char>();

            string[] charEncryptCharPairs = encryptingTableString.Split('%');
            foreach (string pair in charEncryptCharPairs)
            {
                int dashIndex = pair.IndexOf("$", StringComparison.Ordinal);
                char key = Convert.ToChar(pair.Substring(0, dashIndex));
                string value = pair.Substring(dashIndex + 1, pair.Length - dashIndex - 1);

                encryptingTable.Add(value, key); //order!
            }

            return encryptingTable;
        }

        private static string DecryptInternal(char[] numbers, Dictionary<string, char> encryptingTable)
        {
            var decryptedSource = new StringBuilder();
            string current = "";
            foreach (char number in numbers)
            {
                current += number;
                if (encryptingTable.TryGetValue(current, out char character))
                {
                    decryptedSource.Append(character);
                    current = "";
                }
            }

            return decryptedSource.ToString();
        }
    }
}
