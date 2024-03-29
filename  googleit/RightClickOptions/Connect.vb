'Imports System
'Imports Microsoft.VisualStudio.CommandBars
'Imports Extensibility
'Imports EnvDTE
'Imports EnvDTE80
'Imports System.Windows.Forms

'Public Class Connect

'    Implements IDTExtensibility2
'    Implements IDTCommandTarget


'    Const CODEWINDOW_COMMAND_NAME As String = "EmailThisCode"
'    Const PROJECT_COMMAND_NAME As String = "ShowCodeFiles"
'    Const SOLUTION_COMMAND_NAME As String = "CheckSolutionDetails"

'    Dim _applicationObject As DTE2
'    Dim _addInInstance As AddIn

'    Dim _codeWindowButton As CommandBarControl = Nothing
'    Dim _projectButton As CommandBarControl = Nothing
'    Dim _solutionButton As CommandBarControl = Nothing

'    '''<summary>Implements the constructor for the Add-in object. Place your initialization code within this method.</summary>
'    Public Sub New()

'    End Sub

'    '''<summary>Implements the OnConnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being loaded.</summary>
'    '''<param name='application'>Root object of the host application.</param>
'    '''<param name='connectMode'>Describes how the Add-in is being loaded.</param>
'    '''<param name='addInInst'>Object representing this Add-in.</param>
'    '''<remarks></remarks>
'    Public Sub OnConnection(ByVal application As Object, ByVal connectMode As ext_ConnectMode, ByVal addInInst As Object, ByRef custom As Array) Implements IDTExtensibility2.OnConnection
'        _applicationObject = CType(application, DTE2)
'        _addInInstance = CType(addInInst, AddIn)

'        If (connectMode = ext_ConnectMode.ext_cm_AfterStartup) Then
'            OnStartupComplete(Nothing)
'        End If
'    End Sub

'    '''<summary>Implements the OnDisconnection method of the IDTExtensibility2 interface. Receives notification that the Add-in is being unloaded.</summary>
'    '''<param name='disconnectMode'>Describes how the Add-in is being unloaded.</param>
'    '''<param name='custom'>Array of parameters that are host application specific.</param>
'    '''<remarks></remarks>
'    Public Sub OnDisconnection(ByVal disconnectMode As ext_DisconnectMode, ByRef custom As Array) Implements IDTExtensibility2.OnDisconnection
'        If (_codeWindowButton IsNot Nothing) Then
'            _codeWindowButton.Delete()
'        End If
'        If (_projectButton IsNot Nothing) Then
'            _projectButton.Delete()
'        End If
'        If (_solutionButton IsNot Nothing) Then
'            _solutionButton.Delete()
'        End If
'    End Sub

'    '''<summary>Implements the OnAddInsUpdate method of the IDTExtensibility2 interface. Receives notification that the collection of Add-ins has changed.</summary>
'    '''<param name='custom'>Array of parameters that are host application specific.</param>
'    '''<remarks></remarks>
'    Public Sub OnAddInsUpdate(ByRef custom As Array) Implements IDTExtensibility2.OnAddInsUpdate
'    End Sub

'    '''<summary>Implements the OnStartupComplete method of the IDTExtensibility2 interface. Receives notification that the host application has completed loading.</summary>
'    '''<param name='custom'>Array of parameters that are host application specific.</param>
'    '''<remarks></remarks>
'    Public Sub OnStartupComplete(ByRef custom As Array) Implements IDTExtensibility2.OnStartupComplete
'        Dim codeWindowCommand As Command = Nothing
'        Dim projectCommand As Command = Nothing
'        Dim solutionCommand As Command = Nothing
'        Dim codeCommandBar, projectCommandBar, solutionCommandBar As CommandBar
'        Dim commandBars As CommandBars

'        Try
'            codeWindowCommand = AddCommand(CODEWINDOW_COMMAND_NAME, "Send the selected text by email", 18)

'            projectCommand = AddCommand(PROJECT_COMMAND_NAME, "Check code files", 19)

'            solutionCommand = AddCommand(SOLUTION_COMMAND_NAME, "Check Solution Details", 17)

'            commandBars = DirectCast(_applicationObject.CommandBars, CommandBars)

'            codeCommandBar = commandBars.Item("Code Window")
'            solutionCommandBar = commandBars.Item("Solution")
'            projectCommandBar = commandBars.Item("Project")

'            ' Add a button to the built-in "Code Window" context menu
'            _codeWindowButton = AddCommandBar(codeWindowCommand, codeCommandBar, codeCommandBar.Controls.Count + 1, "Email This Code", "Send the selected text by email")

'            _solutionButton = AddCommandBar(solutionCommand, solutionCommandBar, solutionCommandBar.Controls.Count + 1, "Check Solution Details", "Check Solution Details")

'            _projectButton = AddCommandBar(projectCommand, projectCommandBar, projectCommandBar.Controls.Count + 1, "View Code Files...", "View the files containing code in this project")

'        Catch ex As Exception
'            MsgBox(ex.ToString())
'        End Try

'    End Sub

'    Public Function AddCommand(ByVal name As String, ByVal caption As String, ByVal iconID As Integer) As Command
'        Dim foundCommand As Command = Nothing

'        Try
'            foundCommand = _applicationObject.Commands.Item(_addInInstance.ProgID & "." & name)
'        Catch
'            ' command already exists, so just ignore
'        End Try

'        If (foundCommand Is Nothing) Then
'            foundCommand = _applicationObject.Commands.AddNamedCommand(_addInInstance, name, name, caption, _
'                                True, iconID, Nothing, vsCommandStatus.vsCommandStatusSupported Or vsCommandStatus.vsCommandStatusEnabled)
'        End If

'        Return foundCommand
'    End Function

'    Public Function AddCommandBar(ByVal associatedCommand As Command, ByVal parentCommandBar As CommandBar, ByVal position As Integer, ByVal caption As String, ByVal tooltip As String) As CommandBarControl
'        Dim newControl As CommandBarControl = DirectCast(associatedCommand.AddControl(parentCommandBar, position), CommandBarControl)
'        newControl.Caption = caption
'        newControl.TooltipText = tooltip

'        Return newControl
'    End Function

'    '''<summary>Implements the OnBeginShutdown method of the IDTExtensibility2 interface. Receives notification that the host application is being unloaded.</summary>
'    '''<param name='custom'>Array of parameters that are host application specific.</param>
'    '''<remarks></remarks>
'    Public Sub OnBeginShutdown(ByRef custom As Array) Implements IDTExtensibility2.OnBeginShutdown
'    End Sub

'    Public Sub Exec(ByVal CmdName As String, ByVal ExecuteOption As EnvDTE.vsCommandExecOption, ByRef VariantIn As Object, ByRef VariantOut As Object, ByRef Handled As Boolean) Implements EnvDTE.IDTCommandTarget.Exec
'        Handled = False

'        If (ExecuteOption = vsCommandExecOption.vsCommandExecOptionDoDefault) Then

'            ' 'Email This Code...' button clicked
'            If CmdName = _addInInstance.ProgID & "." & CODEWINDOW_COMMAND_NAME Then
'                HandleCodeWindowCommand()

'                Handled = True
'            End If

'            If CmdName = _addInInstance.ProgID & "." & SOLUTION_COMMAND_NAME Then
'                MsgBox("Solution Path: " + _applicationObject.Solution.FullName)
'                Clipboard.SetText(_applicationObject.Solution.FullName)

'                Handled = True
'            End If

'            If CmdName = _addInInstance.ProgID & "." & PROJECT_COMMAND_NAME Then
'                HandleProjectCommand()

'                Handled = True
'            End If

'        End If
'    End Sub

'    Public Sub QueryStatus(ByVal CmdName As String, ByVal NeededText As EnvDTE.vsCommandStatusTextWanted, ByRef StatusOption As EnvDTE.vsCommandStatus, ByRef CommandText As Object) Implements EnvDTE.IDTCommandTarget.QueryStatus
'        If NeededText = vsCommandStatusTextWanted.vsCommandStatusTextWantedNone Then

'            ' Handle Code Window popup
'            If (CmdName = _addInInstance.ProgID & "." & CODEWINDOW_COMMAND_NAME) Then
'                Dim currentFileName As String = _applicationObject.ActiveWindow.Document.FullName.ToLower()
'                If (currentFileName.EndsWith(".vb") OrElse currentFileName.EndsWith(".cs") OrElse currentFileName.EndsWith(".xml")) Then
'                    StatusOption = CType(vsCommandStatus.vsCommandStatusEnabled + _
'                        vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)
'                Else
'                    StatusOption = CType(vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)
'                End If
'            ElseIf (CmdName.StartsWith(_addInInstance.ProgID)) Then
'                StatusOption = CType(vsCommandStatus.vsCommandStatusEnabled + _
'                    vsCommandStatus.vsCommandStatusSupported, vsCommandStatus)
'            End If
'        End If
'    End Sub

'    Public Sub HandleCodeWindowCommand()
'        Dim selection As TextSelection = DirectCast(_applicationObject.ActiveWindow.Document.Selection, TextSelection)
'        If (selection.Text = String.Empty) Then
'            MsgBox("No Text Selected!")
'        Else
'            Dim emailForm As New EmailForm(selection.Text)
'            emailForm.ShowDialog()
'        End If
'    End Sub

'    Public Sub HandleProjectCommand()
'        Dim hierarchy As UIHierarchy = _applicationObject.ToolWindows.SolutionExplorer

'        For Each selectedItem As UIHierarchyItem In DirectCast(hierarchy.SelectedItems, Array)
'            If (TypeOf (selectedItem.Object) Is EnvDTE.Project) Then
'                Dim project As EnvDTE.Project = DirectCast(selectedItem.Object, EnvDTE.Project)

'                Dim projectAnalyserForm As New ProjectAnalyserForm(project)
'                projectAnalyserForm.ShowDialog()
'            End If
'        Next
'    End Sub

'End Class
