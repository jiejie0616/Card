using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class HindlerBase
{
    public abstract void OnReceive(int subCode, object message);

    protected void Dispatch(int areaCode, int eventCode, object message)
    {
        MsgCenter.Instance.Dispatch(areaCode, eventCode, message);
    }
}
