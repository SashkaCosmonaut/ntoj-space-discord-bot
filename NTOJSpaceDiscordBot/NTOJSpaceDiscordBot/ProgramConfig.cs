namespace NTOJSpaceDiscordBot
{
    /// <summary>
    /// Класс для конфигурационного файла программы.
    /// </summary>
    public class ProgramConfig
    {
        /// <summary>
        /// Название порта.
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// Скорость передачи порта.
        /// </summary>
        public int BaudRate { get; set; }
        
        /// <summary>
        /// Время ожидания чтения с порта.
        /// </summary>
        public int ReadTimeout { get; set; }

        /// <summary>
        /// Время ожидания записи в порт.
        /// </summary>
        public int WriteTimeout { get; set; }

        /// <summary>
        /// ID канала, из которого только и будет считывать команды бот.
        /// </summary>
        public int RequiredChannelId { get; set; }
    }
}
