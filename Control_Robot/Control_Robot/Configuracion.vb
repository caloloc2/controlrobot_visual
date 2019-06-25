Public Class Configuracion
    Private Sub Configuracion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each sp As String In My.Computer.Ports.SerialPortNames
            ComboBox1.Items.Add(sp)
        Next

        ComboBox1.Text = My.Settings.puerto_com
        TextBox1.Text = My.Settings.baudrate

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        My.Settings.puerto_com = ComboBox1.Text
        My.Settings.baudrate = TextBox1.Text
        My.Settings.Save()

        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not SerialPort1.IsOpen Then
            With SerialPort1
                .PortName = ComboBox1.Text
                .BaudRate = TextBox1.Text
                .StopBits = IO.Ports.StopBits.One
                .DataBits = 8
                Try
                    .Open()
                    MsgBox("Se ha conectado correctamente.", MsgBoxStyle.OkOnly)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                    .Close()
                End Try
            End With
        End If
    End Sub
End Class