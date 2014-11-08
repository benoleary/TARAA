using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TARAA
{
  abstract class ActivityRecorder
  {
    public static
      Dictionary<System.Windows.Forms.Keys, ActivityRecorder> keyToRecorderMap
      = new Dictionary<System.Windows.Forms.Keys, ActivityRecorder>();
    public static string defaultKey = "None";
    public static string defaultDescription = "No activity";

    public ActivityRecorder( System.Windows.Forms.Button keyButton,
                             System.Windows.Forms.TextBox activityName,
                             System.Windows.Forms.Label durationLabel,
                             System.Windows.Forms.Label countLabel,
                             System.Diagnostics.Stopwatch runTimer )
    {
      activationKey = System.Windows.Forms.Keys.None;
      this.keyButton = keyButton;
      this.activityName = activityName;
      this.durationLabel = durationLabel;
      this.durationLabel.Text = ( 0 ).ToString( "F" );
      this.countLabel = countLabel;
      this.countLabel.Text = ( 0 ).ToString();
      this.runTimer = runTimer;
      this.onAndOffTimes = new List<Tuple<double, double>>();
      this.currentlyOn = false;
      this.lastTurnOnTime = 0.0;
      this.totalOnTime = 0.0;
    }

    public System.Windows.Forms.Keys ActivationKey
    {
      get { return activationKey; }
      set
      {
        keyToRecorderMap.Remove( activationKey );
        keyToRecorderMap[ value ] = this;
        activationKey = value;
        keyButton.Text = value.ToString();
      }
    }

    public string ActivityName
    {
      get { return activityName.Text; }
      set { activityName.Text = value; }
    }

    public abstract void RespondToKeyDown();
    public abstract void RespondToKeyUp();

    public void UpdateTime()
    {
      if ( currentlyOn )
      {
        durationLabel.Text = ( runTimer.Elapsed.TotalSeconds
                               - lastTurnOnTime
                               + totalOnTime ).ToString( "F" );
      }
    }

    public void TurnOn()
    {
      if ( !currentlyOn )
      {
        currentlyOn = true;
        lastTurnOnTime = runTimer.Elapsed.TotalSeconds;
        countLabel.Text = ( onAndOffTimes.Count() + 1 ).ToString();
      }
    }

    public void TurnOff()
    {
      if ( currentlyOn )
      {
        currentlyOn = false;
        onAndOffTimes.Add( new Tuple<double, double>( lastTurnOnTime,
                                             runTimer.Elapsed.TotalSeconds ) );
        totalOnTime += ( runTimer.Elapsed.TotalSeconds - lastTurnOnTime );
      }
    }

    public void ResetRecords()
    {
      durationLabel.Text = ( 0 ).ToString( "F" );
      countLabel.Text = ( 0 ).ToString();
      onAndOffTimes.Clear();
      currentlyOn = false;
      lastTurnOnTime = 0.0;
      totalOnTime = 0.0;
    }

    public string StringForDataLine( int numberOfIntervals,
                                     double intervalLength )
    {
      StringBuilder lineBuilder
      = new StringBuilder( "\"" + durationLabel.Text + "\";\""
                                + countLabel.Text + "\"" );
      if ( numberOfIntervals > 1 )
      {
        double[] intervalDurations = new double[ numberOfIntervals ];
        int[] intervalCounts = new int[ numberOfIntervals ];
        for ( int intervalIndex = 0;
              intervalIndex < numberOfIntervals;
              ++intervalIndex )
        {
          intervalDurations[ intervalIndex ] = 0.0;
          intervalCounts[ intervalIndex ] = 0;
        }

        foreach ( var onAndOffTime in onAndOffTimes )
        {
          int startIndex = (int)( onAndOffTime.Item1 / intervalLength );
          double endTime = onAndOffTime.Item2;
          int endIndex = (int)( endTime / intervalLength );
          if ( endIndex >= numberOfIntervals )
          {
            endIndex = ( numberOfIntervals - 1 );
            endTime = ( numberOfIntervals * intervalLength );
          }

          // For counting purposes, it happens in the interval when it starts
          // (so corresponding to key presses rather than releases).
          intervalCounts[ startIndex ] += 1;

          // If startIndex is greater than endIndex, something screwed up!
          if ( startIndex == endIndex )
          {
            // If the activity was entirely in a single interval, we add its
            // full duration to that interval.
            intervalDurations[ startIndex ]
            += ( endTime - onAndOffTime.Item1 );
          }
          else
          {
            // If the activity was spread over multiple intervals, we
            // effectively break the activity into multiple segments:
            // a segment from the start time until the time of the end of the
            // starting interval,
            // 0 or more segments corresponding to entire intervals,
            // and a segment from the time of the ending interval to the
            // activity end time.
            intervalDurations[ startIndex ]
            += ( ( intervalLength * ( startIndex + 1 ) )
                 - onAndOffTime.Item1 );
            for ( int intervalIndex = ( startIndex + 1 );
                  intervalIndex < endIndex;
                  ++intervalIndex )
            {
              intervalDurations[ intervalIndex ] += intervalLength;
            }
            intervalDurations[ endIndex ]
            += ( endTime - ( intervalLength * endIndex ) );
          }
        }

        for ( int intervalIndex = 0;
              intervalIndex < numberOfIntervals;
              ++intervalIndex )
        {
          lineBuilder.Append( ";\""
                     + intervalDurations[ intervalIndex ].ToString()  + "\";\""
                     + intervalCounts[ intervalIndex ].ToString() + "\"" );
        }
      }
      return lineBuilder.ToString();
    }

    public string StringForHeader( int numberOfIntervals )
    {
      StringBuilder headerBuilder
      = new StringBuilder(
                  "\"" + activityName.Text + ": total duration in seconds\";\""
                       + activityName.Text + ": total count\"" );
      if ( numberOfIntervals > 1 )
      {
        for ( int intervalIndex = 1;
              intervalIndex <= numberOfIntervals;
              ++intervalIndex )
        {
          headerBuilder.Append( ";\""
             + activityName.Text + " interval " + intervalIndex.ToString()
             + ": interval duration in seconds\";\""
             + activityName.Text + " interval " + intervalIndex.ToString()
             + ": interval count\"" );
			  }
      }
      return headerBuilder.ToString();
    }

    public void ResetKeyAndDescription()
    {
      activationKey = System.Windows.Forms.Keys.None;
      keyButton.Text = defaultKey;
      activityName.Text = defaultDescription;
    }

    public void LockActivityName()
    {
      activityName.Enabled = false;
    }

    protected System.Windows.Forms.Keys activationKey;
    protected System.Windows.Forms.Button keyButton;
    protected System.Windows.Forms.TextBox activityName;
    protected System.Windows.Forms.Label durationLabel;
    protected System.Windows.Forms.Label countLabel;
    protected System.Diagnostics.Stopwatch runTimer;
    protected List<Tuple<double, double>> onAndOffTimes;
    protected bool currentlyOn;
    protected double lastTurnOnTime;
    protected double totalOnTime;
  }
}
