Imports System.Data
Imports System.Data.SqlClient

Namespace MGW.<%DataBaseName%>

    Partial Public Class <%TableName%>
		Private Shared Function zCrearUnoDelReader(ByVal dataRecord As IDataRecord) As MGW.<%DataBaseName%>.<%TableName%>
			Dim agregador As New MGW.<%DataBaseName%>.<%TableName%>
    		agregador.IdAgregador=dataRecord("IdAgregador")
agregador.Nombre = dataRecord("Nombre")
agregador.URLAgregador = dataRecord("URLAgregador")
agregador.IdDivisa = dataRecord("IdDivisa")
agregador.ComisionFija = dataRecord("ComisionFija")
agregador.BorradoLogico = dataRecord("BorradoLogico")
agregador.FechaCreacion = dataRecord("FechaCreacion")
agregador.FechaModificacion = dataRecord("FechaModificacion")
agregador.IdInicioSesionModificacion = dataRecord("IdInicioSesionModificacion")
            return agregador			
        End Function
        Public Shared Function zRellenarDataTable(ByVal datatable As DataTable, iIdRefCampo as int32, Optional ByVal conexion As SqlConnection = Nothing, Optional ByVal transaccion As SqlTransaction = Nothing) As Integer
            Dim SQL As String = ""
            Dim comando As SqlCommand
            Dim adapter As SqlDataAdapter
			SQL = "SELECT IdAgregador,Nombre,URLAgregador,IdDivisa,ComisionFija,BorradoLogico,FechaCreacion,FechaModificacion,IdInicioSesionModificacion FROM VEN_Agregadores " & _
				  " WHERE BorradoLogico= 0;"
            comando = New SqlCommand
            comando.CommandText = SQL
            comando.CommandType = CommandType.Text
            comando.Transaction = transaccion
            datatable.Clear()
            adapter = New SqlDataAdapter
            adapter.SelectCommand = comando
			If conexion Is Nothing Then
            	conexion = New SqlConnection()
            	conexion.ConnectionString = DAL.ConectDal.DevolverCadenaConexion(iIdRefCampo)
            End If
            comando.Connection = conexion
            Return adapter.Fill(datatable)
        End Function	
        Public Shared Function zModificar(ByVal oAgregador As MGW.<%DataBaseName%>.Agregador, iIdRefCampo as int32, Optional ByVal conexion As SqlConnection = Nothing, Optional ByVal transaccion As SqlTransaction = Nothing) As Integer
            Dim SQL As String = ""
            Dim comando As SqlCommand
            Dim contadorAgregadores As Integer
			Dim estadoInicialConexion As ConnectionState
			SQL = "UPDATE  VEN_Agregadores SET Nombre=@Nombre,URLAgregador=@URLAgregador,IdDivisa=@IdDivisa,ComisionFija=@ComisionFija,BorradoLogico=@BorradoLogico,FechaCreacion=@FechaCreacion,FechaModificacion=@FechaModificacion,IdInicioSesionModificacion=@IdInicioSesionModificacion WHERE (IdAgregador=@IdAgregador" & _
					" AND BorradoLogico= 0);"
            Try
                comando = New SqlCommand
				comando.CommandText = SQL
                comando.CommandType = CommandType.Text
                comando.Transaction = transaccion
				comando.Parameters.Add("@IdAgregador",  SqlDbType.int)
				comando.Parameters("@IdAgregador").SqlValue = oAgregador.IdAgregador
comando.Parameters.Add(New SqlParameter("@Nombre", SqlDbType.NVarChar))
comando.Parameters("@Nombre").SqlValue = oAgregador.Nombre
comando.Parameters.Add(New SqlParameter("@URLAgregador", SqlDbType.NVarChar))
comando.Parameters("@URLAgregador").SqlValue = oAgregador.URLAgregador
comando.Parameters.Add(New SqlParameter("@IdDivisa", SqlDbType.Int))
comando.Parameters("@IdDivisa").SqlValue = oAgregador.IdDivisa
comando.Parameters.Add(New SqlParameter("@ComisionFija", SqlDbType.Decimal))
comando.Parameters("@ComisionFija").SqlValue = oAgregador.ComisionFija
comando.Parameters.Add(New SqlParameter("@BorradoLogico", SqlDbType.Bit))
comando.Parameters("@BorradoLogico").SqlValue = oAgregador.BorradoLogico
comando.Parameters.Add(New SqlParameter("@FechaCreacion", SqlDbType.DateTime))
comando.Parameters("@FechaCreacion").SqlValue = oAgregador.FechaCreacion
comando.Parameters.Add(New SqlParameter("@FechaModificacion", SqlDbType.DateTime))
comando.Parameters("@FechaModificacion").SqlValue = oAgregador.FechaModificacion
comando.Parameters.Add(New SqlParameter("@IdInicioSesionModificacion", SqlDbType.Int))
comando.Parameters("@IdInicioSesionModificacion").SqlValue = oAgregador.IdInicioSesionModificacion
				if conexion IsNot Nothing Then
					estadoInicialConexion = conexion.State
				Else
					conexion = New SqlConnection()
                	conexion.ConnectionString = DAL.ConectDal.DevolverCadenaConexion(iIdRefCampo)
                	estadoInicialConexion = ConnectionState.Closed
                	conexion.Open()
				End If
				comando.Connection= conexion
                contadorAgregadores = comando.ExecuteNonQuery()
                If contadorAgregadores = 0 Then
                    Throw New Exception("Error concurrencia")
                End If
                Return contadorAgregadores
            Finally
                If conexion IsNot Nothing AndAlso estadoInicialConexion = ConnectionState.Closed AndAlso conexion.State <> ConnectionState.Closed Then
                    conexion.Close()
                End If               
            End Try
        End Function
        Public Shared Function zInsertar(ByVal oAgregador As MGW.<%DataBaseName%>.Agregador, iIdRefCampo as int32, Optional ByVal conexion As SqlConnection = Nothing, Optional ByVal transaccion As SqlTransaction = Nothing) As Integer
            Dim SQL As String = ""
            Dim comando As SqlCommand
            Dim contadorAgregadores As Integer
          	Dim estadoInicialConexion As ConnectionState
	        SQL = "INSERT INTO VEN_Agregadores (Nombre,URLAgregador,IdDivisa,ComisionFija,BorradoLogico,FechaCreacion,FechaModificacion,IdInicioSesionModificacion) VALUES (@Nombre,@URLAgregador,@IdDivisa,@ComisionFija,@BorradoLogico,@FechaCreacion,@FechaModificacion,@IdInicioSesionModificacion); " & _
					" SELECT @Insertado = SCOPE_IDENTITY(); "
            Try
                comando = New SqlCommand(SQL, conexion)
                comando.CommandType = CommandType.Text
                comando.Transaction = transaccion
comando.Parameters.Add(New SqlParameter("@Nombre", SqlDbType.NVarChar))
comando.Parameters("@Nombre").SqlValue = oAgregador.Nombre
comando.Parameters.Add(New SqlParameter("@URLAgregador", SqlDbType.NVarChar))
comando.Parameters("@URLAgregador").SqlValue = oAgregador.URLAgregador
comando.Parameters.Add(New SqlParameter("@IdDivisa", SqlDbType.Int))
comando.Parameters("@IdDivisa").SqlValue = oAgregador.IdDivisa
comando.Parameters.Add(New SqlParameter("@ComisionFija", SqlDbType.Decimal))
comando.Parameters("@ComisionFija").SqlValue = oAgregador.ComisionFija
comando.Parameters.Add(New SqlParameter("@BorradoLogico", SqlDbType.Bit))
comando.Parameters("@BorradoLogico").SqlValue = oAgregador.BorradoLogico
comando.Parameters.Add(New SqlParameter("@FechaCreacion", SqlDbType.DateTime))
comando.Parameters("@FechaCreacion").SqlValue = oAgregador.FechaCreacion
comando.Parameters.Add(New SqlParameter("@FechaModificacion", SqlDbType.DateTime))
comando.Parameters("@FechaModificacion").SqlValue = oAgregador.FechaModificacion
comando.Parameters.Add(New SqlParameter("@IdInicioSesionModificacion", SqlDbType.Int))
comando.Parameters("@IdInicioSesionModificacion").SqlValue = oAgregador.IdInicioSesionModificacion
				comando.Parameters.Add("@Insertado", SqlDbType.int)
                comando.Parameters("@Insertado").Direction = ParameterDirection.Output
				If conexion IsNot Nothing Then
					estadoInicialConexion = conexion.State
				Else
					conexion = New SqlConnection()
					conexion.ConnectionString =  DAL.ConectDal.DevolverCadenaConexion(iIdRefCampo)
					estadoInicialConexion = ConnectionState.Closed
					conexion.Open()
				End If
				comando.Connection = conexion
				contadorAgregadores = comando.ExecuteNonQuery()
				If contadorAgregadores = 1 Then
                    oAgregador.IdAgregador = comando.Parameters("@Insertado").Value
				End If
                Return contadorAgregadores
           	Finally
                If conexion IsNot Nothing AndAlso estadoInicialConexion = ConnectionState.Closed AndAlso conexion.State <> ConnectionState.Closed Then
                    conexion.Close()
                End If
            End Try
        End Function
        Public Shared Function zDevolverPorId(ByVal IdAgregador As Int32, iIdRefCampo as int32, Optional ByVal conexion As SqlConnection = Nothing, Optional ByVal transaccion As SqlTransaction = Nothing) As MGW.<%DataBaseName%>.AgregadoresColeccion
			Dim SQL As String = ""
            Dim comando As SqlCommand
            Dim reader As SqlDataReader = Nothing
			Dim estadoInicialConexion As ConnectionState
			Dim agregadores As MGW.<%DataBaseName%>.AgregadoresColeccion
			 SQL = "SELECT IdAgregador,Nombre,URLAgregador,IdDivisa,ComisionFija,BorradoLogico,FechaCreacion,FechaModificacion,IdInicioSesionModificacion FROM VEN_Agregadores WHERE IdAgregador=@IdAgregador AND BorradoLogico = 0;"
            Try
				agregadores = New MGW.<%DataBaseName%>.AgregadoresColeccion
                comando = New SqlCommand
				comando.CommandText = SQL
                comando.CommandType = CommandType.Text
                comando.Transaction = transaccion
                comando.Parameters.AddWithValue("@IdAgregador", IdAgregador)
                if conexion IsNot Nothing Then
				estadoInicialConexion = conexion.State
				Else
				conexion = New SqlConnection()
				conexion.ConnectionString = DAL.ConectDal.DevolverCadenaConexion(iIdRefCampo)
				estadoInicialConexion = ConnectionState.Closed
				conexion.Open()
				End If
				comando.Connection = conexion
				reader = comando.ExecuteReader
				While reader.Read
					agregadores.Add(zCrearUnoDelReader(reader))
                End While
                Return agregadores
			Finally
				If reader isNOT Nothing AndAlso Not reader.IsClosed Then
   					reader.Close()
  				End If
                If conexion IsNot Nothing AndAlso estadoInicialConexion = ConnectionState.Closed AndAlso conexion.State = ConnectionState.Open Then
                    conexion.Close()
                End If
            End Try
        End Function 
        Public Shared Function zEliminarFisicoPorId(ByVal IdAgregador As Int32, iIdRefCampo as int32, Optional ByVal conexion As SqlConnection = Nothing, Optional ByVal transaccion As SqlTransaction = Nothing) As Integer
            Dim SQL As String = ""
            Dim comando As SqlCommand
            Dim contadorAgregadores As Integer
			Dim estadoInicialConexion As ConnectionState
            SQL = "DELETE VEN_Agregadores WHERE (IdAgregador=@IdAgregador);"
            Try
                comando = New SqlCommand
				comando.CommandText = SQL
                comando.CommandType = CommandType.Text
                comando.Transaction = transaccion
                comando.Parameters.Add("@IdAgregador", SqlDbType.int)
                comando.Parameters("@IdAgregador").SqlValue = IdAgregador
				if conexion IsNot Nothing Then
					estadoInicialConexion = conexion.State
				Else
					conexion = New SqlConnection()
                	conexion.ConnectionString = DAL.ConectDal.DevolverCadenaConexion(iIdRefCampo)
                	estadoInicialConexion = ConnectionState.Closed
                	conexion.Open()
				End If
				comando.Connection= conexion
                contadorAgregadores = comando.ExecuteNonQuery()
                Return contadorAgregadores
           Finally
                If conexion IsNot Nothing AndAlso estadoInicialConexion = ConnectionState.Closed AndAlso conexion.State <> ConnectionState.Closed Then
                    conexion.Close()
                End If               
            End Try
        End Function		
		Public Shared Function zDevolverColeccionBusqueda(ByVal oFiltro As MGW.<%DataBaseName%>.AgregadorFiltro, iIdRefCampo as int32, Optional ByVal conexion As SqlConnection= Nothing, Optional ByVal transaccion As SqlTransaction = Nothing) As MGW.<%DataBaseName%>.AgregadoresColeccion
		Dim SQL As String = ""
        Dim comando As SqlCommand
        Dim reader As SqlDataReader = Nothing
        Dim agregadoresColeccion As MGW.<%DataBaseName%>.AgregadoresColeccion
        Dim agregador As MGW.<%DataBaseName%>.Agregador
        Dim estadoInicialConexion As ConnectionState
		SQL ="SELECT IdAgregador,Nombre,URLAgregador,IdDivisa,ComisionFija,BorradoLogico,FechaCreacion,FechaModificacion,IdInicioSesionModificacion FROM VEN_Agregadores WHERE 1=1"
If oFiltro.IdAgregador.HasValue Then
SQL +=" and IdAgregador = @IdAgregador"
End If
If not String.IsNullOrEmpty(oFiltro.Nombre) Then
SQL +=" and Nombre like '%' + @Nombre + '%'"
End If
If not String.IsNullOrEmpty(oFiltro.URLAgregador) Then
SQL +=" and URLAgregador like '%' + @URLAgregador + '%'"
End If
If oFiltro.IdDivisa.HasValue Then
SQL +=" and IdDivisa = @IdDivisa"
End If
If oFiltro.ComisionFijaDesde.HasValue Then
SQL +=" and ComisionFija >= @ComisionFijaDesde"
End If
If oFiltro.ComisionFijaHasta.HasValue Then
SQL +=" and ComisionFija <= @ComisionFijaHasta"
End If
If oFiltro.BorradoLogico.HasValue Then
SQL +=" and BorradoLogico = @BorradoLogico"
End If
If oFiltro.FechaCreacionDesde.HasValue Then
SQL +=" and FechaCreacion >= @FechaCreacionDesde"
End If
If oFiltro.FechaCreacionHasta.HasValue Then
SQL +=" and FechaCreacion <= @FechaCreacionHasta"
End If
If oFiltro.FechaModificacionDesde.HasValue Then
SQL +=" and FechaModificacion >= @FechaModificacionDesde"
End If
If oFiltro.FechaModificacionHasta.HasValue Then
SQL +=" and FechaModificacion <= @FechaModificacionHasta"
End If
If oFiltro.IdInicioSesionModificacion.HasValue Then
SQL +=" and IdInicioSesionModificacion = @IdInicioSesionModificacion"
End If
		    Try
				agregadoresColeccion = New MGW.<%DataBaseName%>.AgregadoresColeccion
				agregador = New MGW.<%DataBaseName%>.Agregador
                comando = New SqlCommand
				comando.commandtext = SQL
                comando.CommandType = CommandType.Text
                comando.Transaction = transaccion
If oFiltro.IdAgregador.HasValue Then
comando.Parameters.AddWithValue("@IdAgregador", oFiltro.IdAgregador)
End If
If Not String.IsNullOrEmpty(oFiltro.Nombre) Then
comando.Parameters.AddWithValue("@Nombre", oFiltro.Nombre)
End If
If Not String.IsNullOrEmpty(oFiltro.URLAgregador) Then
comando.Parameters.AddWithValue("@URLAgregador", oFiltro.URLAgregador)
End If
If oFiltro.IdDivisa.HasValue Then
comando.Parameters.AddWithValue("@IdDivisa", oFiltro.IdDivisa)
End If
If oFiltro.ComisionFijaDesde.HasValue Then
comando.Parameters.AddWithValue("@ComisionFijaDesde", oFiltro.ComisionFijaDesde)
End If
If oFiltro.ComisionFijaHasta.HasValue Then
comando.Parameters.AddWithValue("@ComisionFijaHasta", oFiltro.ComisionFijaHasta)
End If
If oFiltro.BorradoLogico.HasValue Then
comando.Parameters.AddWithValue("@BorradoLogico", oFiltro.BorradoLogico)
End If
If oFiltro.FechaCreacionDesde.HasValue Then
comando.Parameters.AddWithValue("@FechaCreacionDesde", oFiltro.FechaCreacionDesde)
End If
If oFiltro.FechaCreacionHasta.HasValue Then
comando.Parameters.AddWithValue("@FechaCreacionHasta", oFiltro.FechaCreacionHasta)
End If
If oFiltro.FechaModificacionDesde.HasValue Then
comando.Parameters.AddWithValue("@FechaModificacionDesde", oFiltro.FechaModificacionDesde)
End If
If oFiltro.FechaModificacionHasta.HasValue Then
comando.Parameters.AddWithValue("@FechaModificacionHasta", oFiltro.FechaModificacionHasta)
End If
If oFiltro.IdInicioSesionModificacion.HasValue Then
comando.Parameters.AddWithValue("@IdInicioSesionModificacion", oFiltro.IdInicioSesionModificacion)
End If
		        If conexion IsNot Nothing Then
                    estadoInicialConexion = conexion.State
                Else
                    conexion = New SqlConnection()
                    conexion.ConnectionString = DAL.ConectDal.DevolverCadenaConexion(iIdRefCampo)
                    estadoInicialConexion = ConnectionState.Closed
                    conexion.Open()
                End If
				comando.connection = conexion
				reader = comando.ExecuteReader
				While reader.Read
					agregadoresColeccion.Add(zCrearUnoDelReader(reader))
				End While
				Return agregadoresColeccion
            Finally
				 If reader IsNot Nothing AndAlso Not reader.IsClosed Then
                    reader.Close()
                End If
                If conexion IsNot Nothing AndAlso estadoInicialConexion = ConnectionState.Closed AndAlso conexion.State = ConnectionState.Open Then
                    conexion.Close()
                End If
            End Try
		End Function
	
	End Class
	
End Namespace
