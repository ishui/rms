namespace Rms.WorkFlow
{
    using System;
    using System.Data;

    public interface IActorIO
    {
        DataSet InputActor();
        void OutputActor(Actor actor);
    }
}

