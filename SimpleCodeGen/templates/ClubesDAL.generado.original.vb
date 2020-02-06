
Imports System.Data
Imports System.Data.SqlClient

Namespace MGW.ReservasOnline

#Region "Club métodos"
    Partial Public Class Club
		Private Shared Function zCrearUnoDelReader(ByVal dataRecord As IDataRecord) As MGW.ReservasOnline.Club
			Dim club As New MGW.ReservasOnline.Club
    		club.IdClub=dataRecord("IdClub")
club.Nombre = dataRecord("Nombre")
club.IdTeeSheetTipo = dataRecord("IdTeeSheetTipo")
club.IdTeeSheetClub = dataRecord("IdTeeSheetClub")
If Not dataRecord("UrlAccesoTeeSheet") Is DBNull.Value Then
club.UrlAccesoTeeSheet = dataRecord("UrlAccesoTeeSheet")
End If
club.idPais = dataRecord("idPais")
club.BorradoLogico = dataRecord("BorradoLogico")
club.FechaCreacion = dataRecord("FechaCreacion")
club.FechaModificacion = dataRecord("FechaModificacion")
club.IdInicioSesionModificacion = dataRecord("IdInicioSesionModificacion")
            return club			
        End Function
        Public Shared Function zRellenarDataTable(ByVal datatable As DataTable, iIdRefCampo as int32, Optional ByVal conexion As SqlConnection = Nothing, Optional ByVal transaccion As SqlTransaction = Nothing) As Integer
            Dim SQL As String = ""
            Dim comando As SqlCommand
            Dim adapter As SqlDataAdapter
			SQL = "SELECT IdClub,Nombre,IdTeeSheetTipo,IdTeeSheetClub,UrlAccesoTeeSheet,idPais,BorradoLogico,FechaCreacion,FechaModificacion,IdInicioSesionModificacion FROM CLU_Clubes " & _
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
        Public Shared Function zModificar(ByVal oClub As MGW.ReservasOnline.Club, iIdRefCampo as int32, Optional ByVal conexion As SqlConnection = Nothing, Optional ByVal transaccion As SqlTransaction = Nothing) As Integer
            Dim SQL As String = ""
            Dim comando As SqlCommand
            Dim contadorClubes As Integer
			Dim estadoInicialConexion As ConnectionState
			SQL = "UPDATE  CLU_Clubes SET Nombre=@Nombre,IdTeeSheetTipo=@IdTeeSheetTipo,IdTeeSheetClub=@IdTeeSheetClub,UrlAccesoTeeSheet=@UrlAccesoTeeSheet,idPais=@idPais,BorradoLogico=@BorradoLogico,FechaCreacion=@FechaCreacion,FechaModificacion=@FechaModificacion,IdInicioSesionModificacion=@IdInicioSesionModificacion WHERE (IdClub=@IdClub" & _
					" AND BorradoLogico= 0);"
            Try
                comando = New SqlCommand
				comando.CommandText = SQL
                comando.CommandType = CommandType.Text
                comando.Transaction = transaccion
				comando.Parameters.Add("@IdClub",  SqlDbType.int)
				comando.Parameters("@IdClub").SqlValue = oClub.IdClub
comando.Parameters.Add(New SqlParameter("@Nombre", SqlDbType.NVarChar))
comando.Parameters("@Nombre").SqlValue = oClub.Nombre
comando.Parameters.Add(New SqlParameter("@IdTeeSheetTipo", SqlDbType.Int))
comando.Parameters("@IdTeeSheetTipo").SqlValue = oClub.IdTeeSheetTipo
comando.Parameters.Add(New SqlParameter("@IdTeeSheetClub", SqlDbType.NVarChar))
comando.Parameters("@IdTeeSheetClub").SqlValue = oClub.IdTeeSheetClub
comando.Parameters.Add(New SqlParameter("@UrlAccesoTeeSheet", SqlDbType.NVarChar))
If (Not oClub.UrlAccesoTeeSheet Is Nothing) Then
comando.Parameters("@UrlAccesoTeeSheet").SqlValue = oClub.UrlAccesoTeeSheet
Else
comando.Parameters("@UrlAccesoTeeSheet").SqlValue = DBNull.Value
End If
comando.Parameters.Add(New SqlParameter("@idPais", SqlDbType.Int))
comando.Parameters("@idPais").SqlValue = oClub.idPais
comando.Parameters.Add(New SqlParameter("@BorradoLogico", SqlDbType.Bit))
comando.Parameters("@BorradoLogico").SqlValue = oClub.BorradoLogico
comando.Parameters.Add(New SqlParameter("@FechaCreacion", SqlDbType.DateTime))
comando.Parameters("@FechaCreacion").SqlValue = oClub.FechaCreacion
comando.Parameters.Add(New SqlParameter("@FechaModificacion", SqlDbType.DateTime))
comando.Parameters("@FechaModificacion").SqlValue = oClub.FechaModificacion
comando.Parameters.Add(New SqlParameter("@IdInicioSesionModificacion", SqlDbType.Int))
comando.Parameters("@IdInicioSesionModificacion").SqlValue = oClub.IdInicioSesionModificacion
				if conexion IsNot Nothing Then
					estadoInicialConexion = conexion.State
				Else
					conexion = New SqlConnection()
                	conexion.ConnectionString = DAL.ConectDal.DevolverCadenaConexion(iIdRefCampo)
                	estadoInicialConexion = ConnectionState.Closed
                	conexion.Open()
				End If
				comando.Connection= conexion
                contadorClubes = comando.ExecuteNonQuery()
                If contadorClubes = 0 Then
                    Throw New Exception("Error concurrencia")
                End If
                Return contadorClubes
            Finally
                If conexion IsNot Nothing AndAlso estadoInicialConexion = ConnectionState.Closed AndAlso conexion.State <> ConnectionState.Closed Then
                    conexion.Close()
                End If               
            End Try
        End Function
        Public Shared Function zInsertar(ByVal oClub As MGW.ReservasOnline.Club, iIdRefCampo as int32, Optional ByVal conexion As SqlConnection = Nothing, Optional ByVal transaccion As SqlTransaction = Nothing) As Integer
            Dim SQL As String = ""
            Dim comando As SqlCommand
            Dim contadorClubes As Integer
          	Dim estadoInicialConexion As ConnectionState
	        SQL = "INSERT INTO CLU_Clubes (Nombre,IdTeeSheetTipo,IdTeeSheetClub,UrlAccesoTeeSheet,idPais,BorradoLogico,FechaCreacion,FechaModificacion,IdInicioSesionModificacion) VALUES (@Nombre,@IdTeeSheetTipo,@IdTeeSheetClub,@UrlAccesoTeeSheet,@idPais,@BorradoLogico,@FechaCreacion,@FechaModificacion,@IdInicioSesionModificacion); " & _
					" SELECT @Insertado = SCOPE_IDENTITY(); "
            Try
                comando = New SqlCommand(SQL, conexion)
                comando.CommandType = CommandType.Text
                comando.Transaction = transaccion
comando.Parameters.Add(New SqlParameter("@Nombre", SqlDbType.NVarChar))
comando.Parameters("@Nombre").SqlValue = oClub.Nombre
comando.Parameters.Add(New SqlParameter("@IdTeeSheetTipo", SqlDbType.Int))
comando.Parameters("@IdTeeSheetTipo").SqlValue = oClub.IdTeeSheetTipo
comando.Parameters.Add(New SqlParameter("@IdTeeSheetClub", SqlDbType.NVarChar))
comando.Parameters("@IdTeeSheetClub").SqlValue = oClub.IdTeeSheetClub
comando.Parameters.Add(New SqlParameter("@UrlAccesoTeeSheet", SqlDbType.NVarChar))
If (Not oClub.UrlAccesoTeeSheet Is Nothing) Then
comando.Parameters("@UrlAccesoTeeSheet").SqlValue = oClub.UrlAccesoTeeSheet
Else
comando.Parameters("@UrlAccesoTeeSheet").SqlValue = DBNull.Value
End If
comando.Parameters.Add(New SqlParameter("@idPais", SqlDbType.Int))
comando.Parameters("@idPais").SqlValue = oClub.idPais
comando.Parameters.Add(New SqlParameter("@BorradoLogico", SqlDbType.Bit))
comando.Parameters("@BorradoLogico").SqlValue = oClub.BorradoLogico
comando.Parameters.Add(New SqlParameter("@FechaCreacion", SqlDbType.DateTime))
comando.Parameters("@FechaCreacion").SqlValue = oClub.FechaCreacion
comando.Parameters.Add(New SqlParameter("@FechaModificacion", SqlDbType.DateTime))
comando.Parameters("@FechaModificacion").SqlValue = oClub.FechaModificacion
comando.Parameters.Add(New SqlParameter("@IdInicioSesionModificacion", SqlDbType.Int))
comando.Parameters("@IdInicioSesionModificacion").SqlValue = oClub.IdInicioSesionModificacion
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
				contadorClubes = comando.ExecuteNonQuery()
				If contadorClubes = 1 Then
                    oClub.IdClub = comando.Parameters("@Insertado").Value
				End If
                Return contadorClubes
           	Finally
                If conexion IsNot Nothing AndAlso estadoInicialConexion = ConnectionState.Closed AndAlso conexion.State <> ConnectionState.Closed Then
                    conexion.Close()
                End If
            End Try
        End Function
        Public Shared Function zDevolverPorId(ByVal IdClub As Int32, iIdRefCampo as int32, Optional ByVal conexion As SqlConnection = Nothing, Optional ByVal transaccion As SqlTransaction = Nothing) As MGW.ReservasOnline.ClubesColeccion
			Dim SQL As String = ""
            Dim comando As SqlCommand
            Dim reader As SqlDataReader = Nothing
			Dim estadoInicialConexion As ConnectionState
			Dim clubes As MGW.ReservasOnline.ClubesColeccion
			 SQL = "SELECT IdClub,Nombre,IdTeeSheetTipo,IdTeeSheetClub,UrlAccesoTeeSheet,idPais,BorradoLogico,FechaCreacion,FechaModificacion,IdInicioSesionModificacion FROM CLU_Clubes WHERE IdClub=@IdClub AND BorradoLogico = 0;"
            Try
				clubes = New MGW.ReservasOnline.ClubesColeccion
                comando = New SqlCommand
				comando.CommandText = SQL
                comando.CommandType = CommandType.Text
                comando.Transaction = transaccion
                comando.Parameters.AddWithValue("@IdClub", IdClub)
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
					clubes.Add(zCrearUnoDelReader(reader))
                End While
                Return clubes
			Finally
				If reader isNOT Nothing AndAlso Not reader.IsClosed Then
   					reader.Close()
  				End If
                If conexion IsNot Nothing AndAlso estadoInicialConexion = ConnectionState.Closed AndAlso conexion.State = ConnectionState.Open Then
                    conexion.Close()
                End If
            End Try
        End Function 
        Public Shared Function zEliminarFisicoPorId(ByVal IdClub As Int32, iIdRefCampo as int32, Optional ByVal conexion As SqlConnection = Nothing, Optional ByVal transaccion As SqlTransaction = Nothing) As Integer
            Dim SQL As String = ""
            Dim comando As SqlCommand
            Dim contadorClubes As Integer
			Dim estadoInicialConexion As ConnectionState
            SQL = "DELETE CLU_Clubes WHERE (IdClub=@IdClub);"
            Try
                comando = New SqlCommand
				comando.CommandText = SQL
                comando.CommandType = CommandType.Text
                comando.Transaction = transaccion
                comando.Parameters.Add("@IdClub", SqlDbType.int)
                comando.Parameters("@IdClub").SqlValue = IdClub
				if conexion IsNot Nothing Then
					estadoInicialConexion = conexion.State
				Else
					conexion = New SqlConnection()
                	conexion.ConnectionString = DAL.ConectDal.DevolverCadenaConexion(iIdRefCampo)
                	estadoInicialConexion = ConnectionState.Closed
                	conexion.Open()
				End If
				comando.Connection= conexion
                contadorClubes = comando.ExecuteNonQuery()
                Return contadorClubes
           Finally
                If conexion IsNot Nothing AndAlso estadoInicialConexion = ConnectionState.Closed AndAlso conexion.State <> ConnectionState.Closed Then
                    conexion.Close()
                End If               
            End Try
        End Function		
		Public Shared Function zDevolverColeccionBusqueda(ByVal oFiltro As MGW.ReservasOnline.ClubFiltro, iIdRefCampo as int32, Optional ByVal conexion As SqlConnection= Nothing, Optional ByVal transaccion As SqlTransaction = Nothing) As MGW.ReservasOnline.ClubesColeccion
		Dim SQL As String = ""
        Dim comando As SqlCommand
        Dim reader As SqlDataReader = Nothing
        Dim clubesColeccion As MGW.ReservasOnline.ClubesColeccion
        Dim club As MGW.ReservasOnline.Club
        Dim estadoInicialConexion As ConnectionState
		SQL ="SELECT IdClub,Nombre,IdTeeSheetTipo,IdTeeSheetClub,UrlAccesoTeeSheet,idPais,BorradoLogico,FechaCreacion,FechaModificacion,IdInicioSesionModificacion FROM CLU_Clubes WHERE 1=1"
If oFiltro.IdClub.HasValue Then
SQL +=" and IdClub = @IdClub"
End If
If not String.IsNullOrEmpty(oFiltro.Nombre) Then
SQL +=" and Nombre like '%' + @Nombre + '%'"
End If
If oFiltro.IdTeeSheetTipo.HasValue Then
SQL +=" and IdTeeSheetTipo = @IdTeeSheetTipo"
End If
If not String.IsNullOrEmpty(oFiltro.IdTeeSheetClub) Then
SQL +=" and IdTeeSheetClub like '%' + @IdTeeSheetClub + '%'"
End If
If not String.IsNullOrEmpty(oFiltro.UrlAccesoTeeSheet) Then
SQL +=" and UrlAccesoTeeSheet like '%' + @UrlAccesoTeeSheet + '%'"
End If
If oFiltro.idPais.HasValue Then
SQL +=" and idPais = @idPais"
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
				clubesColeccion = New MGW.ReservasOnline.ClubesColeccion
				club = New MGW.ReservasOnline.Club
                comando = New SqlCommand
				comando.commandtext = SQL
                comando.CommandType = CommandType.Text
                comando.Transaction = transaccion
If oFiltro.IdClub.HasValue Then
comando.Parameters.AddWithValue("@IdClub", oFiltro.IdClub)
End If
If Not String.IsNullOrEmpty(oFiltro.Nombre) Then
comando.Parameters.AddWithValue("@Nombre", oFiltro.Nombre)
End If
If oFiltro.IdTeeSheetTipo.HasValue Then
comando.Parameters.AddWithValue("@IdTeeSheetTipo", oFiltro.IdTeeSheetTipo)
End If
If Not String.IsNullOrEmpty(oFiltro.IdTeeSheetClub) Then
comando.Parameters.AddWithValue("@IdTeeSheetClub", oFiltro.IdTeeSheetClub)
End If
If Not String.IsNullOrEmpty(oFiltro.UrlAccesoTeeSheet) Then
comando.Parameters.AddWithValue("@UrlAccesoTeeSheet", oFiltro.UrlAccesoTeeSheet)
End If
If oFiltro.idPais.HasValue Then
comando.Parameters.AddWithValue("@idPais", oFiltro.idPais)
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
					clubesColeccion.Add(zCrearUnoDelReader(reader))
				End While
				Return clubesColeccion
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
	#End Region
	
End Namespace

