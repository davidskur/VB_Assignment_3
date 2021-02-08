Option Strict On

Public Class ShoppingCart
    Implements IShoppingCart

    'Fields
    Private _cartContents As New List(Of IItem)

    'Properties
    Public Property GetItems As List(Of IItem)
        Get
            Return _cartContents
        End Get
        Set(value As List(Of IItem))

        End Set
    End Property

    'IShoppingCart interface implementations
    Public Sub UpdateCart(item As IItem, quantity As Integer) Implements IShoppingCart.UpdateCart
        'Kinda clunky. Definitely needs a refactor but I'm writing this during the Super Bowl :-D
        _cartContents.RemoveAll(Function(x) x.Name = item.Name)
        _cartContents.AddRange(Enumerable.Repeat(item, quantity))
    End Sub

    Public Sub EmptyCart() Implements IShoppingCart.EmptyCart
        _cartContents.Clear()
    End Sub

    Public Sub CheckOut(sender As Object, e As System.EventArgs) Implements IShoppingCart.CheckOut

        If _cartContents.Count > 0 Then

            Dim totalCost As Decimal = 0
            For Each cartItem As IItem In _cartContents
                totalCost += cartItem.Price
            Next

            'create new form here
            Using dialog As New ReceiptDialog()
                dialog.lblSubtotal.Text = $"{totalCost}"
                dialog.lblSalesTax.Text = $"{totalCost * 0.6D}"
                totalCost = (totalCost + (totalCost * 0.6D))

                'Calculate change. I've got a bug here, I need to make sure output is only formatted to 2 decimal places.
                Dim pennies, nickels, dimes, quarters As Integer
                Dim change = (totalCost - Math.Truncate(totalCost)) * 100

                quarters = CType(change, Currency) \ Currency.Quarter 'so convoluted
                change = change Mod Currency.Quarter

                dimes = CType(change, Currency) \ Currency.Dime
                change = change Mod Currency.Dime

                nickels = CType(change, Currency) \ Currency.Nickel
                change = change Mod Currency.Nickel

                pennies = CType(change, Currency) \ Currency.Penny

                dialog.lblTotal.Text = $"{totalCost}"
                dialog.lblChangeQuantity.Text = $"{quarters}Q, {dimes}D, {nickels}N, {pennies}P"
                dialog.ShowDialog()
            End Using

        End If

    End Sub

End Class
