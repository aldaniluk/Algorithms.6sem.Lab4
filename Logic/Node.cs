namespace Logic
{
    public class CharEntryNode
    {
        public CharEntryNode LeftNode { get; set; }

        public CharEntryNode RightNode { get; set; }

        public string Value { get; set; }

        public int RepetitionsSum { get; set; }

        public CharEntryNode(CharEntryNode left, CharEntryNode right, string value, int repetitions)
        {
            LeftNode = left;
            RightNode = right;
            Value = value;
            RepetitionsSum = repetitions;
        }
    }
}
