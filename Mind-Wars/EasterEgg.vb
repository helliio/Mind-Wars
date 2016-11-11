Option Strict On
Option Explicit On
Option Infer Off

'Copyright (c) 2016 Liang Zhu
'Applies to this class (EasterEgg.vb)
'Permission is hereby granted, free of charge, to any person obtaining
'a copy of this software and associated documentation files (the
'"Software"), to deal in the Software without restriction, including
'without limitation the rights to use, copy, modify, merge, publish,
'distribute, sublicense, and/or sell copies of the Software, and to
'permit persons to whom the Software is furnished to do so, subject to
'the following conditions:

'The above copyright notice and this permission notice shall be
'included in all copies or substantial portions of the Software.

'THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
'EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
'MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
'IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
'CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
'TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
'SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

'imdb.com/title/tt1212027/goofs?item=gf1175507
Public Class EasterEgg
    Private egg As Integer = 1, counter As Integer
    Private strHostName, strIPAddress As String
    Sub New(Optional n As Integer = 0)
        counter = n
        strHostName = System.Net.Dns.GetHostName()
        strIPAddress = System.Net.Dns.GetHostEntry(strHostName).AddressList(0).ToString()
    End Sub
    Public Sub Trigger()
        If egg < counter Then
            egg += 1
        Else
            Clear()
        End If
    End Sub
    Public Sub Clear()
        egg = 1
    End Sub
End Class
