using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace NTOJSpaceDiscordBot.Services
{
    /// <summary>
    /// Сервис для взаимодействия с устройствами по последовательному порту.
    /// </summary>
    public class SerialPortService
    {
        /// <summary>
        /// Контейнер сервисов.
        /// </summary>
        private readonly IServiceProvider _services;

        /// <summary>
        /// Объект последовательного порта.
        /// </summary>
        private readonly SerialPort _serialPort;

        /// <summary>
        /// Конструктор сервиса.
        /// </summary>
        /// <param name="services">Сохраняем контейнер сервисов.</param>
        public SerialPortService(IServiceProvider services)
        {
            _services = services;
        }

        /// <summary>
        /// Инициализация сервиса.
        /// </summary>
        /// <returns>Асинхронная операция.</returns>
        public async Task InitializeAsync()
        {

        }
    }
}
