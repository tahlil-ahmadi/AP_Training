using System;
using System.Collections.Generic;
using System.Text;
using Framework.Core;

namespace Framework.Application
{
    public class CommandBus : ICommandBus
    {
        private IServiceLocator _locator;
        public CommandBus(IServiceLocator locator)
        {
            this._locator = locator;
        }

        public void Dispatch<T>(T command)
        {
            var handler = _locator.Resolve<ICommandHandler<T>>();
            handler.Handle(command);
        }
    }
}
