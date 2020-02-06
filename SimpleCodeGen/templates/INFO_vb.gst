Namespace MGW.<%DataBaseName%>
    <Serializable()> _
    Partial Public Class <%TableName[Singular]%>
		<%= CreateVariablesFromColumns(); %>
		<%= CreatePropertiesFromColumns(); %>
    End Class

    <Serializable()> _
    Partial Public Class <%TableName%>Coleccion
        Inherits System.Collections.Generic.List(Of <%TableName%>)
    End Class
	
	
	<Serializable()> _
	Partial Public Class <%TableName%>Filtro
		<%= CreateFilterPropertiesFromColumns(); %>
    End Class
End Namespace


