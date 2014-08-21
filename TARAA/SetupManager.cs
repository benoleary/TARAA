using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TARAA
{
  class SetupManager : ModeManager
  {
    public static string lastSettingsFilename
      = ( Directory.GetCurrentDirectory() + @"\TARAA_last_settings.csv" );

    public SetupManager( List<ActivityRecorder> activityRecorders,
                         Button leaveSetupOrStartRecordingButton,
                         Button saveRecordButton,
                         Button discardRecordButton,
                         Button pauseButton,
                         TextBox totalTimerInput,
                         TextBox experimenterName,
                         TextBox animalName,
                         RichTextBox additionalComments,
                         RadioButton specificStartKeyRadioButton,
                         RadioButton anyStartKeyRadioButton )
      : base( activityRecorders,
              leaveSetupOrStartRecordingButton,
              saveRecordButton,
              discardRecordButton,
              pauseButton,
              totalTimerInput,
              experimenterName,
              animalName,
              additionalComments,
              specificStartKeyRadioButton,
              anyStartKeyRadioButton )
    {
      activityRecorderAwaitingKey = null;
      DisableTextboxes();
      saveRecordButton.Enabled = false;
      discardRecordButton.Enabled = false;
    }

    private ActivityRecorder activityRecorderAwaitingKey;

    public override void RespondToKeyDown( Keys keyDown )
    {
      // In setup mode, we either let the user edit the activity description
      // TextBox fields, or we have to capture a key press.
      if ( activityRecorderAwaitingKey != null )
      {
        // If we are capturing a key press, we need to remove this key from
        // any ActivityRecorder that already had it.
        ActivityRecorder activityRecorderWithKey;
        if ( ActivityRecorder.keyToRecorderMap.TryGetValue( keyDown,
                                                out activityRecorderWithKey ) )
        {
          activityRecorderWithKey.ActivationKey = Keys.None;
        }

        // Finally we can assign the key to the waiting ActivityRecorder.
        // (The dictionary is updated by the ActivityRecorder's property.)
        activityRecorderAwaitingKey.ActivationKey = keyDown;

        // Now the key has been set, so there is no ActivityRecorder
        // awaiting a key.
        activityRecorderAwaitingKey = null;
      }
    }

    public override void RespondToKeyUp( Keys keyUp )
    {
      // This does nothing.
    }

    public override void RespondToActivityButtonClick(
                                             ActivityRecorder clickedActivity )
    {
      activityRecorderAwaitingKey = clickedActivity;
    }

    public override void SaveSettingsToFile( string settingsFilename )
    {
      using ( StreamWriter streamWriter = new StreamWriter( settingsFilename,
                                                            false ) )
      {
        for ( int whichIndex = 0;
              whichIndex < activityRecorders.Count;
              ++whichIndex )
        {
          streamWriter.WriteLine(
            activityRecorders[ whichIndex ].ActivationKey.ToString() );
          streamWriter.WriteLine(
            activityRecorders[ whichIndex ].ActivityName );
        }
        streamWriter.WriteLine( totalTimerInput.Text );
        streamWriter.WriteLine( experimenterName.Text );
        if ( anyStartKeyRadioButton.Checked )
        {
          streamWriter.WriteLine( "any" );
        }
        else
        {
          streamWriter.WriteLine( "F1" );
        }
      }
    }

    public void ClearSettings()
    {
      foreach ( ActivityRecorder activityRecorder in activityRecorders )
      {
        activityRecorder.ResetRecords();
        activityRecorder.ResetKeyAndDescription();
      }
      activityRecorderAwaitingKey = null;
      ActivityRecorder.keyToRecorderMap.Clear();
    }

    public override void LoadSettingsFromFile( string settingsFilename )
    {
      ClearSettings();

      using ( StreamReader streamReader
              = new StreamReader( settingsFilename ) )
      {
        string lineRead;
        KeysConverter keysConverter = new KeysConverter();
        int lineCount = 0;
        for ( int whichIndex = 0;
              whichIndex < activityRecorders.Count;
              ++whichIndex )
        {
          if ( streamReader.EndOfStream )
          {
            MessageBox.Show( "Uhoh. Settings file didn't have the right number"
                             + " of lines (read " + lineCount.ToString()
                             + "lines). Carrying on regardless." );
            return;
          }
          lineRead = streamReader.ReadLine();
          ++lineCount;
          if ( lineRead != "None" )
          {
            activityRecorders[ whichIndex ].ActivationKey
              = (Keys)keysConverter.ConvertFromString( lineRead );
          }
          if ( streamReader.EndOfStream )
          {
            MessageBox.Show( "Uhoh. Settings file didn't have the right number"
                             + " of lines (read " + lineCount.ToString()
                             + "lines). Carrying on regardless." );
            return;
          }
          lineRead = streamReader.ReadLine();
          ++lineCount;
          activityRecorders[ whichIndex ].ActivityName = lineRead;
        }
        if ( streamReader.EndOfStream )
        {
          MessageBox.Show( "Uhoh. Settings file didn't have the right number"
                           + " of lines (read " + lineCount.ToString()
                           + "lines). Carrying on regardless." );
          return;
        }
        lineRead = streamReader.ReadLine();
        ++lineCount;
        totalTimerInput.Text = lineRead;
        if ( streamReader.EndOfStream )
        {
          MessageBox.Show( "Uhoh. Settings file didn't have the right number"
                           + " of lines (read " + lineCount.ToString()
                           + "lines). Carrying on regardless." );
          return;
        }
        lineRead = streamReader.ReadLine();
        ++lineCount;
        experimenterName.Text = lineRead;
        if ( streamReader.EndOfStream )
        {
          MessageBox.Show( "Uhoh. Settings file didn't have the right number"
                           + " of lines (read " + lineCount.ToString()
                           + "lines). Carrying on regardless." );
          return;
        }
        lineRead = streamReader.ReadLine();
        ++lineCount;
        if ( lineRead == "any" )
        {
          specificStartKeyRadioButton.Checked = false;
          anyStartKeyRadioButton.Checked = true;
        }
        else
        {
          specificStartKeyRadioButton.Checked = true;
          anyStartKeyRadioButton.Checked = false;
        }
      }
    }

  }
}
