using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TARAA
{
  class OneOnAtAnyTimeTimer : ActivityRecorder
  {
    public OneOnAtAnyTimeTimer( System.Windows.Forms.Button keyButton,
                                System.Windows.Forms.TextBox activityName,
                                System.Windows.Forms.Label countLabel,
                                System.Windows.Forms.Label durationLabel,
                                System.Diagnostics.Stopwatch runTimer,
                                List<OneOnAtAnyTimeTimer> oneOnAtAnyTimeTimers )
      : base( keyButton,
              activityName,
              durationLabel,
              countLabel,
              runTimer )
    {
      this.oneOnAtAnyTimeTimers = oneOnAtAnyTimeTimers;
    }

    public override void RespondToKeyDown()
    {
      this.TurnOn();
      foreach ( OneOnAtAnyTimeTimer oneOnAtAnyTimeTimer in oneOnAtAnyTimeTimers )
      {
        if ( oneOnAtAnyTimeTimer != this )
        {
          oneOnAtAnyTimeTimer.TurnOff();
        }
      }
    }

    public override void RespondToKeyUp()
    {
      // This does nothing.
    }

    protected List<OneOnAtAnyTimeTimer> oneOnAtAnyTimeTimers;
  }
}
