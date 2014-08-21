using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TARAA
{
  abstract class ModeManager
  {
    public enum UtilityButtons
    {
      startRecording,
      nextAnimal,
      discardAnimal,
      loadSettings,
      saveSettings,
      clearSettings
    }

    public ModeManager( List<ActivityRecorder> activityRecorders,
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
    {
      this.activityRecorders = activityRecorders;
      this.leaveSetupOrStartRecordingButton = leaveSetupOrStartRecordingButton;
      this.saveRecordButton = saveRecordButton;
      this.discardRecordButton = discardRecordButton;
      this.pauseButton = pauseButton;
      this.totalTimerInput = totalTimerInput;
      this.experimenterName = experimenterName;
      this.animalName = animalName;
      this.additionalComments = additionalComments;
      this.specificStartKeyRadioButton = specificStartKeyRadioButton;
      this.anyStartKeyRadioButton = anyStartKeyRadioButton;
    }

    public ModeManager( ModeManager copySource )
    {
      this.activityRecorders = copySource.activityRecorders;
      this.leaveSetupOrStartRecordingButton
        = copySource.leaveSetupOrStartRecordingButton;
      this.saveRecordButton = copySource.saveRecordButton;
      this.discardRecordButton = copySource.discardRecordButton;
      this.pauseButton = copySource.pauseButton;
      this.totalTimerInput = copySource.totalTimerInput;
      this.experimenterName = copySource.experimenterName;
      this.animalName = copySource.animalName;
      this.additionalComments = copySource.additionalComments;
      this.specificStartKeyRadioButton
        = copySource.specificStartKeyRadioButton;
      this.anyStartKeyRadioButton = copySource.anyStartKeyRadioButton;
    }
    
    protected List<ActivityRecorder> activityRecorders;
    protected Button leaveSetupOrStartRecordingButton;
    protected Button saveRecordButton;
    protected Button discardRecordButton;
    protected Button pauseButton;
    protected TextBox totalTimerInput;
    protected TextBox experimenterName;
    protected TextBox animalName;
    protected RichTextBox additionalComments;
    protected RadioButton specificStartKeyRadioButton;
    protected RadioButton anyStartKeyRadioButton;

    public abstract void RespondToKeyDown( Keys keyDown );

    public abstract void RespondToKeyUp( Keys keyUp );

    public virtual void RespondToActivityButtonClick(
      ActivityRecorder clickedActivity )
    {
      // This does nothing by default.
    }

    public virtual void RespondToUtilityButtonClick(
      UtilityButtons clickedUtility )
    {
      // This does nothing by default.
    }

    public virtual void SaveSettingsToFile( string settingsFilename )
    {
      // This does nothing by default.
    }

    public virtual void LoadSettingsFromFile( string settingsFilename )
    {
      // This does nothing by default.
    }

    protected void DisableTextboxes()
    {
      animalName.Text = "[Disabled temporarily]";
      animalName.Enabled = false;
      additionalComments.Text = "[Disabled until end of timing/counting]";
      additionalComments.Enabled = false;
    }

  }
}
