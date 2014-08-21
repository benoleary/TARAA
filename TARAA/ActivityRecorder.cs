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

    public string StringForDataLine()
    {
      return ( "\"" + durationLabel.Text
               + "\";\"" + countLabel.Text + "\"" );
    }

    public string StringForHeader()
    {
      return ( "\"" + activityName.Text + ": total duration in seconds\";\""
               + activityName.Text + ": total count\"" );
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
