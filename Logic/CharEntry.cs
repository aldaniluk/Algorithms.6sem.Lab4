namespace Logic
{
    public class CharEntry
    {
        public char Character { get; set; }

        public int Repetitions { get; set; }

        public CharEntry(char character, int repetitions)
        {
            Character = character;
            Repetitions = repetitions;
        }
    }
}
