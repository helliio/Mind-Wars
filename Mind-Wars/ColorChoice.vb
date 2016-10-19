Public Class ColorChoice
    Public varRectangle As Rectangle
    Private RepresentsColor_Tag As Integer = 0
    Private varSelected As Boolean = False
    Private varStateSize As Integer ' 0 = size is correct, 1 = size is too small, 2 = size is too large

    Public ReadOnly Property Rectangle As Rectangle
        Get
            Return varRectangle
        End Get
    End Property

    Public Property RectangleLocation As Point
        Get
            Return varRectangle.Location
        End Get
        Set(value As Point)
            varRectangle.Location = value
        End Set
    End Property

    Public Property RectangleSize As Integer
        Get
            Return varRectangle.Width
        End Get
        Set(value As Integer)
            varRectangle.Width = value
            varRectangle.Height = value
        End Set
    End Property

    Public Property Selected As Boolean
        Set(value As Boolean)
            varSelected = value
            ChoiceList.Item(RepresentsColor_Tag).Invalidate()
        End Set
        Get
            Return varSelected
        End Get
    End Property

    Public Property RepresentsTag As Integer
        Set(value As Integer)
            RepresentsColor_Tag = value
        End Set
        Get
            Return RepresentsColor_Tag
        End Get
    End Property

    Public Property SizeState As Integer
        Get
            Return varStateSize
        End Get
        Set(value As Integer)
            varStateSize = value
        End Set
    End Property

End Class
