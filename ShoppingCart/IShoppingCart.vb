Public Interface IShoppingCart

    Sub UpdateCart(item As IItem, quantity As Integer)
    Sub EmptyCart()
    Sub CheckOut(sender As Object, e As System.EventArgs)

End Interface
