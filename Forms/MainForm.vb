Option Strict On

Public Class MainForm

    'Fields
    Public Shared _shoppingCart As IShoppingCart
    Private ReadOnly _itemList As New List(Of IItem)

    'Constructor
    Public Sub New()
        InitializeComponent()
        'Create a shared shopping cart object
        _shoppingCart = New ShoppingCart()
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AddHandler btnCheckOut.Click, AddressOf _shoppingCart.CheckOut

        If _shoppingCart IsNot Nothing Then
            _itemList.AddRange(BuildStore())
        End If
    End Sub

    Private Sub ClearData()
        'pry should just subscribe an event...
        _shoppingCart.EmptyCart()

        'TODO: Loop through all of these
        Apples.SelectedIndex = -1
        Bananas.SelectedIndex = -1
        Grapes.SelectedIndex = -1
        Chair.SelectedIndex = -1
        Lamp.SelectedIndex = -1
        Fan.SelectedIndex = -1
        TV.SelectedIndex = -1
        Phone.SelectedIndex = -1
        Clock.SelectedIndex = -1
    End Sub

    Private Sub btnEmptyCart_Click(sender As Object, e As EventArgs) Handles btnEmptyCart.Click

        Dim prompt = "Are you sure you want to empty your cart?"
        Dim choice = MessageBox.Show(prompt, "Confirm Action", MessageBoxButtons.YesNo)
        If choice = DialogResult.Yes Then
            ClearData()
        End If

    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        Dim prompt = "Are you sure you want to exit?"
        Dim choice = MessageBox.Show(prompt, "Confirm Action", MessageBoxButtons.YesNo)
        If choice = DialogResult.Yes Then Application.Exit()

    End Sub

    Private Sub ItemQuantityChanged(sender As Object, e As EventArgs) Handles Apples.SelectedValueChanged, Bananas.SelectedValueChanged, Grapes.SelectedValueChanged,
                                                                            Chair.SelectedValueChanged, Lamp.SelectedValueChanged, Fan.SelectedValueChanged,
                                                                            TV.SelectedValueChanged, Phone.SelectedValueChanged, Clock.SelectedValueChanged
        Dim box = CType(sender, ComboBox)
        If box.SelectedItem IsNot Nothing Then
            'Definitely not the best approach but it'll work for the assignment :-/
            _shoppingCart.UpdateCart(_itemList.Find(Function(x) x.Name = box.Name), CInt(box.SelectedItem))
        End If

    End Sub

    Private Function BuildStore() As List(Of IItem)
        'Produce
        Dim apples As New Item With {.Name = "Apples",
                                     .Price = 5.3D}
        Dim bananas As New Item With {.Name = "Bananas",
                                     .Price = 2.55D
        }
        Dim grapes As New Item With {.Name = "Grapes",
                                     .Price = 4.85D}
        'Home goods
        Dim chair As New Item With {.Name = "Chair",
                                    .Price = 50.7D}
        Dim lamp As New Item With {.Name = "Lamp",
                                   .Price = 25.75D}
        Dim fan As New Item With {.Name = "Fan",
                                  .Price = 100.25D}
        'Electronics
        Dim television As New Item With {.Name = "TV",
                                         .Price = 535.8D}
        Dim phone As New Item With {.Name = "Phone",
                                    .Price = 777.45D}
        Dim clock As New Item With {.Name = "Clock",
                                         .Price = 25.9D}

        Dim itemList As New List(Of IItem) From {apples, bananas, grapes,
                                                chair, lamp, fan,
                                                television, phone, clock}
        Return itemList

    End Function

End Class
