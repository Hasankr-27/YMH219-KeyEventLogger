namespace YMH219_KeyEventLogger
{
    // Tek bir tuş kaydı: hangi tuş, ne zaman basıldı
    public class KeyLogEntry
    {
        public DateTime Time { get; }
        public string KeyName { get; }

        public KeyLogEntry(DateTime time, string keyName)
        {
            Time = time;
            KeyName = keyName;
        }

        // ListBox nesneyi ekranda bu metinle gösterir
        public override string ToString()
        {
            return $"[{Time:HH:mm:ss.fff}]  {KeyName}";
        }
    }
}