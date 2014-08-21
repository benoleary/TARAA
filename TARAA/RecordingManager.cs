using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TARAA
{
  class RecordingManager : ModeManager
  {
    public RecordingManager( ModeManager previousManager,
                             Button saveSettingsButton,
                             string recordsFile,
                             Label totalTimerLabel,
                             ProgressBar durationProgressBar,
                             Keys startRecordingKey,
                             System.Diagnostics.Stopwatch activitiesTimer )
      : base( previousManager )
    {
      this.activitiesTimer = activitiesTimer;
      this.saveSettingsButton = saveSettingsButton;
      allowedDuration = 0.0;
      try
      {
        allowedDuration = Convert.ToDouble( totalTimerInput.Text );
      }
      catch
      {
        MessageBox.Show( "Unable to interpret \"" + totalTimerInput.Text
                         + "\" as a number of seconds." );
        allowedDuration = 0.0;
      }
      recordingActivities = false;
      readyToStartRecording = true;
      this.totalTimerLabel = totalTimerLabel;
      this.durationProgressBar = durationProgressBar;
      this.recordsFile = recordsFile;
      animalIndex = 1;
      
      using ( StreamWriter streamWriter = new StreamWriter( recordsFile,
                                                            false) )
      {
        // [index of recording] [date timestamp] [animal name (auto/optional)]
        // ... [activity info]... [additional comments]
        string lineToWrite =( "\"Index of recording\""
                              + ";\"Date timestamp\""
                              + ";\"Experimenter name\""
                              + ";\"Animal name\"" );
        foreach ( ActivityRecorder activityRecorder in activityRecorders )
        {
          activityRecorder.LockActivityName();
          if ( activityRecorder.ActivationKey != Keys.None )
          {
            lineToWrite += ( ";" + activityRecorder.StringForHeader() );
          }
        }
        lineToWrite += ( ";\"Additional comments\"" );
        streamWriter.WriteLine( lineToWrite );
      }
      this.startRecordingKey = startRecordingKey;
      ResetRecords();
    }

    private Button saveSettingsButton;
    private double allowedDuration;
    private bool recordingActivities;
    private bool readyToStartRecording;
    private Label totalTimerLabel;
    private ProgressBar durationProgressBar;
    private string recordsFile;
    private int animalIndex;
    private Keys startRecordingKey;
    private System.Diagnostics.Stopwatch activitiesTimer;

    public override void RespondToUtilityButtonClick(
      UtilityButtons clickedUtility )
    {
      // The utility buttons should only do something if not recording.
      // Also, saving/discarding the record should only be active after a
      // timing/counting run (when readyToStartRecording is false).
      if ( recordingActivities )
      {
        return;
      }
      switch ( clickedUtility )
      {
        case UtilityButtons.startRecording:
          {
            TimeActivities();
          }
          break;
        case UtilityButtons.nextAnimal:
          {
            if ( !readyToStartRecording )
            {
              RecordLastAnimal();
              ResetRecords();
            }
          }
          break;
        case UtilityButtons.discardAnimal:
          {
            if ( !readyToStartRecording )
            {
              DialogResult dialogResult
                = MessageBox.Show( ( "Are you sure that you want to discard"
                                     + " this recording?" ),
                                  "Confirm that recording should be discarded",
                                   MessageBoxButtons.YesNo );
              if ( dialogResult == DialogResult.Yes )
              {
                ResetRecords();
              }
            }
          }
          break;
        default:
          {
          }
          break;
      }
    }

    public override void RespondToKeyDown( Keys keyDown )
    {
      if ( recordingActivities )
      {
        passKeyDownToActivityRecorder( keyDown );
      }
      else if ( readyToStartRecording )
      {
        if ( anyStartKeyRadioButton.Checked )
        {
          if ( passKeyDownToActivityRecorder( keyDown ) )
          {
            TimeActivities();
          }
        }
        else if ( keyDown == startRecordingKey )
        {
          TimeActivities();
        }
      }
    }

    public override void RespondToKeyUp( Keys keyUp )
    {
      if ( recordingActivities )
      {
        passKeyUpToActivityRecorder( keyUp );
      }
    }

    private bool passKeyDownToActivityRecorder( Keys keyDown )
    {
      ActivityRecorder activityRecorderWithKey;
      bool returnValue
        = ActivityRecorder.keyToRecorderMap.TryGetValue( keyDown,
                                                 out activityRecorderWithKey );
      if ( returnValue )
      {
        activityRecorderWithKey.RespondToKeyDown();
      }
      return returnValue;
    }

    private bool passKeyUpToActivityRecorder( Keys keyUp )
    {
      ActivityRecorder activityRecorderWithKey;
      bool returnValue
        = ActivityRecorder.keyToRecorderMap.TryGetValue( keyUp,
                                                 out activityRecorderWithKey );
      if ( returnValue )
      {
        activityRecorderWithKey.RespondToKeyUp();
      }
      return returnValue;
    }

    private void TimeActivities()
    {
      pauseButton.Enabled = true;
      saveSettingsButton.Enabled = false;
      leaveSetupOrStartRecordingButton.Text
        = "Currently timing/counting\nor saving/discarding record.";
      leaveSetupOrStartRecordingButton.Enabled = false;
      readyToStartRecording = false;
      recordingActivities = true;
      activitiesTimer.Reset();
      activitiesTimer.Start();
      while ( activitiesTimer.Elapsed.TotalSeconds <= allowedDuration )
      {
        totalTimerLabel.Text
          = activitiesTimer.Elapsed.TotalSeconds.ToString( "F" );
        durationProgressBar.Value
          = (int)( ( 100.0 * activitiesTimer.Elapsed.TotalSeconds )
                   / allowedDuration );
        foreach ( ActivityRecorder activityRecorder in activityRecorders )
        {
          activityRecorder.UpdateTime();
        }
        Application.DoEvents();
      }
      activitiesTimer.Stop();
      foreach ( ActivityRecorder activityRecorder in activityRecorders )
      {
        activityRecorder.UpdateTime();
        activityRecorder.TurnOff();
      }
      recordingActivities = false;
      pauseButton.Enabled = false;
      animalName.Enabled = true;
      animalName.Text = ( "Animal " + animalIndex.ToString( "D2" ) );
      additionalComments.Enabled = true;
      additionalComments.Text = "No additional comments";
      saveRecordButton.Enabled = true;
      discardRecordButton.Enabled = true;
      saveSettingsButton.Enabled = true;
    }

    private void ResetRecords()
    {
      activitiesTimer.Reset();
      foreach ( ActivityRecorder activityRecorder in activityRecorders )
      {
        activityRecorder.ResetRecords();
      }
      durationProgressBar.Value = 0;
      totalTimerLabel.Text = (0).ToString("F");
      recordingActivities = false;
      readyToStartRecording = true;
      DisableTextboxes();
      saveRecordButton.Enabled = false;
      discardRecordButton.Enabled = false;
      leaveSetupOrStartRecordingButton.Enabled
        = !(anyStartKeyRadioButton.Checked);
      if ( leaveSetupOrStartRecordingButton.Enabled )
      {
        leaveSetupOrStartRecordingButton.Text
          = ( "In timing/counting mode.\nClick here or press "
              + startRecordingKey.ToString() + " to start the timer." );
      }
      else
      {
        leaveSetupOrStartRecordingButton.Text
          = ( "This button has been disabled.\n"
              + "Press any activity key to start timing/counting." );
      }
    }

    private void RecordLastAnimal()
    {
      using ( StreamWriter streamWriter = new StreamWriter( recordsFile,
                                                            true ) )
      {
        // [index of recording] [date timestamp] [animal name (auto/optional)]
        // ... [activity info]... [additional comments]
        string lineToWrite = ( "\"" + animalIndex.ToString() + "\""
                              + ";\"" + DateTime.Now + "\""
                              + ";\"" + experimenterName.Text + "\""
                              + ";\"" + animalName.Text + "\"" );
        foreach ( ActivityRecorder activityRecorder in activityRecorders )
        {
          if ( activityRecorder.ActivationKey != Keys.None )
          {
            lineToWrite += ( ";" + activityRecorder.StringForDataLine() );
          }
        }
        lineToWrite += ( ";\"" + additionalComments.Text + "\"" );
        streamWriter.WriteLine( lineToWrite );
      }
      ++animalIndex;
    }
  }
}
