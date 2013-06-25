using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TsSoft.Commons.Utils;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TsSoft.Commons.Test.Utils
{
    [TestClass]
    public class DirectoryLockerTest
    {
        private DirectoryInfo tempDir;

        [TestInitialize]
        public void TestInit()
        {
            tempDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "DirectoryLockerTest"));
            tempDir.Create();
        }

        [TestCleanup]
        public void TestClean()
        {
            tempDir.Delete(true);
        }

        [TestMethod]
        public void TestWriteLock()
        {
            var testDir = new DirectoryInfo(Path.Combine(tempDir.FullName, Guid.NewGuid().ToString()));
            testDir.Create();
            var locker = new DirectoryLocker(testDir.FullName);
            locker.WriteLock();
            Assert.AreEqual(1, testDir.GetFiles("write*.lock").Count());
        }

        [TestMethod]
        public void TestWriteUnlock()
        {
            var testDir = new DirectoryInfo(Path.Combine(tempDir.FullName, Guid.NewGuid().ToString()));
            testDir.Create();
            var locker = new DirectoryLocker(testDir.FullName);
            locker.WriteLock();
            Assert.AreEqual(1, testDir.GetFiles("write*.lock").Count());
            locker.WriteUnlock();
            Assert.AreEqual(0, testDir.GetFiles("write*.lock").Count());
        }

        [TestMethod]
        public void TestStaticWriteUnlock()
        {
            var testDir = new DirectoryInfo(Path.Combine(tempDir.FullName, Guid.NewGuid().ToString()));
            testDir.Create();
            var locker = new DirectoryLocker(testDir.FullName);
            locker.WriteLock();
            Assert.AreEqual(1, testDir.GetFiles("write*.lock").Count());
            DirectoryLocker.WriteUnlock(locker.LockerId, testDir.FullName);
            Assert.AreEqual(0, testDir.GetFiles("write*.lock").Count());
        }

        [TestMethod]
        public void TestReadWriteLock()
        {
            var testDir = new DirectoryInfo(Path.Combine(tempDir.FullName, Guid.NewGuid().ToString()));
            testDir.Create();
            var locker = new DirectoryLocker(testDir.FullName);
            locker.ReadWriteLock();
            Assert.AreEqual(1, testDir.GetFiles("readwrite.lock").Count());
        }

        [TestMethod]
        public void TestReadWriteUnlock()
        {
            var testDir = new DirectoryInfo(Path.Combine(tempDir.FullName, Guid.NewGuid().ToString()));
            testDir.Create();
            using (var lockFile = File.Create(Path.Combine(testDir.FullName, "readwrite.lock"))) { }
            var locker = new DirectoryLocker(testDir.FullName);
            locker.ReadWriteUnlock();
            Assert.AreEqual(0, testDir.GetFiles("readwrite.lock").Count());
        }

        [TestMethod]
        public void TestIsWriteLocked()
        {
            var testDir = new DirectoryInfo(Path.Combine(tempDir.FullName, Guid.NewGuid().ToString()));
            testDir.Create();
            var locker = new DirectoryLocker(testDir.FullName);
            using (var lockFile = File.Create(Path.Combine(testDir.FullName, "write_" + locker.LockerId.ToString() + ".lock"))) { }
            Assert.IsTrue(locker.IsWriteLocked);
        }

        [TestMethod]
        public void TestIsReadWriteLocked()
        {
            var testDir = new DirectoryInfo(Path.Combine(tempDir.FullName, Guid.NewGuid().ToString()));
            testDir.Create();
            using (var lockFile = File.Create(Path.Combine(testDir.FullName, "readwrite.lock"))) { }
            var locker = new DirectoryLocker(testDir.FullName);
            Assert.IsTrue(locker.IsReadWriteLocked);
        }

        [TestMethod]
        public void TestWaitReadWriteUnlock()
        {
            var testDir = new DirectoryInfo(Path.Combine(tempDir.FullName, Guid.NewGuid().ToString()));
            testDir.Create();
            var testLocker = new DirectoryLocker(testDir.FullName);
            testLocker.ReadWriteLock();
            Task.Factory.StartNew(() => {
                ReadWriteUnLock(testLocker, 5);
            });
            var locker = new DirectoryLocker(testDir.FullName);
            var start = DateTime.Now;
            locker.WaitReadWriteUnlock(10, 1000);
            var finish = DateTime.Now;
            var delta = finish - start;
            Assert.IsTrue(delta.TotalSeconds > 3);
        }

        [TestMethod]
        public void TestWaitWriteUnlock()
        {
            var testDir = new DirectoryInfo(Path.Combine(tempDir.FullName, Guid.NewGuid().ToString()));
            testDir.Create();
            var testLocker = new DirectoryLocker(testDir.FullName);
            testLocker.WriteLock();
            Task.Factory.StartNew(() =>
            {
                WriteUnLock(testLocker, 10);
            });
            var start = DateTime.Now;
            testLocker.WaitWriteUnlock(100, 1000);
            var finish = DateTime.Now;
            var delta = finish - start;
            Assert.IsTrue(delta.TotalSeconds > 7);
        }

        private void ReadWriteUnLock(DirectoryLocker locker, int waitSeconds)
        {
            Thread.Sleep(waitSeconds * 1000);
            locker.ReadWriteUnlock();
        }

        private void WriteUnLock(DirectoryLocker locker, int waitSeconds)
        {
            Thread.Sleep(waitSeconds * 1000);
            locker.WriteUnlock();
        }

    }
}
