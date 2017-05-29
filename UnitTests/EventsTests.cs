namespace PipeliningLibrary.UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class EventsTests
    {
        private static readonly PipelineGroup _sortPipelines = new SortPipelines();

        private static readonly PipelineGroup _textPipelines = new TextPipelines();

        private static readonly int[] _sortInput = new[] { 2, 1, 3, 4, 7, 9, 8, 6, 5, };

        private static readonly string _textInput = Tamagotchi.English;

        private Action<PipelineEvent, PipelineEvent> _areEqual = (expected, actual) =>
        {
            Assert.AreEqual(expected.GetType(), actual.GetType());
            Assert.AreEqual(expected.Pipe, actual.Pipe);
            Assert.AreEqual(expected.Current, actual.Current);
        };

        [TestMethod]
        public void Simple()
        {
            var events = new Queue<PipelineEvent>();

            _textPipelines["sanitize_input"].RunDetailed(_textInput, e => events.Enqueue(e));

            _areEqual(new PipeStarted { Pipe = typeof(RemoveNonAlphaPipe), Current = 1, }, events.Dequeue());
            _areEqual(new PipeEnded { Pipe = typeof(RemoveNonAlphaPipe), Current = 1, }, events.Dequeue());

            _areEqual(new PipeStarted { Pipe = typeof(RemoveCasePipe), Current = 2, }, events.Dequeue());
            _areEqual(new PipeEnded { Pipe = typeof(RemoveCasePipe), Current = 2, }, events.Dequeue());

            Assert.IsFalse(events.Any());
        }

        [TestMethod]
        public void Loop()
        {
            var events = new Queue<PipelineEvent>();

            _sortPipelines["bubble_sort"].RunDetailed(_sortInput, e => events.Enqueue(e));

            _areEqual(new PipeStarted { Pipe = typeof(BubbleSortPipe), Current = 1, }, events.Dequeue());
            _areEqual(new PipeEnded { Pipe = typeof(BubbleSortPipe), Current = 1, }, events.Dequeue());

            _areEqual(new PipeStarted { Pipe = typeof(BubbleSortPipe), Current = 2, }, events.Dequeue());
            _areEqual(new PipeEnded { Pipe = typeof(BubbleSortPipe), Current = 2, }, events.Dequeue());

            _areEqual(new PipeStarted { Pipe = typeof(BubbleSortPipe), Current = 3, }, events.Dequeue());
            _areEqual(new PipeEnded { Pipe = typeof(BubbleSortPipe), Current = 3, }, events.Dequeue());

            _areEqual(new PipeStarted { Pipe = typeof(BubbleSortPipe), Current = 4, }, events.Dequeue());
            _areEqual(new PipeEnded { Pipe = typeof(BubbleSortPipe), Current = 4, }, events.Dequeue());

            _areEqual(new PipeStarted { Pipe = typeof(BubbleSortPipe), Current = 5, }, events.Dequeue());
            _areEqual(new PipeEnded { Pipe = typeof(BubbleSortPipe), Current = 5, }, events.Dequeue());

            Assert.IsFalse(events.Any());
        }

        [TestMethod]
        public void Branch()
        {

        }
    }
}
