Public Class MinimaxFunctions

    Function MiniCalculateEliminated(ByVal B As Integer, ByVal W As Integer, ByVal HypotheticalGuess() As Integer, ByVal L As ArrayList) As Integer
        Dim SolutionsEliminated As Integer = 0
        Dim q As Integer = 0
        Dim ListCount As Integer = L.Count - 1
        Do Until q = ListCount
            AI_BW_Check = MiniGetBW(L.Item(q), HypotheticalGuess)
            If Not AI_BW_Check(0) = B OrElse Not AI_BW_Check(1) = W Then
                SolutionsEliminated += 1
            End If
            q += 1
        Loop
        Return SolutionsEliminated
    End Function

    Function MiniIntToArr(ByVal int As Integer) As Integer()
        Dim str As String = int.ToString
        Dim arr(str.Length - 1) As Integer
        Dim l As Integer = str.Length - 1
        For i As Integer = 0 To l
            arr(i) = str.Chars(i).ToString
        Next
        Return arr
    End Function

    Function MiniArrayToInt(ByVal array() As Integer) As Integer
        Dim int As Integer
        Dim l As Integer = array.Length - 1
        For i As Integer = 0 To l
            int += array(i) * 10 ^ (l - i)
        Next
        Return int
    End Function

    Public Function MiniGetBW(ByVal ail() As Integer, ByVal g() As Integer) As Integer()
        Dim whitepegs As Integer
        Dim blackpegs As Integer
        Dim counted(holes - 1) As Integer
        Dim correct(holes - 1) As Integer

        Dim checkcorrectindex As Integer = 0
        Do
            If ail(checkcorrectindex) = g(checkcorrectindex) Then
                blackpegs += 1
                counted(checkcorrectindex) = 1
                correct(checkcorrectindex) = 1
            End If
            checkcorrectindex += 1
        Loop Until checkcorrectindex = holes

        Dim currentstep As Integer = 0
        Do
            If correct(currentstep) = 0 AndAlso ail.Contains(g(currentstep)) Then
                Dim countedindex As Integer = 0
                Dim foundmatch As Boolean = False
                Do
                    If countedindex = currentstep Then
                        countedindex += 1
                        Continue Do
                    ElseIf ail(countedindex) = g(currentstep) AndAlso counted(countedindex) = 0 Then
                        counted(countedindex) = 1
                        whitepegs += 1
                        foundmatch = True
                    End If
                    countedindex += 1
                Loop Until countedindex = holes Or foundmatch = True
            End If
            currentstep += 1
        Loop Until currentstep = holes

        Dim AntallBW(1) As Integer
        AntallBW = {blackpegs, whitepegs}
        Return AntallBW
    End Function

End Class
