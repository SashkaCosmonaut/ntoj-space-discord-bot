using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace NTOJSpaceDiscordBot.Services
{
    /// <summary>
    /// Сервис для взаимодействия с устройствами по последовательному порту.
    /// </summary>
    public class SerialPortService : IDisposable    
    {
        // Константы для общения с другим устройством
        private const string FORWARD = "f";
        private const string BACK = "b";
        private const string LEFT = "l";
        private const string RIGHT = "r";
        private const string OK = "k";

        /// <summary>
        /// Объект последовательного порта.
        /// </summary>
        private readonly SerialPort _serialPort;

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public SerialPortService()
        {
            _serialPort = new SerialPort
            {
                PortName = "COM4",
                BaudRate = 9600
            };
        }

        /// <summary>
        /// Асинхронная инициализация сервиса.
        /// </summary>
        /// <returns>Асинхронная операция.</returns>
        public async Task InitializeAsync()
        {
            await Task.Run(() =>
            {
                _serialPort?.Open();
            });
        }

        /// <summary>
        /// Закрытие порта и освобождение ресурсов.
        /// </summary>
        public void Dispose()
        {
            _serialPort?.Close();
        }

        /// <summary>
        /// Отправить команду движения вперёд.
        /// </summary>
        /// <returns>Результат выполнения команды.</returns>
        public async Task<bool> SendForwardAsync()
        {
            // Проверяем, что порт создан и открыт
            if (_serialPort == null || !_serialPort.IsOpen)
                return false;

            // Параллельно оправляем команду и ждём результат
            var taskResult = await Task.Run(() => {

                _serialPort.WriteLine(FORWARD);

                return _serialPort.ReadLine();
            });

            // Проверяем, что пришёл корректный результат
            return taskResult == OK;
        }
    }
}
