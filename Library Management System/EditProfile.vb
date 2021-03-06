Public Class EditProfileForm
    Private Shared DropDownActive As Boolean = False
    Dim editstring As String = ""

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.Close()
	End Sub

	Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Me.Close()
	End Sub

	Private Sub DropDownButton_Click(sender As Object, e As EventArgs) Handles DropDownButton.Click
		If DropDownActive = False Then
			DropDown.Show(DropDownButton, New Point(0, 0), ToolStripDropDownDirection.AboveRight)
			DropDownActive = True
		Else
			DropDownActive = False
			DropDown.Hide()
		End If
	End Sub

	Private Sub EditProfileForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StudentAcc.Checked = False
        TeacherAcc.Checked = False
		AdminAcc.Checked = False
		UsernameTextBox.Text = ""
		FullnameTextBox.Text = ""
		OldPasswordTextBox.Text = ""
		NewPasswordTextBox.Text = ""
		ConfirmPasswordTextPassword.Text = ""
		DropDownButton.Text = ""
		If GLogin.LoggedIn = False Then
			MessageBox.Show("Already Logged In", "You are already Logged in! Logic error in form", MessageBoxButtons.OK, MessageBoxIcon.Information)
			MyBase.Close()
		End If
		UsernameTextBox.Text = GLogin.Username
		FullnameTextBox.Text = GLogin.Fullname
        DropDownButton.Text = GLogin.AccType
        If GLogin.AccType = "Student" Then
            StudentAcc.Checked = True
        ElseIf GLogin.AccType = "Teacher" Then
            TeacherAcc.Checked = True
        Else
            AdminAcc.Checked = True
        End If

    End Sub

	Private Sub EditProfileButton_Click(sender As Object, e As EventArgs) Handles EditProfileButton.Click
		' TODO: Update row to reflect change in profile
		If UsernameTextBox.Text <> GLogin.Username Then
			For Each C As Char In UsernameTextBox.Text
				If AscW(C) >= AscW("a") AndAlso AscW("z") >= AscW(C) Then
					Continue For
				ElseIf AscW(C) >= AscW("A") AndAlso AscW("Z") >= AscW(C) Then
					Continue For
				ElseIf AscW(C) >= AscW("0") AndAlso AscW("9") >= AscW(C) Then
					Continue For
				ElseIf AscW(C) = AscW("_") Then
					Continue For
				Else
					MessageBox.Show("Invalid Characters in Username", "Use only alphanumerics( a-z or A-Z ) or underscores( _ )", MessageBoxButtons.OK, MessageBoxIcon.Error)
					Exit Sub
				End If
			Next
            ' change username here

            If SqlInterface.Changeusername(UsernameTextBox.Text) = False Then
                MessageBox.Show("please enter a valid username", "error 1")
            Else

            End If

        End If
        If FullnameTextBox.Text <> GLogin.Fullname Then
            For Each C As Char In FullnameTextBox.Text
                If AscW(C) >= AscW("a") AndAlso AscW("z") >= AscW(C) Then
                    Continue For
                ElseIf AscW(C) >= AscW("A") AndAlso AscW("Z") >= AscW(C) Then
                    Continue For
                ElseIf AscW(C) = AscW(" ") Then
                    Continue For
                Else
                    MessageBox.Show("Invalid Characters in Name", "Use only alphanumerics( a-z or A-Z ) or Space", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    GLogin.LogOut()
                    Exit Sub
                End If
            Next
            ' change fulllname here
            If SqlInterface.Changefullname(FullnameTextBox.Text) = False Then
                MessageBox.Show("Enter a valid Name", "Invalid Name")
            Else

            End If

        End If

        If DropDownButton.Text <> GLogin.AccType Then
            ' change acc type here
            SqlInterface.Changestatus(DropDownButton.Text)
        End If


    End Sub

	Private Sub ChangePasswordButton_Click(sender As Object, e As EventArgs) Handles ChangePasswordButton.Click
		If True Then ' check old password here
			If NewPasswordTextBox.Text = ConfirmPasswordTextPassword.Text Then
				' change pass word here
			Else
				MessageBox.Show("New Passwords do not match. Try Again", "Passwords Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error)
				NewPasswordTextBox.Text = ""
				ConfirmPasswordTextPassword.Text = ""
			End If
		Else
			MessageBox.Show("Old Passwordis incorrect. Try Again", "Passwords Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error)
			OldPasswordTextBox.Text = ""
			NewPasswordTextBox.Text = ""
			ConfirmPasswordTextPassword.Text = ""

		End If
	End Sub

	Private Sub StudentAcc_Click(sender As Object, e As EventArgs) Handles StudentAcc.Click
		DropDownButton.Text = "Student"
		StudentAcc.Checked = True
		TeacherAcc.Checked = False
		AdminAcc.Checked = False
	End Sub

	Private Sub TeacherAcc_Click(sender As Object, e As EventArgs) Handles TeacherAcc.Click
		DropDownButton.Text = "Teacher"
		StudentAcc.Checked = False
		TeacherAcc.Checked = True
		AdminAcc.Checked = False
	End Sub

	Private Sub AdminAcc_Click(sender As Object, e As EventArgs) Handles AdminAcc.Click
		DropDownButton.Text = "Admin"
		StudentAcc.Checked = False
		TeacherAcc.Checked = False
		AdminAcc.Checked = True
	End Sub


	Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
		MyBase.Close()
		AccountSummaryForm.Show()
	End Sub
End Class
