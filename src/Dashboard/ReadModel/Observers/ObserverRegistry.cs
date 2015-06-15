using System.Collections.Generic;
using Dashboard.Infrastructure;

namespace Dashboard.ReadModel.Observers
{
    public class ObserverRegistry
    {
        public IEnumerable<object> GetObservers(IProjectionWriterFactory factory)
        {
            //I'm sure this will be used. Leaving here for a reference.
            yield return null; //new SamplesObserver(factory.GetProjectionWriter<SampleView>());
        }
    }
}