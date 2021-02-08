Option Strict On

Public Class Item
    Implements IItem

    Private _name As String
    Private _price As Decimal

    'Default constructor
    Public Sub New()
        Me._name = "Stage Scotch"
        Me._price = 0
    End Sub

    'Constructor
    Public Sub New(name As String, price As Decimal)
        Me._name = name
        Me._price = price
    End Sub

    Public Property Name As String Implements IItem.Name
        Get
            Return _name
        End Get
        Set(value As String)
            _name = If(value.Length > 0, value, "Stage Scotch")
        End Set
    End Property

    Public Property Price As Decimal Implements IItem.Price
        Get
            Return _price
        End Get
        Set(value As Decimal)
            _price = If(value >= 0, value, 0)
        End Set
    End Property
End Class
