'------------------------------------------------------------------------------
' <autogenerado>
'    Código generado mediante plantillas personalizadas de CodeSmith.
'    Fecha:   10/01/2018 06:32  
'    Versión CodeSmith: 4.0.0.0
'
'    Modificaciones en este archivo pueden generar un funcionamiento incorrecto.
'    Realizar las modificaciones sobre otro archivo con el mismo nombre, utilizando partial class y almacenado fuera de esta carpeta de archivos autogenerados.
' </autogenerado>
'------------------------------------------------------------------------------

Namespace MGW.ReservasOnline
    <Serializable()> _
    Partial Public Class Club
	#Region " variables "
Private mIdClub As System.Int32
Private mNombre As System.String
Private mIdTeeSheetTipo As System.Int32
Private mIdTeeSheetClub As System.String
Private mUrlAccesoTeeSheet As System.String
Private midPais As System.Int32
Private mBorradoLogico As System.Boolean
Private mFechaCreacion As System.DateTime
Private mFechaModificacion As System.DateTime
Private mIdInicioSesionModificacion As System.Int32
		#End Region
	#Region " propiedades "
Public Property IdClub() As System.Int32
Get
Return mIdClub
End Get
Set (ByVal value As System.Int32)
mIdClub = value
End Set
End Property
Public Property Nombre() As System.String
Get
Return mNombre
End Get
Set (ByVal value As System.String)
mNombre = value
End Set
End Property
Public Property IdTeeSheetTipo() As System.Int32
Get
Return mIdTeeSheetTipo
End Get
Set (ByVal value As System.Int32)
mIdTeeSheetTipo = value
End Set
End Property
Public Property IdTeeSheetClub() As System.String
Get
Return mIdTeeSheetClub
End Get
Set (ByVal value As System.String)
mIdTeeSheetClub = value
End Set
End Property
Public Property UrlAccesoTeeSheet() As System.String
Get
Return mUrlAccesoTeeSheet
End Get
Set (ByVal value As System.String)
mUrlAccesoTeeSheet = value
End Set
End Property
Public Property idPais() As System.Int32
Get
Return midPais
End Get
Set (ByVal value As System.Int32)
midPais = value
End Set
End Property
Public Property BorradoLogico() As System.Boolean
Get
Return mBorradoLogico
End Get
Set (ByVal value As System.Boolean)
mBorradoLogico = value
End Set
End Property
Public Property FechaCreacion() As System.DateTime
Get
Return mFechaCreacion
End Get
Set (ByVal value As System.DateTime)
mFechaCreacion = value
End Set
End Property
Public Property FechaModificacion() As System.DateTime
Get
Return mFechaModificacion
End Get
Set (ByVal value As System.DateTime)
mFechaModificacion = value
End Set
End Property
Public Property IdInicioSesionModificacion() As System.Int32
Get
Return mIdInicioSesionModificacion
End Get
Set (ByVal value As System.Int32)
mIdInicioSesionModificacion = value
End Set
End Property
	#End Region	
    End Class

    <Serializable()> _
    Partial Public Class ClubesColeccion
        Inherits System.Collections.Generic.List(Of Club)
    End Class
	
	
	<Serializable()> _
	Partial Public Class ClubFiltro
    #Region " variables/propiedades "
Private mIdClub As  System.Nullable(of System.Int32)
Public Property IdClub() As  System.Nullable(of System.Int32)
Get
Return mIdClub
End Get
Set (ByVal value As System.Nullable(of System.Int32))
mIdClub = value
End Set
End Property
Private mNombre As System.String
Public Property Nombre() As System.String
Get
Return mNombre
End Get
Set (ByVal value As System.String)
mNombre = value
End Set
End Property
Private mIdTeeSheetTipo As  System.Nullable(of System.Int32)
Public Property IdTeeSheetTipo() As  System.Nullable(of System.Int32)
Get
Return mIdTeeSheetTipo
End Get
Set (ByVal value As System.Nullable(of System.Int32))
mIdTeeSheetTipo = value
End Set
End Property
Private mIdTeeSheetClub As System.String
Public Property IdTeeSheetClub() As System.String
Get
Return mIdTeeSheetClub
End Get
Set (ByVal value As System.String)
mIdTeeSheetClub = value
End Set
End Property
Private mUrlAccesoTeeSheet As System.String
Public Property UrlAccesoTeeSheet() As System.String
Get
Return mUrlAccesoTeeSheet
End Get
Set (ByVal value As System.String)
mUrlAccesoTeeSheet = value
End Set
End Property
Private midPais As  System.Nullable(of System.Int32)
Public Property idPais() As  System.Nullable(of System.Int32)
Get
Return midPais
End Get
Set (ByVal value As System.Nullable(of System.Int32))
midPais = value
End Set
End Property
Private mBorradoLogico As  System.Nullable(of System.Boolean)
Public Property BorradoLogico() As  System.Nullable(of System.Boolean)
Get
Return mBorradoLogico
End Get
Set (ByVal value As System.Nullable(of System.Boolean))
mBorradoLogico = value
End Set
End Property
Private mFechaCreacionDesde As System.Nullable(of System.DateTime)
Public Property FechaCreacionDesde() As  System.Nullable(of System.DateTime)
Get
Return mFechaCreacionDesde
End Get
Set (ByVal value As System.Nullable(of System.DateTime))
mFechaCreacionDesde = value
End Set
End Property
Private mFechaCreacionHasta As  System.Nullable(of System.DateTime)
Public Property FechaCreacionHasta() As  System.Nullable(of System.DateTime)
Get
Return mFechaCreacionHasta
End Get
Set (ByVal value As System.Nullable(of System.DateTime))
mFechaCreacionHasta = value
End Set
End Property
Private mFechaModificacionDesde As System.Nullable(of System.DateTime)
Public Property FechaModificacionDesde() As  System.Nullable(of System.DateTime)
Get
Return mFechaModificacionDesde
End Get
Set (ByVal value As System.Nullable(of System.DateTime))
mFechaModificacionDesde = value
End Set
End Property
Private mFechaModificacionHasta As  System.Nullable(of System.DateTime)
Public Property FechaModificacionHasta() As  System.Nullable(of System.DateTime)
Get
Return mFechaModificacionHasta
End Get
Set (ByVal value As System.Nullable(of System.DateTime))
mFechaModificacionHasta = value
End Set
End Property
Private mIdInicioSesionModificacion As  System.Nullable(of System.Int32)
Public Property IdInicioSesionModificacion() As  System.Nullable(of System.Int32)
Get
Return mIdInicioSesionModificacion
End Get
Set (ByVal value As System.Nullable(of System.Int32))
mIdInicioSesionModificacion = value
End Set
End Property
		#End Region
    End Class
	
	
End Namespace


