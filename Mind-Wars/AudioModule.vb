Module AudioModule
    Public AudioStarted As Boolean = False
    Sub PlayLoopingBackgroundSoundFile(ByVal songnumber As Integer)
        Select Case songnumber
            Case 1
                'My.Computer.Audio.Play("C:\MindWars\A Different Time in Another World.wav", AudioPlayMode.BackgroundLoop)
            Case 2
                'My.Computer.Audio.Play("C:\MindWars\Hello_World_The_Birth_Of_OS0_1.wav", AudioPlayMode.BackgroundLoop)
            Case 3
                'My.Computer.Audio.Play("C:\MindWars\Creperum_Ex_Machina.wav", AudioPlayMode.BackgroundLoop)
            Case 4
                'My.Computer.Audio.Play("C:\MindWars\Zero_Day.wav", AudioPlayMode.BackgroundLoop)
        End Select
    End Sub
End Module
