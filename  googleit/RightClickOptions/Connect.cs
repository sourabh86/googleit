using System;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Windows.Forms;


namespace RightClickOptions
{
	/// <summary>The object for implementing an Add-in.</summary>
	/// <seealso class='IDTExtensibility2' />
	public class Connect : IDTExtensibility2,IDTCommandTarget
	{
		/// <summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
		public Connect()
		{
		}

		/// <summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
		/// <param term='application'>Root object of the host application.</param>
		/// <param term='connectMode'>Describes how the Add-in is being loaded.</param>
		/// <param term='addInInst'>Object representing this Add-in.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnConnection(object application, ext_ConnectMode connectMode, object addInInst, ref Array custom)
		{
			_applicationObject = (DTE2)application;
			_addInInstance = (AddIn)addInInst;
            
			
		}

		/// <summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
		/// <param term='disconnectMode'>Describes how the Add-in is being unloaded.</param>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnDisconnection(ext_DisconnectMode disconnectMode, ref Array custom)
		{
		}

		/// <summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification when the collection of Add-ins has changed.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />		
		public void OnAddInsUpdate(ref Array custom)
		{
		}

		/// <summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnStartupComplete(ref Array custom)
		{
            Command codeWindowCommand = null;
            CommandBarControl codeWindowButton;
            CommandBar codeCommandBar;
            CommandBars commandBars;
            try
            {
                //try
                //{
                //    codeWindowCommand = _applicationObject.Commands.Item(_addInInstance.ProgID + "." + CODEWINDOW_COMMAND_NAME, 0);
                //}
                //catch
                //{
                //}
                if (codeWindowCommand == null)
                {

                    codeWindowCommand = _applicationObject.Commands.AddNamedCommand(_addInInstance, CODEWINDOW_COMMAND_NAME, CODEWINDOW_COMMAND_NAME, "Google the seleceted text in default browser",
                                        true, 6, ref contextGUIDS, (int)vsCommandStatus.vsCommandStatusSupported + (int)vsCommandStatus.vsCommandStatusEnabled);
                }
                commandBars = ((CommandBars)_applicationObject.CommandBars);
                codeCommandBar = ((Microsoft.VisualStudio.CommandBars.CommandBars)_applicationObject.CommandBars)["Code window"];;
                codeWindowButton=((CommandBarControl)codeWindowCommand.AddControl(codeCommandBar,codeCommandBar.Controls.Count+1));
                codeWindowButton.Caption = "Google It";
                codeWindowButton.TooltipText = "Google the seleceted text in default browser";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

		}

		/// <summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
		/// <param term='custom'>Array of parameters that are host application specific.</param>
		/// <seealso class='IDTExtensibility2' />
		public void OnBeginShutdown(ref Array custom)
		{
		}
        public void QueryStatus(string commandName, vsCommandStatusTextWanted neededText, ref vsCommandStatus status, ref object commandText)
        {
            if(neededText ==vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
            {
                if (commandName == _addInInstance.ProgID + "." + CODEWINDOW_COMMAND_NAME)
                {
                    status =(vsCommandStatus)vsCommandStatus.vsCommandStatusSupported|vsCommandStatus.vsCommandStatusEnabled;
                    
                }
            }
        }

        public void Exec(string commandName, vsCommandExecOption executeOption, ref object varIn, ref object varOut, ref bool handled)
        {
            handled = false;
            if (executeOption == vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                if (commandName == _addInInstance.ProgID + "." + CODEWINDOW_COMMAND_NAME)
                {
                    HandleCodeWindowCommand();
                    handled = true;
                }
            }
        }

        public void HandleCodeWindowCommand()
        {
            TextSelection selection = ((TextSelection)_applicationObject.ActiveWindow.Document.Selection);
            if (selection.Text == "")
                MessageBox.Show("No Text selected");
            else
                System.Diagnostics.Process.Start("http://www.google.com/search?hl=en&q=" + selection.Text);
        }
        private const string CODEWINDOW_COMMAND_NAME = "GoogleIt";
        object[] contextGUIDS = new object[] { };
		private DTE2 _applicationObject;
		private AddIn _addInInstance;
	}
}