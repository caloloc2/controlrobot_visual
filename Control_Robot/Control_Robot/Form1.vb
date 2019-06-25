Public Class Form1
    Dim adelante, izquierda, derecha, detener, accion1, accion2, automatico, manual As Integer
    Dim conectado As Boolean = False

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If conectado = False Then
            If Conectar() = False Then
                MsgBox("No se ha realizado la conexión al dispositivo, revise la configuración.", MsgBoxStyle.Critical)
            End If
        Else
            If SerialPort1.IsOpen Then
                Cerrar_Conexion()
            End If
        End If
    End Sub

    Private Sub Cerrar_Conexion()
        SerialPort1.Close()
        ToolStripStatusLabel1.Text = "Desconectado"
        Label1.ForeColor = Color.DimGray
        Button7.Text = "Conectar"
        GroupBox1.Enabled = False
        Timer1.Enabled = False
        conectado = False
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        adelante = Math.Abs(adelante - 1)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        izquierda = Math.Abs(izquierda - 1)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        derecha = Math.Abs(derecha - 1)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        detener = Math.Abs(detener - 1)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        accion1 = Math.Abs(accion1 - 1)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        accion2 = Math.Abs(accion2 - 1)
    End Sub

    Private Sub ConfiguraciónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfiguraciónToolStripMenuItem.Click

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        If SerialPort1.IsOpen Then
            SerialPort1.Close()
        End If
        Application.Exit()
    End Sub

    Private Sub ConexiónToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConexiónToolStripMenuItem.Click
        Configuracion.ShowDialog()
    End Sub

    Function Conectar() As Boolean
        Conectar = False

        If Not SerialPort1.IsOpen Then
            With SerialPort1
                .PortName = My.Settings.puerto_com
                .BaudRate = My.Settings.baudrate
                .StopBits = IO.Ports.StopBits.One
                .DataBits = 8

                Try
                    .Open()
                    ToolStripStatusLabel1.Text = "Conectado"
                    Button7.Text = "Desconectar"
                    conectado = True
                    GroupBox1.Enabled = True
                    Timer1.Enabled = True
                    Conectar = True
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                    Cerrar_Conexion()
                End Try
            End With
        End If

        Return Conectar
    End Function


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If SerialPort1.IsOpen Then
            ' REALIZA EL ENVIO DE DATOS
            Dim envio As String = adelante.ToString + "*" + derecha.ToString + "*" + izquierda.ToString +
                "*" + detener.ToString + "*" + accion1.ToString + "*" + accion2.ToString +
                "*" + automatico.ToString + "*" + manual.ToString
            SerialPort1.Write(envio)

            ' REALIZA LA LECTURA DE DATOS
            Dim data As String = SerialPort1.ReadLine()
            If (Trim(data) <> "") Then
                'Label2.Text = Trim(data)
                Sensor_Evasion(data)
            End If
        End If
    End Sub

    Private Sub Sensor_Evasion(data As String)
        If IsNumeric(data) Then
            Dim valor As Integer = CInt(data)

            If valor = 1 Then
                Label1.ForeColor = Color.DarkOrange
            ElseIf valor = 0 Then
                Label1.ForeColor = Color.DimGray
            End If
        End If
    End Sub
End Class
