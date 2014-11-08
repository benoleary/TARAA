using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TARAA
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      // TODO:
      // 1) Fix focus problem - DONE!
      // 2) Fix cancel on choose file - DONE!
      // 3) Add time intervals - DONE!
      // 4) Allow installation folder to be chosen
      // 1) & 2) can probably be solved together with the
      // "in setup mode / in timing mode" button initially being a
      // "select file" button.
      InitializeComponent();
      numberOfIntervals = Convert.ToInt32( numberOfIntervalsInput.Value );
      intervalDuration = ModeManager.IntervalDuration( intervalSecondsInput );

      activitiesTimer = new System.Diagnostics.Stopwatch();
      isPaused = false;
      // pauseButton.Enabled = false;

      // I don't have a better way of linking all the buttons, text boxes, &
      // labels to the recorders.
      activityRecorders = new List<ActivityRecorder>();

      oneOnAtAnyTimeTimers = new List<OneOnAtAnyTimeTimer>();
      OneOnAtAnyTimeTimer oneOnAtAnyTimeTimer
        = new OneOnAtAnyTimeTimer( leftButtonOne,
                                   leftDescriptionOne,
                                   leftCountOne,
                                   leftDurationOne,
                                   activitiesTimer,
                                   oneOnAtAnyTimeTimers );
      oneOnAtAnyTimeTimers.Add( oneOnAtAnyTimeTimer );
      activityRecorders.Add( oneOnAtAnyTimeTimer );
      oneOnAtAnyTimeTimer = new OneOnAtAnyTimeTimer( leftButtonTwo,
                                                     leftDescriptionTwo,
                                                     leftCountTwo,
                                                     leftDurationTwo,
                                                     activitiesTimer,
                                                     oneOnAtAnyTimeTimers );
      oneOnAtAnyTimeTimers.Add( oneOnAtAnyTimeTimer );
      activityRecorders.Add( oneOnAtAnyTimeTimer );
      oneOnAtAnyTimeTimer = new OneOnAtAnyTimeTimer( leftButtonThree,
                                                     leftDescriptionThree,
                                                     leftCountThree,
                                                     leftDurationThree,
                                                     activitiesTimer,
                                                     oneOnAtAnyTimeTimers );
      oneOnAtAnyTimeTimers.Add( oneOnAtAnyTimeTimer );
      activityRecorders.Add( oneOnAtAnyTimeTimer );
      oneOnAtAnyTimeTimer = new OneOnAtAnyTimeTimer( leftButtonFour,
                                                     leftDescriptionFour,
                                                     leftCountFour,
                                                     leftDurationFour,
                                                     activitiesTimer,
                                                     oneOnAtAnyTimeTimers );
      oneOnAtAnyTimeTimers.Add( oneOnAtAnyTimeTimer );
      activityRecorders.Add( oneOnAtAnyTimeTimer );
      oneOnAtAnyTimeTimer = new OneOnAtAnyTimeTimer( leftButtonFive,
                                                     leftDescriptionFive,
                                                     leftCountFive,
                                                     leftDurationFive,
                                                     activitiesTimer,
                                                     oneOnAtAnyTimeTimers );
      oneOnAtAnyTimeTimers.Add( oneOnAtAnyTimeTimer );
      activityRecorders.Add( oneOnAtAnyTimeTimer );
      oneOnAtAnyTimeTimer = new OneOnAtAnyTimeTimer( leftButtonSix,
                                                     leftDescriptionSix,
                                                     leftCountSix,
                                                     leftDurationSix,
                                                     activitiesTimer,
                                                     oneOnAtAnyTimeTimers );
      oneOnAtAnyTimeTimers.Add( oneOnAtAnyTimeTimer );
      activityRecorders.Add( oneOnAtAnyTimeTimer );
      oneOnAtAnyTimeTimer = new OneOnAtAnyTimeTimer( leftButtonSeven,
                                                     leftDescriptionSeven,
                                                     leftCountSeven,
                                                     leftDurationSeven,
                                                     activitiesTimer,
                                                     oneOnAtAnyTimeTimers );
      oneOnAtAnyTimeTimers.Add( oneOnAtAnyTimeTimer );
      activityRecorders.Add( oneOnAtAnyTimeTimer );
      oneOnAtAnyTimeTimer = new OneOnAtAnyTimeTimer( leftButtonEight,
                                                     leftDescriptionEight,
                                                     leftCountEight,
                                                     leftDurationEight,
                                                     activitiesTimer,
                                                     oneOnAtAnyTimeTimers );
      oneOnAtAnyTimeTimers.Add( oneOnAtAnyTimeTimer );
      activityRecorders.Add( oneOnAtAnyTimeTimer );

      onIfButtonPressedTimers = new List<OnIfButtonPressedTimer>();
      OnIfButtonPressedTimer onIfButtonPressedTimer
        = new OnIfButtonPressedTimer( rightButtonOne,
                                      rightDescriptionOne,
                                      rightCountOne,
                                      rightDurationOne,
                                      activitiesTimer );
      onIfButtonPressedTimers.Add( onIfButtonPressedTimer );
      activityRecorders.Add( onIfButtonPressedTimer );
      onIfButtonPressedTimer = new OnIfButtonPressedTimer( rightButtonTwo,
                                                           rightDescriptionTwo,
                                                           rightCountTwo,
                                                           rightDurationTwo,
                                                           activitiesTimer );
      onIfButtonPressedTimers.Add( onIfButtonPressedTimer );
      activityRecorders.Add( onIfButtonPressedTimer );
      onIfButtonPressedTimer = new OnIfButtonPressedTimer( rightButtonThree,
                                                         rightDescriptionThree,
                                                           rightCountThree,
                                                           rightDurationThree,
                                                           activitiesTimer );
      onIfButtonPressedTimers.Add( onIfButtonPressedTimer );
      activityRecorders.Add( onIfButtonPressedTimer );
      onIfButtonPressedTimer = new OnIfButtonPressedTimer( rightButtonFour,
                                                           rightDescriptionFour,
                                                           rightCountFour,
                                                           rightDurationFour,
                                                           activitiesTimer );
      onIfButtonPressedTimers.Add( onIfButtonPressedTimer );
      activityRecorders.Add( onIfButtonPressedTimer );
      onIfButtonPressedTimer = new OnIfButtonPressedTimer( rightButtonFive,
                                                          rightDescriptionFive,
                                                           rightCountFive,
                                                           rightDurationFive,
                                                           activitiesTimer );
      onIfButtonPressedTimers.Add( onIfButtonPressedTimer );
      activityRecorders.Add( onIfButtonPressedTimer );
      onIfButtonPressedTimer = new OnIfButtonPressedTimer( rightButtonSix,
                                                           rightDescriptionSix,
                                                           rightCountSix,
                                                           rightDurationSix,
                                                           activitiesTimer );
      onIfButtonPressedTimers.Add( onIfButtonPressedTimer );
      activityRecorders.Add( onIfButtonPressedTimer );
      onIfButtonPressedTimer = new OnIfButtonPressedTimer( rightButtonSeven,
                                                         rightDescriptionSeven,
                                                           rightCountSeven,
                                                           rightDurationSeven,
                                                           activitiesTimer );
      onIfButtonPressedTimers.Add( onIfButtonPressedTimer );
      activityRecorders.Add( onIfButtonPressedTimer );
      onIfButtonPressedTimer = new OnIfButtonPressedTimer( rightButtonEight,
                                                         rightDescriptionEight,
                                                           rightCountEight,
                                                           rightDurationEight,
                                                           activitiesTimer );
      onIfButtonPressedTimers.Add( onIfButtonPressedTimer );
      activityRecorders.Add( onIfButtonPressedTimer );

      // The event handler for a key press has to be initialized.
      KeyPreview = true;
      KeyDown += new KeyEventHandler( MainForm_KeyDown );
      KeyUp += new KeyEventHandler( MainForm_KeyUp );

      startRecordingKey = Keys.Space;
      specificStartKeyRadioButton.Text = ( "Start recording only with "
                                           + startRecordingKey.ToString()
                                      + " (starts 1st activity in list too)" );
      recordsFileLabel.Text = ( "No record file chosen!" );
      leaveSetupOrStartRecordingButton.Text
        = ( "Click here to choose a file\nto record the data." );
      recordsFile = "none";

      // The form begins in setup mode.
      inSetupMode = true;
      setupManager = new SetupManager( activityRecorders,
                                       leaveSetupOrStartRecordingButton,
                                       nextAnimalButton,
                                       discardAnimalButton,
        // pauseButton,
                                       numberOfIntervalsInput,
                                       intervalSecondsInput,
                                       experimenterNameTextBox,
                                       animalNameTextBox,
                                       additionalCommentsRichTextBox,
                                       specificStartKeyRadioButton,
                                       anyStartKeyRadioButton );
      modeManager = setupManager;
      if ( System.IO.File.Exists( SetupManager.lastSettingsFilename ) )
      {
        modeManager.LoadSettingsFromFile( SetupManager.lastSettingsFilename );
      }
    }

    private System.Diagnostics.Stopwatch activitiesTimer;
    private ModeManager modeManager;
    private SetupManager setupManager;
    private List<ActivityRecorder> activityRecorders;
    private List<OneOnAtAnyTimeTimer> oneOnAtAnyTimeTimers;
    private List<OnIfButtonPressedTimer> onIfButtonPressedTimers;
    private string recordsFile;
    private bool inSetupMode;
    private bool isPaused;
    private Keys startRecordingKey;
    private int numberOfIntervals;
    private double intervalDuration;

    private void MainForm_KeyDown( object sender, KeyEventArgs keyEventArgs )
    {
      if ( !isPaused )
      {
        modeManager.RespondToKeyDown( keyEventArgs.KeyCode );
      }
      keyEventArgs.Handled = true;
    }

    private void MainForm_KeyUp( object sender, KeyEventArgs keyEventArgs )
    {
      if ( !isPaused )
      {
        modeManager.RespondToKeyUp( keyEventArgs.KeyCode );
      }
      keyEventArgs.Handled = true;
    }

    private void LeftButtonClick( int whichButton )
    {
      modeManager.RespondToActivityButtonClick(
                                               oneOnAtAnyTimeTimers[ whichButton ] );
    }

    private void RightButtonClick( int whichButton )
    {
      modeManager.RespondToActivityButtonClick(
                                            onIfButtonPressedTimers[ whichButton ] );
    }

    private void leaveSetupOrStartRecordingButton_Click( object sender,
                                                         EventArgs e )
    {
      if ( recordsFile == "none" )
      {
        DialogResult dialogResult = saveRecordFileDialog.ShowDialog();
        if ( dialogResult == DialogResult.OK )
        {
          recordsFile = saveRecordFileDialog.FileName;
          recordsFileLabel.Text = ( "Recording to \"" + recordsFile + "\"" );
          leaveSetupOrStartRecordingButton.Text
            = ( " In setup mode.\nClick to enter timing/counting mode." );
        }
      }
      else if ( inSetupMode )
      {
        intervalSecondsInput.Enabled = false;
        experimenterNameTextBox.Enabled = false;
        leaveSetupOrStartRecordingButton.Text
          = ( "In timing/counting mode.\n"
              + "Click here or press " + startRecordingKey.ToString()
              + " to start the timer." );
        loadSettingsButton.Enabled = false;
        clearSettingsButton.Enabled = false;
        if ( anyStartKeyRadioButton.Checked )
        {
          leaveSetupOrStartRecordingButton.Text
            = ( "This button has been disabled.\n"
                + "Press any activity key to start timing/counting." );
          leaveSetupOrStartRecordingButton.Enabled = false;
        }
        specificStartKeyRadioButton.Enabled = false;
        anyStartKeyRadioButton.Enabled = false;
        modeManager = new RecordingManager( modeManager,
                                            saveSettingsButton,
                                            recordsFile,
                                            totalTimerLabel,
                                            durationProgressBar,
                                            startRecordingKey,
                                            activitiesTimer );
        inSetupMode = false;
        setupManager.SaveSettingsToFile( SetupManager.lastSettingsFilename );
      }
      else
      {
        modeManager.RespondToKeyDown( startRecordingKey );
      }
    }

    private void leftButtonOne_Click( object sender, EventArgs e )
    {
      LeftButtonClick( 0 );
    }

    private void leftButtonTwo_Click( object sender, EventArgs e )
    {
      LeftButtonClick( 1 );
    }

    private void leftButtonThree_Click( object sender, EventArgs e )
    {
      LeftButtonClick( 2 );
    }

    private void leftButtonFour_Click( object sender, EventArgs e )
    {
      LeftButtonClick( 3 );
    }

    private void leftButtonFive_Click( object sender, EventArgs e )
    {
      LeftButtonClick( 4 );
    }

    private void leftButtonSix_Click( object sender, EventArgs e )
    {
      LeftButtonClick( 5 );
    }

    private void leftButtonSeven_Click( object sender, EventArgs e )
    {
      LeftButtonClick( 6 );
    }

    private void leftButtonEight_Click( object sender, EventArgs e )
    {
      LeftButtonClick( 7 );
    }

    private void rightButtonOne_Click( object sender, EventArgs e )
    {
      RightButtonClick( 0 );
    }

    private void rightButtonTwo_Click( object sender, EventArgs e )
    {
      RightButtonClick( 1 );
    }

    private void rightButtonThree_Click( object sender, EventArgs e )
    {
      RightButtonClick( 2 );
    }

    private void rightButtonFour_Click( object sender, EventArgs e )
    {
      RightButtonClick( 3 );
    }

    private void rightButtonFive_Click( object sender, EventArgs e )
    {
      RightButtonClick( 4 );
    }

    private void rightButtonSix_Click( object sender, EventArgs e )
    {
      RightButtonClick( 5 );
    }

    private void rightButtonSeven_Click( object sender, EventArgs e )
    {
      RightButtonClick( 6 );
    }

    private void rightButtonEight_Click( object sender, EventArgs e )
    {
      RightButtonClick( 7 );
    }

    private void loadSettingsButton_Click( object sender, EventArgs e )
    {
      DialogResult dialogResult = loadSettingsFileDialog.ShowDialog();
      if ( dialogResult == DialogResult.OK )
      {
        modeManager.LoadSettingsFromFile( loadSettingsFileDialog.FileName );
      }
    }

    private void saveSettingsButton_Click( object sender, EventArgs e )
    {
      DialogResult dialogResult = saveSettingsFileDialog.ShowDialog();
      if ( dialogResult == DialogResult.OK )
      {
        setupManager.SaveSettingsToFile( saveSettingsFileDialog.FileName );
      }
    }

    private void clearSettingsButton_Click( object sender, EventArgs e )
    {
      DialogResult dialogResult
        = MessageBox.Show( ( "Are you sure that you want to clear all the"
                             + " settings?" ),
                           "Confirm that settings should be cleared",
                           MessageBoxButtons.YesNo );
      if ( dialogResult == DialogResult.Yes )
      {
        setupManager.ClearSettings();
      }
    }

    private void nextAnimalButton_Click( object sender, EventArgs e )
    {
      modeManager.RespondToUtilityButtonClick(
                                       ModeManager.UtilityButtons.nextAnimal );
    }

    private void discardAnimalButton_Click( object sender, EventArgs e )
    {
      modeManager.RespondToUtilityButtonClick(
                                    ModeManager.UtilityButtons.discardAnimal );
    }

    private void specificStartKeyRadioButton_CheckedChanged( object sender,
                                                             EventArgs e )
    {
      anyStartKeyRadioButton.Checked
        = !( specificStartKeyRadioButton.Checked );
    }

    private void anyStartKeyRadioButton_CheckedChanged( object sender,
                                                        EventArgs e )
    {
      specificStartKeyRadioButton.Checked
        = !( anyStartKeyRadioButton.Checked );
    }

    //private void pauseButton_Click( object sender, EventArgs e )
    //{
    //  if ( isPaused )
    //  {
    //    isPaused = false;
    //    activitiesTimer.Start();
    //    pauseButton.Text = "Click to pause";
    //    discardAnimalButton.Enabled = false;
    //  }
    //  else
    //  {
    //    isPaused = true;
    //    activitiesTimer.Stop();
    //    pauseButton.Text = "Click to unpause";
    //    discardAnimalButton.Enabled = true;
    //  }
    //}

    private void numberOfIntervalsInput_ValueChanged( object sender,
                                                      EventArgs e )
    {
      if ( numberOfIntervalsInput.Value == 1 )
      {
        numberOfIntervalsLabel.Text = "interval, of duration";
      }
      else
      {
        numberOfIntervalsLabel.Text = "intervals, of duration";
      }
      numberOfIntervals = Convert.ToInt32( numberOfIntervalsInput.Value );
      totalDurationLabel.Text
      = ( numberOfIntervals * intervalDuration ).ToString();
    }

    private void intervalSecondsInput_TextChanged( object sender, EventArgs e )
    {
      intervalDuration = ModeManager.IntervalDuration( intervalSecondsInput );
      totalDurationLabel.Text
      = ( numberOfIntervals * intervalDuration ).ToString();
    }

  }
}
