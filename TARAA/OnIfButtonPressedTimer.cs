using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TARAA
{
  class OnIfButtonPressedTimer : ActivityRecorder
  {
    public OnIfButtonPressedTimer( System.Windows.Forms.Button keyButton,
                                   System.Windows.Forms.TextBox activityName,
                                   System.Windows.Forms.Label countLabel,
                                   System.Windows.Forms.Label durationLabel,
                                   System.Diagnostics.Stopwatch runTimer )
      : base( keyButton,
              activityName,
              durationLabel,
              countLabel,
              runTimer )
    {
      // This just initializes the base class.
    }

    public override void RespondToKeyDown()
    {
      TurnOn();
    }

    public override void RespondToKeyUp()
    {
      TurnOff();
    }
  }
}
