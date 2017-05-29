namespace PipeliningLibrary.UnitTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class BubbleSortPipe : IPipe
    {
        static void Foo()
        {
            var grupo = new PipelineGroup();
        }

        public object Run(dynamic input)
        {
            IEnumerable collection = input;

            var comparer = Comparer.Default;
            var values = collection.Cast<object>().ToArray();

            Action<IList<object>, int, int> swap = (list, i, j) =>
            {
                var aux = list[i];
                list[i] = list[j];
                list[j] = aux;
            };

            var swapped = false;
            for (int i = 0, j = 1; i < (values.Length - 1); ++i, ++j)
            {
                if (comparer.Compare(values[i], values[j]) > 0)
                {
                    swap(values, i, j);
                    swapped = true;
                }
            }

            if (swapped)
                return values;
            else
                return new PipelineEnd(values);
        }
    }
}
