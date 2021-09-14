﻿using NSubstitute;
using NUnit.Framework;
using System;

namespace ManualDi.Main.Tests
{
    public class TestDiContainerDispose
    {
        [Test]
        public void TestDispose()
        {
            var container = new ContainerBuilder().Build();
            var instance = new object();
            var disposeAction = Substitute.For<Action>();

            container.Bind<object>()
                .FromInstance(instance)
                .RegisterDispose((o, c) => disposeAction);

            _ = container.FinishAndResolve<object>();

            disposeAction.DidNotReceive().Invoke();

            container.Dispose();

            disposeAction.Received(1).Invoke();
            disposeAction.ClearReceivedCalls();

            container.Dispose();

            disposeAction.DidNotReceive().Invoke();
        }
    }
}
