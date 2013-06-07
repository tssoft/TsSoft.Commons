namespace TsSoft.Commons.Utils
{
    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
    using System.Threading;

    /// <summary>
    /// Синхронизация доступа к папкам
    /// </summary>
    public class DirectoryLocker
    {
        private static readonly string lockFileExtension = "lock";

        private static readonly string baseWriteLockFileName = "write";

        private static readonly string baseReadWriteLockFileName = "readwrite";

        private static readonly string lockerIdSeparator = "_";

        private DirectoryInfo directory { get; set; }

        private Guid lockerId { get; set; }

        private string writeLockFileName 
        { 
            get
            {
                return baseWriteLockFileName + lockerIdSeparator + lockerId.ToString() + "." + lockFileExtension;
            }
        }

        private string readWriteLockFileName
        {
            get 
            { 
                return baseReadWriteLockFileName + "." + lockFileExtension;
            }
        }

        private FileInfo writeLockFileInfo
        {
            get
            {
                return new FileInfo(Path.Combine(directory.FullName, writeLockFileName));
            }
        }

        private FileInfo readWriteLockFileInfo
        {
            get
            {
                return new FileInfo(Path.Combine(directory.FullName, readWriteLockFileName));
            }
        }

        public bool IsWriteLocked
        {
            get
            {
                return directory.GetFiles(baseWriteLockFileName + "*." + lockFileExtension).Count() > 0;
            }
        }

        public bool IsReadWriteLocked
        {
            get
            {
                return readWriteLockFileInfo.Exists;
            }
        }

        /// <summary>
        /// Инициализирует новый экземпляр
        /// </summary>
        /// <param name="path">Путь до папки</param>
        public DirectoryLocker(string path)
        {
            directory = new DirectoryInfo(path);
            lockerId = Guid.NewGuid();
        }

        /// <summary>
        /// Блокирует директорию для записи
        /// </summary>
        public void WriteLock()
        {
            if (IsWriteLocked)
            {
                throw new DirectoryLockerException("Directory is already locked for writing.");
            }
            using (var fileStream = writeLockFileInfo.Create()) { }
        }

        /// <summary>
        /// Разблокирует директорию для записи
        /// </summary>
        public void WriteUnlock()
        {
            if (!IsWriteLocked)
            {
                throw new DirectoryLockerException("Directory is already unlocked for writing.");
            }
            writeLockFileInfo.Delete();
        }

        /// <summary>
        /// Блокирует директорию для чтения и записи
        /// </summary>
        public void ReadWriteLock()
        {
            if (IsReadWriteLocked)
            {
                throw new DirectoryLockerException("Directory is already locked for reading and writing.");
            }
            using (var fileStream = readWriteLockFileInfo.Create()) { }
        }

        /// <summary>
        /// Разблокирует директорию для чтения записи
        /// </summary>
        public void ReadWriteUnlock()
        {
            if (!IsReadWriteLocked)
            {
                throw new DirectoryLockerException("Directory is already unlocked for reading and writing.");
            }
            readWriteLockFileInfo.Delete();
        }

        /// <summary>
        /// Приостанавливает поток, пока директория не будет разблокирована для записи
        /// </summary>
        /// <param name="count">Количество проверок директории</param>
        /// <param name="timeSpan">Время ожидания между проверками в миллисекундах</param>
        public void WaitWriteUnlock(int count = 10, int timeSpan = 500)
        {
            var iterator = 0;
            while (IsWriteLocked)
            {
                if (iterator >= count)
                {
                    var message = "Время ожидания директории истекло";
                    throw new DirectoryLockerException(message);
                }
                Thread.Sleep(timeSpan);
                iterator++;
            }
        }

        /// <summary>
        /// Приостанавливает поток, пока директория не будет разблокирована для чтения и записи
        /// </summary>
        /// <param name="count">Максимальное количество проверок директории</param>
        /// <param name="timeSpan">Время ожидания между проверками в миллисекундах</param>
        public void WaitReadWriteUnlock(int count = 10, int timeSpan = 500)
        {
            var iterator = 0;
            while (IsReadWriteLocked)
            {
                if (iterator >= count)
                {
                    var message = "Время ожидания директории истекло";
                    throw new DirectoryLockerException(message);
                }
                Thread.Sleep(timeSpan);
                iterator++;
            }
        }

    }

    public class DirectoryLockerException : Exception
    {
        public DirectoryLockerException(string message)
            :base(message)
        {

        }

        public DirectoryLockerException(string message, Exception innerException)
            :base(message, innerException)
        {

        }
    }
}
