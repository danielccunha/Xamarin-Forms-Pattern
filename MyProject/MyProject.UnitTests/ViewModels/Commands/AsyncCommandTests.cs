using MyProject.UnitTests.Base;
using MyProject.ViewModels.Base.Commands;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MyProject.UnitTests.ViewModels.Commands
{
    public class AsyncCommandTests : BaseTestFixture
    {
        [Test]
        public void Constructor_ExecuteIsNull_ThrowArgumentNullException()
        {
            Func<Task> execute = null;
            Assert.That(() => new AsyncCommand(execute), Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_ExecuteWithParameterIsNull_ThrowArgumentNullException()
        {
            Func<object, Task> execute = null;
            Assert.That(() => new AsyncCommand(execute), Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_CanExecuteIsNull_ThrowArgumentNullException()
        {
            Func<bool> canExecute = null;
            Assert.That(() => new AsyncCommand(ExecuteMethod, canExecute), Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_CanExecuteWithParameterIsNull_ThrowArgumentNullException()
        {
            Func<object, bool> canExecute = null;
            Assert.That(() => new AsyncCommand(ExecuteMethodWithParameter, canExecute), Throws.ArgumentNullException);
        }

        private static Task ExecuteMethod() => Task.FromResult(true);
        private static Task ExecuteMethodWithParameter(object obj) => Task.FromResult<object>(true);
    }
}
